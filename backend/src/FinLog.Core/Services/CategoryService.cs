using AutoMapper;
using FinLog.Core.DTOs;
using FinLog.Core.Entities;
using FinLog.Core.Interfaces;

namespace FinLog.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.ListAllAsync();
            return _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(int id, CategoryCreateDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
            {
                _mapper.Map(dto, category);
                await _repository.UpdateAsync(category);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
            {
                await _repository.DeleteAsync(category);
            }
        }
    }
}
