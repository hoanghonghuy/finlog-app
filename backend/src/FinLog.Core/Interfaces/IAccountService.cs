using FinLog.Core.DTOs;

namespace FinLog.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IReadOnlyList<AccountDto>> GetAllAsync();
        Task<AccountDto?> GetByIdAsync(int id);
        Task<AccountDto> CreateAsync(AccountCreateDto dto);
        Task UpdateAsync(int id, AccountCreateDto dto); // Thường chỉ đổi tên/loại
        Task DeleteAsync(int id);
    }
}
