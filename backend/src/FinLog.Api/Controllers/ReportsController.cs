using FinLog.Core.DTOs;
using FinLog.Core.Enums;
using FinLog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ICategoryService _categoryService;

        public ReportsController(ITransactionService transactionService, ICategoryService categoryService)
        {
            _transactionService = transactionService;
            _categoryService = categoryService;
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyStats([FromQuery] int month, [FromQuery] int year)
        {
            if (month < 1 || month > 12 || year < 2000) return BadRequest("Invalid Date");

            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1).AddDays(-1);

            var transactions = await _transactionService.GetByDateRangeAsync(start, end);

            // Cần xác định giao dịch nào là Thu nhập, giao dịch nào là Chi tiêu
            // Thông tin này nằm trong Category (Type).
            // Lấy tất cả danh mục về để tạo map tra cứu cho nhanh (Lookup Dictionary).
            var categories = await _categoryService.GetAllAsync();
            var categoryMap = categories.ToDictionary(c => c.Id, c => c.Type);

            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (var t in transactions)
            {
                if (t.CategoryId.HasValue && categoryMap.TryGetValue(t.CategoryId.Value, out var type))
                {
                   if (type == TransactionType.Income) totalIncome += t.Amount;
                   else totalExpense += t.Amount;
                }
            }

            var stats = new MonthlyStatsDto
            {
                Month = month,
                Year = year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense
            };

            return Ok(stats);
        }
    }
}
