using System;

namespace FinLog.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
