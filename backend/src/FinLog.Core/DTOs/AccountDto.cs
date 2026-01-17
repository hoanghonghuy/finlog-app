using FinLog.Core.Enums;

namespace FinLog.Core.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
        public decimal CurrentBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
