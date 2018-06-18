using ShoppingCart.Commons;
using ShoppingCart.Commons.Models.Objects;
using ShoppingCart.Models.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models.Objects
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductsViewModel
    {
        public List<Product> Products { get; set; }
    }
}