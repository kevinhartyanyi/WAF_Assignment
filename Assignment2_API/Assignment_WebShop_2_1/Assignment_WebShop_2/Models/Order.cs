using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String PhoneNumber { get; set; }

        public List<BasketElem> OrderedProducts { get; set; }
        public bool Delivered { get; set; }
    }
}
