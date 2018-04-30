using System.ComponentModel.DataAnnotations;

namespace Gamanet.Models.ETL
{
    public class InputProductProperties
    {
        public string CompanyName { get; set; }

        public string Copyright { get; set; }

        public string ProductCategory { get; set; }

        public string ProductDescription { get; set; }

        public string ProductName { get; set; }

        public string ProjectUrl { get; set; }

        public int Size { get; set; }

        public string Url { get; set; }

        public string Version { get; set; }

        public string VendorContact { get; set; }

        public string ReleasedOn { get; set; }
    }
}