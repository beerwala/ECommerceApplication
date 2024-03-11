using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.shared.Models
{
    
        public class ImageModel
        {
            public string Base64 { get; set; }
            public string ImageUrl { get; set; }
            public string ThumbImageUrl { get; set; }
            public string SignatureUrl { get; set; }
        }
    
}
