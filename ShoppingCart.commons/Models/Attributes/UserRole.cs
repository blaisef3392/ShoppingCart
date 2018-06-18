using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Commons.Models.Attributes
{
    class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
