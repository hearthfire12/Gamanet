using Gamanet.Models;
using Gamanet.Models.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Gamanet.Controllers
{
    public class ProductsController : Controller
    {
        public ProductsController()
        {

        }

        DataStorage ds = new DataStorage();
        // GET: Products
        public ActionResult Index(string sortOrder, string companyName,string category)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentFilter = companyName;
            ViewBag.CurrentCategory = category;
            IEnumerable<OutputProduct> result;
            //Select Company
            if (ViewBag.NoCompany != null)
                result = ds.GetListOfProducts();
            else
            {
                switch (String.IsNullOrEmpty(companyName))
                {
                    case true:
                        result = ds.GetListOfProducts();
                        break;
                    default:
                        result = ds.GetListOfProducts(companyName);
                        break;
                }
            }
            //Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    result = result.OrderByDescending(p => p.CompanyName);
                    break;
                default:
                    result = result.OrderBy(p => p.CompanyName);
                    break;
            }
            if (!String.IsNullOrEmpty(category))
            {
                foreach (var item in category.Trim(' ').Split(',')) 
                {
                    result = result.Where(p => p.Category.Contains(item));
                }
            }
            return View(result);
        }

        public ActionResult Details(string productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var prod = ds.GetListOfProducts().ToList().Find(p => p.ProductId == productId);
            if (prod == null)
            {
                return HttpNotFound();
            }
            return View(prod);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OutputProduct product)
        {

            if (ModelState.IsValid)
            {
                ds.InsertProduct(product);

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }
        
        public ActionResult SetUp()
        {
            try
            {
                DataStorage ds = new DataStorage();
                ds.InsertProductInfo(Server.MapPath("~/App_Data/products.csv"));
                ds.InsertReleaseDates(Server.MapPath("~/App_Data/products - release dates.csv"));
                ds.InsertProductsFromJS(Server.MapPath("~/App_Data/drivers.json"));
            }
            catch {
               return new ContentResult() { Content = "Data is already in database or error occured :)" };
            }
            return new RedirectResult("Index");
        }
    }
}