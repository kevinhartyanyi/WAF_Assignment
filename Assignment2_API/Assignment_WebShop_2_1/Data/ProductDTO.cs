using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ProductDTO
    {
        public Int32 Id { get; set; }

        public string Manufacturer { get; set; }

        public int ModelID { get; set; }

        public string Description { get; set; }

        public CategoryDTO Category { get; set; }

        public int Price { get; set; }

        public int Amount { get; set; }

        public bool Available { get; set; }

        public byte[] Image { get; set; }
    }
}
