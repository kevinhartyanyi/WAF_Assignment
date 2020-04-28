using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Basket
    {
        public Basket()
        {
            elems = new List<BasketElem>();
        }

        [Key]
        public int ID { get; set; }

        public string UserName { get; set; }

        public ICollection<BasketElem> elems { get; set; }
    }
}
