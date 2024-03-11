using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class MasterCard
    {
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int CVV { get; set; }
    }
}
