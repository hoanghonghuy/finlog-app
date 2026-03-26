using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Transaction
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid AccountId { get; set; } // Source account
    
    public Guid? ToAccountId { get; set; } // Destination account (for transfers only)
    
    public Guid? PayeeId { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    [Required]
    [Range(-999999999, 999999999)]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(20)]
    public string TransactionType { get; set; } = "EXPENSE"; // EXPENSE, INCOME, TRANSFER
    
    [Required]
    public DateTime TransactionDate { get; set; }
    
    [StringLength(1000)]
    public string? Notes { get; set; }
    
    public bool IsReconciled { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}