using FinLog.Core.DTOs;

namespace FinLog.Core.Interfaces
{
    public interface IBudgetService
    {
        Task<IReadOnlyList<BudgetDto>> GetAllAsync();
        Task<IReadOnlyList<BudgetDto>> GetByMonthYearAsync(int month, int year);
        Task<BudgetDto> CreateAsync(BudgetCreateDto dto);
        Task UpdateAsync(int id, BudgetCreateDto dto);
        Task DeleteAsync(int id);
    }
}
