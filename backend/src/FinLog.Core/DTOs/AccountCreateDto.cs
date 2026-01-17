using FinLog.Core.Enums;

namespace FinLog.Core.DTOs
{
    public class AccountCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
