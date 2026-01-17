namespace FinLog.Core.DTOs
{
    public class MonthlyStatsDto
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetSavings => TotalIncome - TotalExpense;
    }
}
