using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Commons.Models.Objects
{
    public class Order
    {
        [Key]
        public int Id { get;  set; }
        public int AccountId { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get;  set; }

        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get;  set; }

        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get;  set; }

        [Display(Name = "Delivery Recipient")]
        public string DeliveryRecipient { get;  set; }

        [Display(Name = "Payable")]
        [DataType(DataType.Currency)]
        public double Total { get; set; }
    }
}
