using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Models
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider, string imageDirectory)
        {
            WebShopContext context = serviceProvider.GetRequiredService<WebShopContext>();

            context.Database.Migrate();



            if (context.Categories.Any())
            {

                return;
            }


            var applePath = Path.Combine(imageDirectory, "apple.png");
            var pearPath = Path.Combine(imageDirectory, "pearl.png");


            var categories = new Category[]
            {
                new Category
                {
                    Name = "Lamps",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "DEL",
                            Amount = 10,
                            Available = true,
                            CategoryId = 2,
                            ModelID = 3101,
                            Price = 200,
                            Description = "In good shape",
                            Image = File.Exists(pearPath) ? File.ReadAllBytes(pearPath) : null
                        },
                        new Product
                        {
                            Manufacturer = "Samsung",
                            Amount = 3,
                            Available = true,
                            CategoryId = 1,
                            ModelID = 1024,
                            Price = 50,
                            Description = "Lightness",
                            Image = File.Exists(applePath) ? File.ReadAllBytes(applePath) : null
                        }
                    }
                },
                new Category
                {
                    Name = "Monitors",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "PLAT",
                            Amount = 3,
                            Available = false,
                            CategoryId = 2,
                            ModelID = 4024,
                            Price = 40,
                            Description = "There's no light without darkness",
                            Image = File.Exists(pearPath) ? File.ReadAllBytes(pearPath) : null
                        }
                    }
                }
            };


            foreach (var cat in categories)
            {
                context.Categories.Add(cat);
            }
            context.SaveChanges();
        }
    }
}
