using FinLog.Core.DTOs;
using FinLog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _service;

        public BudgetsController(IBudgetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? month, [FromQuery] int? year)
        {
            if (month.HasValue && year.HasValue)
            {
                var result = await _service.GetByMonthYearAsync(month.Value, year.Value);
                return Ok(result);
            }
            
            var all = await _service.GetAllAsync();
            return Ok(all);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BudgetCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
