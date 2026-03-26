using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Category
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    public Guid? ParentId { get; set; } // Self-referencing for subcategories
    
    public bool IsIncome { get; set; } // true = income category, false = expense category
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}