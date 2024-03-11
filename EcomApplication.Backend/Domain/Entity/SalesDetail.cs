using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SalesDetail
    {
        public int Id { get; set; }
        public int SalesMasterId { get; set; }
        public SalesMaster SalesMaster { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductCode { get; set; }
        public int SaleQty { get; set; }
        public int NewStockQty { get; set; }
        public float SellingPrice { get; set; }
    }
}
