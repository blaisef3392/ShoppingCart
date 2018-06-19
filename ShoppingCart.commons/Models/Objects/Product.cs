using ShoppingCart.commons.Models.Objects;
using ShoppingCart.Commons.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Commons.Models.Objects
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string Model { get; set; }

        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public ProductType Type { get; set; }
        public Image Image { get; set; }
        public string Description { get; set; }
    }
}
