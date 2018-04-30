using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gamanet.Models
{
    public class OutputProduct
    {
        [Key]
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public string CompanyName { get; set; }

        public string Version { get; set; }

        public int Size { get; set; }

        public string ReleasedOn { get; set; }

        public string Url { get; set; }

        public string VendorContact { get; set; }

        public string Category { get; set; }
    }
}