using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Budget
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid CategoryId { get; set; }
    
    [Required]
    [Range(0, 999999999)]
    public decimal Amount { get; set; }
    
    [Required]
    public DateTime PeriodStart { get; set; }
    
    [Required]
    public DateTime PeriodEnd { get; set; }
    
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}