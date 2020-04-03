using Assignment_WebShop_2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Basket
    {
        public Basket()
        {
            elems = new List<BasketElem>();            
        }

        [Key]
        public Int32 ID { get; set; }

        public string UserName { get; set; }

        public ICollection<BasketElem> elems { get; set; }
    }
}
