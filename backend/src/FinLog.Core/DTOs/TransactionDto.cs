using System;

namespace FinLog.Core.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int? AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
