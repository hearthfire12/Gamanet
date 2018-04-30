using System.ComponentModel.DataAnnotations;

namespace Gamanet.Models.ETL
{
    public class dbProductInfo
    {
        [Key]
        public string ProductId { get; set; }
        public int Size { get; set; }
        public string ProductCategory { get; set; }
        public string Version { get; set; }
        public string ReleasedOn { get; set; }

        public dbProduct Product { get; set; }
    }
}