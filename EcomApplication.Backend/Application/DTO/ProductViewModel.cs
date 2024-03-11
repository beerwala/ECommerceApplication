﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; } 
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
