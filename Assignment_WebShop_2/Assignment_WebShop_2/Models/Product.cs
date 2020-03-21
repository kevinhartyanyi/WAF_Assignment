using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class Product
    {
        [Key]
        public Int32 ID { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public int ModelID { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Category")]
        public Int32 CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
