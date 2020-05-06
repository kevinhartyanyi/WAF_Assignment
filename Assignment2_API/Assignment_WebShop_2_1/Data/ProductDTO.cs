using Data.Models;
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

        public static explicit operator Product(ProductDTO dto) => new Product
        {
            ID = dto.Id,
            Manufacturer = dto.Manufacturer,
            Amount = dto.Amount,
            Available = dto.Available,
            Price = dto.Price,
            Description = dto.Description,
            ModelID = dto.ModelID, 
            Category = new Category { ID = dto.Category.Id, Name = dto.Category.Name},
            Image = dto.Image
        };

        public static explicit operator ProductDTO(Product i) => new ProductDTO
        {
            Id = i.ID,
            Manufacturer = i.Manufacturer,
            Amount = i.Amount,
            Available = i.Available,
            Price = i.Price,
            Description = i.Description,
            ModelID = i.ModelID,
            Category = new CategoryDTO { Id = i.Category.ID, Name = i.Category.Name },
            Image = i.Image
        };
    }
}
