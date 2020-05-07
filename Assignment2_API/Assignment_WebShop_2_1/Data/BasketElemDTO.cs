using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class BasketElemDTO
    {
        public Int32 Id { get; set; }

        public ProductDTO product { get; set; }
        public int amount { get; set; }
    }
}
