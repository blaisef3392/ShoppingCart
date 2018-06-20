using ShoppingCart.commons.Models;
using ShoppingCart.commons.Models.Objects;
using ShoppingCart.Commons;
using ShoppingCart.Models.Objects;
using ShoppingCart.Models.Service;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        
        [HttpPost]
        public ActionResult AddToCart(ProductDetailViewModel ProductDetailViewModel)
        {
            var cart = new Cart();
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

                this.SetSession(Constants.CART_SESSION_KEY);

                var v = cart.Items.FirstOrDefault(x => x.Product.Id.Equals(product.Id));

                if (v == null)
                {
                    cart.Items.Add(item);
                }
                else
                {
                    v.Quantity += quantity;
                }


                Session.Add(Constants.CART_SESSION_KEY, cart);

                return RedirectToAction("ViewCart");
            }
        }
        

        public ActionResult ViewCart()
        {
            this.SetSession("Cart");
            var cart = new Cart();
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