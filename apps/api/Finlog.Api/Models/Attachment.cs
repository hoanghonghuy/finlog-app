using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Attachment
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid TransactionId { get; set; }
    
    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? FileName { get; set; }
    
    [StringLength(50)]
    public string? FileType { get; set; } // image/jpeg, application/pdf, etc.
    
    public long FileSize { get; set; } // bytes
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}