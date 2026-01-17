using FinLog.Core.Enums;
using System.Collections.Generic;

namespace FinLog.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public string? ColorCode { get; set; } // Mã màu Hex cho UI
        
        // Navigation property
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
