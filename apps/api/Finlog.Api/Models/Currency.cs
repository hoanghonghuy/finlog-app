using System.ComponentModel.DataAnnotations;

namespace Finlog.Api.Models;

public class Currency
{
    [Required]
    [StringLength(3)]
    public string Code { get; set; } = "VND"; // USD, EUR, VND, etc.
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(5)]
    public string? Symbol { get; set; }
    
    public int DecimalPlaces { get; set; } = 2;
    
    public decimal ExchangeRate { get; set; } = 1.0m; // So với currency base
    
    public bool IsBaseCurrency { get; set; } = false;
    
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}