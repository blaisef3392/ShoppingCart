using ShoppingCart.commons.Models.Objects;
using ShoppingCart.Commons.Models.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Commons
{
    public class Cart
    {
        [Key]
        public List<Item> Items { get; set; }

        [Display(Name = "Payable")]
        [DataType(DataType.Currency)]
        public double Total
        {
            get
            {
                return Calculate();
            }
        } 
        
        public Cart()
        {
            Items = new List<Item>();
        }

        private double Calculate()
        {
            Double total = 0;

            foreach (var item in Items)
            {
                total += item.Total;
            }

            return total;
        }
    }
}
