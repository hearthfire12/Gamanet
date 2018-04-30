using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Gamanet.Models.ETL
{
    class JsonSerializer
    {
        public static IEnumerable<InputProduct> GetListOfProducts(string path)
        {
            //Json to String
            string json = new StreamReader(File.Open(path, FileMode.Open, FileAccess.Read)).ReadToEnd();
            //String to Product List             
            return new JavaScriptSerializer().Deserialize(json, typeof(List<InputProduct>)) as List<InputProduct>;
        }
    }
}