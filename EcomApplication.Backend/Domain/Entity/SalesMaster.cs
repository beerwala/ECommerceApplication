using Domain.Model;
using Domain.Model.CascadingData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SalesMaster
    {
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime InvoiceDate { get; set; }
        public float SubTotal { get; set; }
        public string DeliveryAddress { get; set; }
        public int DeliveryZipcode { get; set; }
        public int DeliveryStateId { get; set; }
        public int DeliveryCountryId { get; set; }
        public State DeliveryState { get; set; }
        public Country DeliveryCountry { get; set; }
    }
}
