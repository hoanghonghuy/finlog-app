using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Account
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public decimal Balance { get; set; }
    
    [Required]
    [StringLength(20)]
    public string AccountType { get; set; } = "CASH"; // CASH, BANK, CREDIT_CARD, LOAN
    
    public string? Currency { get; set; } = "VND";
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}