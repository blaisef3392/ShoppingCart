﻿using ShoppingCart.Commons;
using ShoppingCart.Models.Objects;
using ShoppingCart.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        public int counter = 0;
        
        public ActionResult ViewProducts(int typeId, int categoryId)
        {
            var model = new ProductsViewModel
            {
                Products = ShoppingCartCSV.GetProducts().Where(x => x.Type.Id.Equals(typeId) && x.Category.Id.Equals(categoryId)).ToList()
            };

            return View(model);
        }

        public ActionResult ViewProduct(int id)
        {
            var Model = new ProductDetailViewModel
            {
                Product = ShoppingCartCSV.GetProduct(id)
            };

            return View(Model);
        }
    }
}