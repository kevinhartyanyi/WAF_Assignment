using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Category
    {
        [Key]
        public Int32 ID { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
