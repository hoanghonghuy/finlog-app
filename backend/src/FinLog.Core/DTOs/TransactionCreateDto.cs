using System;

namespace FinLog.Core.DTOs
{
    public class TransactionCreateDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
    }
}
