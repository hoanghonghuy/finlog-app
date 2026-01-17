using AutoMapper;
using FinLog.Core.DTOs;
using FinLog.Core.Entities;
using FinLog.Core.Enums;
using FinLog.Core.Interfaces;

namespace FinLog.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public TransactionService(
            IRepository<Transaction> repository, 
            IRepository<Category> categoryRepository, 
            IRepository<Account> accountRepository,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TransactionDto>> GetAllAsync()
        {
            var transactions = await _repository.ListAllAsync();
            return _mapper.Map<IReadOnlyList<TransactionDto>>(transactions);
        }

        public async Task<IReadOnlyList<TransactionDto>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            // Lọc giao dịch trong khoảng thời gian (EF Core tự xử lý so sánh ngày tháng)
            var transactions = await _repository.ListAsync(t => t.Date >= start && t.Date <= end);
            return _mapper.Map<IReadOnlyList<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            return transaction == null ? null : _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> CreateAsync(TransactionCreateDto dto)
        {
            // Kiểm tra Danh mục có tồn tại không
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null) throw new ArgumentException("Invalid Category ID");

            // Kiểm tra Tài khoản có tồn tại không
            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account == null) throw new ArgumentException("Invalid Account ID");

            // Tạo Giao dịch mới
            var transaction = _mapper.Map<Transaction>(dto);
            await _repository.AddAsync(transaction);

            // Tự động cập nhật số dư Tài khoản
            // Nếu là Thu nhập -> Cộng tiền, Chi tiêu -> Trừ tiền
            if (category.Type == TransactionType.Income)
            {
                account.CurrentBalance += transaction.Amount;
            }
            else
            {
                account.CurrentBalance -= transaction.Amount;
            }
            await _accountRepository.UpdateAsync(account);

            // Gán lại object để trả về kết quả đầy đủ (cho UI hiển thị ngay)
            transaction.Category = category;
            transaction.Account = account;
            
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateAsync(int id, TransactionCreateDto dto)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction != null)
            {
                // 1. Hoàn lại số dư cũ (Revert Old Balance)
                // Cần lấy thông tin Danh mục/tài khoản cũ để hoàn tiền lại
                // Dùng .HasValue vì FK có thể null (nếu danh mục cũ đã bị xoá)
                Category? oldCategory = null;
                if (transaction.CategoryId.HasValue) 
                    oldCategory = await _categoryRepository.GetByIdAsync(transaction.CategoryId.Value);

                Account? oldAccount = null;
                if (transaction.AccountId.HasValue) 
                    oldAccount = await _accountRepository.GetByIdAsync(transaction.AccountId.Value);

                if (oldCategory != null && oldAccount != null)
                {
                    // Logic ngược lại với lúc tạo: 
                    // Nếu ngày xưa là Thu nhập (đã cộng) -> Giờ trừ đi
                    if (oldCategory.Type == TransactionType.Income)
                        oldAccount.CurrentBalance -= transaction.Amount;
                    else
                        oldAccount.CurrentBalance += transaction.Amount; // Nếu là chi tiêu (đã trừ) -> Giờ cộng lại
                    
                    await _accountRepository.UpdateAsync(oldAccount);
                }

                // 2. Cập nhật thông tin mới vào entity (nhưng chưa lưu xuống DB ngay)
                _mapper.Map(dto, transaction);

                // 3. Áp dụng số dư mới (Apply New Balance)
                // Lấy thông tin Danh mục/Tài khoản mới từ DTO gửi lên
                var newCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId); // Bắt buộc phải có
                var newAccount = await _accountRepository.GetByIdAsync(dto.AccountId);    // Bắt buộc phải có

                if (newCategory == null) throw new ArgumentException("Invalid Category ID");
                if (newAccount == null) throw new ArgumentException("Invalid Account ID");

                // Tính toán lại số dư mới
                if (newCategory.Type == TransactionType.Income)
                    newAccount.CurrentBalance += transaction.Amount; // Cộng số tiền mới
                else
                    newAccount.CurrentBalance -= transaction.Amount; // Trừ số tiền mới

                await _accountRepository.UpdateAsync(newAccount);

                // 4. Lưu tất cả thay đổi của giao dịch xuống Database
                await _repository.UpdateAsync(transaction);
            }
        }

        public async Task DeleteAsync(int id)
        {
             var transaction = await _repository.GetByIdAsync(id);
             if (transaction != null)
             {
                  // Cần lấy Category và Account để hoàn lại tiền vào tài khoản
                  // Vì đây là Repository Generic nên không có sẵn Include, phải query thủ công
                  
                  Category? category = null;
                 if (transaction.CategoryId.HasValue)
                 {
                    category = await _categoryRepository.GetByIdAsync(transaction.CategoryId.Value);
                 }

                 Account? account = null;
                 if (transaction.AccountId.HasValue)
                 {
                    account = await _accountRepository.GetByIdAsync(transaction.AccountId.Value);
                 }

                 if (category != null && account != null)
                 {
                     if (category.Type == TransactionType.Income)
                        account.CurrentBalance -= transaction.Amount;
                     else
                        account.CurrentBalance += transaction.Amount;
                        
                     await _accountRepository.UpdateAsync(account);
                 }

                 await _repository.DeleteAsync(transaction);
             }
        }
    }
}
