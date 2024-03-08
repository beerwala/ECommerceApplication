using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Product : BaseEntity<int>, IEntity
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; } // Image path or URL
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime SellingDate { get; set; }
        public int Stock { get; set; }

    }
}
