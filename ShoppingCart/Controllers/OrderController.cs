using ShoppingCart.commons.Models.Objects;
using ShoppingCart.Commons;
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
    public class OrderController : Controller
    {
        static DateTime orderDate = DateTime.Today; 
        static DateTime deliveryDate = DateTime.Today.AddDays(30); //default
        
        public ActionResult ViewOrders()
        {
            var model = new OrdersViewModel
            {
                Orders = ShoppingCartCSV.GetOrders().Where(x => x.AccountId.Equals(123)).ToList()
            };

            return View(model);
        }

        public ActionResult ViewOrder(int id)
        {
            Order order = ShoppingCartCSV.GetOrders().Where(x => x.Id.Equals(id)).FirstOrDefault();
            List<OrderLine> orderLines = ShoppingCartCSV.GetOrderLines().Where(x => x.OrderId.Equals(order.Id)).ToList();
            
            var model = new OrderDetailViewModel
            {
                Order = order,
                OrderLines = orderLines
            };

            return View(model);
        }

        public ActionResult DisplayForm()
        {
            Cart cart = Session["Cart"] as Cart;

            Order order = new Order
            {
                OrderDate = orderDate,
                DeliveryDate = deliveryDate,
                DeliveryAddress = "",
                DeliveryRecipient = ""
            };

            var model = new OrderFormViewModel
            {
                Order = order,
                Cart = cart
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderFormViewModel orderFormViewModel)
        {
            int accountId = Convert.ToInt32(Session["accountId"]);
            Order order = orderFormViewModel.Order;
            Cart cart = Session["Cart"] as Cart;
            order.Total = cart.Total;

            ShoppingCartCSV.AddOrder(accountId, order, cart);
            Session.Remove("Cart");

            return RedirectToAction("ViewOrders");
        }

        [HttpPost]
        public ActionResult CancelOrder(OrderDetailViewModel OrderDetailViewModel)
        {
            int accountId = Convert.ToInt32(Session["accountId"]);

            ShoppingCartCSV.CancelOrder(accountId, OrderDetailViewModel.Order.Id);

            return RedirectToAction("ViewOrders");
        }
    }
}