using ShoppingCart.Commons.Models.Attributes;
using ShoppingCart.Commons.Models.Objects;
using ShoppingCart.Models.Objects;
using ShoppingCart.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            Session["accountId"] = 123; //temporary TODO: replace with retrieve from csv
            try
            {
                List<Product> products = ShoppingCartCSV.GetProducts();
                List<ProductCategory> productCategories = ShoppingCartCSV.GetProductCategories().OrderBy(x => x.Name).ToList();
                List<ProductType> productTypes = ShoppingCartCSV.GetProductTypes().OrderBy(x => x.Name).ToList();
                model.ProductCategories = productCategories;
                model.ProductTypes = productTypes;
                model.Products = products;

            }
            catch (Exception ex)
            {
                model.HasError = true;
                model.Errormessage = ex.Message;
                //throw;
            }
           
            return View(model);
            //return RedirectToAction("ViewProducts", "Product", null);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}