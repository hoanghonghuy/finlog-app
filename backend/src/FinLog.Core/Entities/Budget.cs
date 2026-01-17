namespace FinLog.Core.Entities
{
    public class Budget : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        public decimal Amount { get; set; } // Monthly limit
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
