namespace Finlog.Api.Models;

public class TransactionTag
{
    public Guid TransactionId { get; set; }
    public Guid TagId { get; set; }
    
    // Navigation properties
    public Transaction? Transaction { get; set; }
    public Tag? Tag { get; set; }
}