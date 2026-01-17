using FinLog.Core.Enums;
using System.Collections.Generic;

namespace FinLog.Core.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public AccountType Type { get; set; }
        public decimal CurrentBalance { get; set; }
        
        // Navigation property
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
