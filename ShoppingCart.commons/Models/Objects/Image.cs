using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.commons.Models.Objects
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string FilePath { get; set; }
    }
}
