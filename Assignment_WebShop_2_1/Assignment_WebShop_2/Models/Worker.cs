using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Worker
    {
        [Key]
        public Int32 ID { get; set; }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
