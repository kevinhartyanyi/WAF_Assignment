using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class BasketElem
    {
        [Key]
        public int ID { get; set; }
        public Product product { get; set; }
        public int amount { get; set; }
    }
}
