using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class BasketElem
    {
        [Key]
        public Int32 ID { get; set; }
        public Product product { get; set; }
        public int amount { get; set; }
    }
}
