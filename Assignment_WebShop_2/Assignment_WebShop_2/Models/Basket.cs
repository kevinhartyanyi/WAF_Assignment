using Assignment_WebShop_2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Basket
    {

        public Basket()
        {
            elems = new List<Product>();
        }

        public ICollection<Product> elems { get; set; }


        public void Add(Product p)
        {
            elems.Add(p);
        }
    }
}
