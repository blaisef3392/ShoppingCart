using ShoppingCart.Commons.Models.Attributes;
using ShoppingCart.Commons.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models.Objects
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductType> ProductTypes { get; set; }

        public bool HasError { get; set; }
        public string Errormessage { get; set; }
    }
}