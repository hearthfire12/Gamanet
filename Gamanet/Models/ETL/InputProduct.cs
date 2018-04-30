using System.ComponentModel.DataAnnotations;

namespace Gamanet.Models.ETL
{
    public class InputProduct
    {
        public string DriverID { get; set; }
        public InputProductProperties Properties { get; set; }
    }
}