using FinLog.Core.DTOs;

namespace FinLog.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(int id, CategoryCreateDto dto);
        Task DeleteAsync(int id);
    }
}
