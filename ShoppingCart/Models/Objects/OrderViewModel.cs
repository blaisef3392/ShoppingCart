using ShoppingCart.Commons;
using ShoppingCart.Commons.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models.Objects
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }

    public class OrderFormViewModel
    {
        public Order Order { get; set; }
        public Cart Cart { get; set; }
    }

    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
    }
}