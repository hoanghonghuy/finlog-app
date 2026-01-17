using AutoMapper;
using FinLog.Core.DTOs;
using FinLog.Core.Entities;
using FinLog.Core.Interfaces;

namespace FinLog.Core.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepository<Budget> _repository;
        private readonly IMapper _mapper;

        public BudgetService(IRepository<Budget> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BudgetDto>> GetAllAsync()
        {
            var budgets = await _repository.ListAllAsync();
            return _mapper.Map<IReadOnlyList<BudgetDto>>(budgets);
        }

        public async Task<IReadOnlyList<BudgetDto>> GetByMonthYearAsync(int month, int year)
        {
             var budgets = await _repository.ListAsync(b => b.Month == month && b.Year == year);
             return _mapper.Map<IReadOnlyList<BudgetDto>>(budgets);
        }

        public async Task<BudgetDto> CreateAsync(BudgetCreateDto dto)
        {
            var budget = _mapper.Map<Budget>(dto);
            await _repository.AddAsync(budget);
            return _mapper.Map<BudgetDto>(budget);
        }

        public async Task UpdateAsync(int id, BudgetCreateDto dto)
        {
            var budget = await _repository.GetByIdAsync(id);
            if (budget != null)
            {
                _mapper.Map(dto, budget);
                await _repository.UpdateAsync(budget);
            }
        }

        public async Task DeleteAsync(int id)
        {
             var budget = await _repository.GetByIdAsync(id);
            if (budget != null)
            {
                await _repository.DeleteAsync(budget);
            }
        }
    }
}
