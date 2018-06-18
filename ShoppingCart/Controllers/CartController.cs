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
    public class CartController : Controller
    {
        Cart cart;
        
        [HttpPost]
        public ActionResult AddToCart(ProductDetailViewModel ProductDetailViewModel)
        {
            if (ProductDetailViewModel.Quantity < 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                var product = ShoppingCartCSV.GetProduct(ProductDetailViewModel.Product.Id);
                var quantity = ProductDetailViewModel.Quantity;

                Item item = new Item
                {
                    Product = product,
                    Quantity = quantity
                };

                this.SetSession("Cart");

                var v = cart.Items.FirstOrDefault(x => x.Product.Id.Equals(product.Id));

                if (v == null)
                {
                    cart.Items.Add(item);
                }
                else
                {
                    v.Quantity += quantity;
                }


                Session.Add("Cart", cart);

                return RedirectToAction("ViewCart");
            }
        }
        

        public ActionResult ViewCart()
        {
            this.SetSession("Cart");
            
            var model = new CartViewModel()
            {
                Cart = cart
            };

            return View(model);
        }

        public ActionResult RemoveItem(int index)
        {
            this.SetSession("Cart");

            cart.Items.RemoveAt(index);

            return RedirectToAction("ViewCart");
        }

        public ActionResult SubtractProduct(int index)
        {
            this.SetSession("Cart");

            cart.Items[index].Quantity -= 1;

            if(cart.Items[index].Quantity == 0)
                return RedirectToAction("RemoveItem", new { index });

            return RedirectToAction("ViewCart");
        }

        public ActionResult AddProduct(int index)
        {
            this.SetSession("Cart");

            cart.Items[index].Quantity += 1;

            return RedirectToAction("ViewCart");
        }
        
        private void SetSession(string key)
        {
            cart = Session[key] as Cart ?? new Cart();
        }

    }
}