using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login() //TODO: Accept ID parameter
        {
            if (Validation()) //admin
                return RedirectToAction("Index", "Admin");
            return Content("NONE!");
        }

        private Boolean Validation() //TOD: Validation
        {
            return true;
        }

    }
}