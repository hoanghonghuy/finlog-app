using AutoMapper;
using FinLog.Core.DTOs;
using FinLog.Core.Entities;

namespace FinLog.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            
            CreateMap<Transaction, TransactionDto>()
                // Kiểm tra null để tránh lỗi khi Danh mục/Tài khoản đã bị xoá
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account != null ? src.Account.Name : string.Empty));
            
            CreateMap<TransactionCreateDto, Transaction>();
            
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<AccountCreateDto, Account>()
                .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.InitialBalance));

            CreateMap<Budget, BudgetDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BudgetCreateDto, Budget>();
        }
    }
}
