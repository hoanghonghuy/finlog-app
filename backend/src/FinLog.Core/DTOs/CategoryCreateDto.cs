using FinLog.Core.Enums;

namespace FinLog.Core.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public string? ColorCode { get; set; }
    }
}
