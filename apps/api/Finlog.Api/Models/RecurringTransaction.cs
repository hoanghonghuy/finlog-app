using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class RecurringTransaction
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid AccountId { get; set; }
    
    public Guid? ToAccountId { get; set; }
    
    public Guid? PayeeId { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(20)]
    public string TransactionType { get; set; } = "EXPENSE";
    
    [Required]
    [StringLength(20)]
    public string Frequency { get; set; } = "MONTHLY"; // DAILY, WEEKLY, MONTHLY, YEARLY
    
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int? OccurrenceCount { get; set; } // Số lần lặp (optional)
    
    [StringLength(1000)]
    public string? Notes { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}