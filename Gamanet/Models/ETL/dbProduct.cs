using System.ComponentModel.DataAnnotations;

namespace Gamanet.Models.ETL
{
    public class dbProduct
    {
        [Key]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Url { get; set; }
        public string VendorContact { get; set; }

//        public dbProductInfo ProductInfo { get; set; }
    }
}