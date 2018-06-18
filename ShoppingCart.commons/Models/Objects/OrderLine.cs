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
    public class OrderLine
    {
        public int OrderId { get; set; }
        public Item Item { get; set; }
    }
}
