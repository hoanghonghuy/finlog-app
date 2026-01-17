using FinLog.Core.DTOs;

namespace FinLog.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IReadOnlyList<TransactionDto>> GetAllAsync();
        Task<IReadOnlyList<TransactionDto>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<TransactionDto?> GetByIdAsync(int id);
        Task<TransactionDto> CreateAsync(TransactionCreateDto dto);
        Task UpdateAsync(int id, TransactionCreateDto dto);
        Task DeleteAsync(int id);
    }
}
