using AutoMapper;
using FinLog.Core.DTOs;
using FinLog.Core.Entities;
using FinLog.Core.Interfaces;

namespace FinLog.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _repository;
        private readonly IMapper _mapper;

        public AccountService(IRepository<Account> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<AccountDto>> GetAllAsync()
        {
            var accounts = await _repository.ListAllAsync();
            return _mapper.Map<IReadOnlyList<AccountDto>>(accounts);
        }

        public async Task<AccountDto?> GetByIdAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            return account == null ? null : _mapper.Map<AccountDto>(account);
        }

        public async Task<AccountDto> CreateAsync(AccountCreateDto dto)
        {
            var account = _mapper.Map<Account>(dto);
            // Số dư hiện tại được lấy từ Số dư ban đầu
            await _repository.AddAsync(account);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task UpdateAsync(int id, AccountCreateDto dto)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account != null)
            {
                // Chỉ cập nhật tên và loại tài khoản, số dư thường giữ nguyên
                account.Name = dto.Name;
                account.Type = dto.Type;
                // Bỏ qua InitialBalance vì đây là cập nhật thông tin
                await _repository.UpdateAsync(account);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account != null)
            {
                await _repository.DeleteAsync(account);
            }
        }
    }
}
