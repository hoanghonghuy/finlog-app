using FinLog.Core.Enums;

namespace FinLog.Core.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public string? ColorCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
