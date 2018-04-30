using System.Collections.Generic;
using System.Linq;

namespace Gamanet.Models.ETL
{
    public class DataStorage
    {
        private ProductContext context { get; set; }

        public DataStorage(ProductContext context) : base()
        {
            this.context = context;
        }
        public DataStorage() : base()
        {
            context = new ProductContext("MSSqlLocalDB");
        }


        public void InsertReleaseDates(string path)
        {
            var cvs = CSVReader.GetListOfData(path);
            foreach (var item in cvs)
                context.ProdutsInfo.Add(new dbProductInfo() { ProductId = item[0], ReleasedOn = item[1] });
            context.SaveChanges();
        }
        public void InsertProductInfo(string path)
        {
            var cvs = CSVReader.GetListOfData(path);
            foreach (var item in cvs)
            {
                context.Products.Add(new dbProduct()
                {
                    ProductId = item[0],
                    ProductName = item[1],
                    Url = item[2],
                    VendorContact = item[3]

                });
            }
            context.SaveChanges();
        }
        public void InsertProductsFromJS(string path)
        {
            var InputProducts = JsonSerializer.GetListOfProducts(path);

            foreach (var product in InputProducts)
            {
                try
                {
                    var dbProduct = context.Products.Where(p => p.ProductName == product.Properties.ProductName).FirstOrDefault();
                    if (dbProduct != null)
                    {
                        context.Companies.Add(new dbCompany()
                        {
                            CompanyName = product.Properties.CompanyName,
                            ProductId = dbProduct.ProductId
                        });

                        var dbReleaseDate = context.ProdutsInfo.Where(p => p.ProductId == dbProduct.ProductId).FirstOrDefault();
                        if (dbReleaseDate != null)
                        {
                            dbReleaseDate.Size = product.Properties.Size;
                            dbReleaseDate.ProductCategory = product.Properties.ProductCategory;
                            dbReleaseDate.Version = product.Properties.Version;
                        }
                    }
                    context.SaveChanges();
                }
                catch { }
            }
        }
        public void InsertProduct(OutputProduct product) {
            try
            {
                var dbProduct = context.Products.Where(p => p.ProductName == product.ProductName).FirstOrDefault();
                if (dbProduct != null)
                {
                    context.Companies.Add(new dbCompany()
                    {
                        CompanyName = product.CompanyName,
                        ProductId = dbProduct.ProductId
                    });

                    var dbReleaseDate = context.ProdutsInfo.Where(p => p.ProductId == dbProduct.ProductId).FirstOrDefault();
                    if (dbReleaseDate != null)
                    {
                        dbReleaseDate.Size = product.Size;
                        dbReleaseDate.ProductCategory = product.Category;
                        dbReleaseDate.Version = product.Version;
                    }
                }
                else
                {
                    context.Companies.Add(new dbCompany()
                    {
                        CompanyName = product.CompanyName,
                        ProductId = product.ProductId

                    });
                    context.Products.Add(new dbProduct()
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Url = product.Url,
                        VendorContact = product.VendorContact
                    });
                    context.ProdutsInfo.Add(new dbProductInfo()
                    {
                        ProductId = product.ProductId,
                        ProductCategory = product.Category,
                        Size = product.Size,
                        Version = product.Version,
                        ReleasedOn = product.ReleasedOn
                    });
                }
                context.SaveChanges();
            }
            catch { }
        }

        public IEnumerable<OutputProduct> GetListOfProducts()
        {
            var dbProducts = (from p in context.Products.AsEnumerable()
                              orderby p.ProductId
                             join c in context.Companies.AsEnumerable() on p.ProductId equals c.ProductId
                             join pi in context.ProdutsInfo.AsEnumerable() on c.ProductId equals pi.ProductId
                              select new OutputProduct
                             {
                                  ProductId=p.ProductId,
                                 ProductName = p.ProductName,
                                 CompanyName = c.CompanyName,
                                 Version = pi.Version,
                                 Size = pi.Size,
                                 ReleasedOn = pi.ReleasedOn,
                                 Url = p.Url,
                                 VendorContact = p.VendorContact,
                                 Category = pi.ProductCategory
                             }).OrderBy(p=>p.ProductName);
            return dbProducts;
        }
        public IEnumerable<OutputProduct> GetListOfProducts(string companyName)
        {
            var dbProducts = (from p in context.Products.AsEnumerable()
                              orderby p.ProductId
                              join c in context.Companies.AsEnumerable() on p.ProductId equals c.ProductId
                              join pi in context.ProdutsInfo.AsEnumerable() on c.ProductId equals pi.ProductId
                              where c.CompanyName== companyName
                              select new OutputProduct
                              {
                                  ProductId = p.ProductId,
                                  ProductName = p.ProductName,
                                  CompanyName = c.CompanyName,
                                  Version = pi.Version,
                                  Size = pi.Size,
                                  ReleasedOn = pi.ReleasedOn,
                                  Url = p.Url,
                                  VendorContact = p.VendorContact,
                                  Category = pi.ProductCategory
                              }).OrderBy(p => p.ProductName); ;
            return dbProducts;
        }
    }
}