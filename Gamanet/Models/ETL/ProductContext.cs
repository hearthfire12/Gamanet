using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gamanet.Models.ETL
{
    public class ProductContext : DbContext
    {
        public DbSet<dbCompany> Companies { get; set; }
        public DbSet<dbProduct> Products { get; set; }
        public DbSet<dbProductInfo> ProdutsInfo { get; set; }

        public ProductContext()
        {

        }

        public ProductContext(string connection) : base(connection)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProductContext>());
        }

        public System.Data.Entity.DbSet<Gamanet.Models.OutputProduct> OutputProducts { get; set; }
    }
}