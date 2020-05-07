using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<BasketElemDTO> OrderedProducts { get; set; }
        public bool Delivered { get; set; }        
    }
}
