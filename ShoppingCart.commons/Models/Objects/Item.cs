using ShoppingCart.Commons.Models.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.commons.Models.Objects
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Sub Total")]
        [DataType(DataType.Currency)]
        public double Total
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
    }
}
