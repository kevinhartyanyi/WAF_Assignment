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

            //context.Database.EnsureCreated();
            context.Database.Migrate();



            if (context.Categories.Any())
            {

                return;
            }

            var tabble_lamp = Path.Combine(imageDirectory, "tabble_lamp.png");
            var simple_lamp = Path.Combine(imageDirectory, "simple_lamp.png");
            var dark_lamp = Path.Combine(imageDirectory, "dark_lamp.png");

            var monitor_1 = Path.Combine(imageDirectory, "monitor_1.png");
            var monitor_2 = Path.Combine(imageDirectory, "monitor_2.png");


            var categories = new Category[]
            {
                new Category
                {
                    Name = "Lamps",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "Light",
                            Amount = 10,
                            Available = true,
                            CategoryId = 1,
                            ModelID = 3101,
                            Price = 200,
                            Description = "In good shape",
                            Image = File.Exists(tabble_lamp) ? File.ReadAllBytes(tabble_lamp) : null
                        },
                        new Product
                        {
                            Manufacturer = "Light",
                            Amount = 3,
                            Available = true,
                            CategoryId = 1,
                            ModelID = 1024,
                            Price = 50,
                            Description = "Brighter than you think",
                            Image = File.Exists(simple_lamp) ? File.ReadAllBytes(simple_lamp) : null
                        },
                        new Product
                        {
                            Manufacturer = "Dark",
                            Amount = 3,
                            Available = true,
                            CategoryId = 1,
                            ModelID = 1222,
                            Price = 100,
                            Description = "Works in the dark too",
                            Image = File.Exists(dark_lamp) ? File.ReadAllBytes(dark_lamp) : null
                        },
                        new Product
                        {
                            Manufacturer = "Dark",
                            Amount = 5,
                            Available = false,
                            CategoryId = 1,
                            ModelID = 1111,
                            Price = 1111,
                            Description = "Not avalilable",
                            Image = File.Exists(simple_lamp) ? File.ReadAllBytes(simple_lamp) : null
                        },
                        new Product
                        {
                            Manufacturer = "Dark",
                            Amount = 0,
                            Available = true,
                            CategoryId = 1,
                            ModelID = 2222,
                            Price = 2222,
                            Description = "0 amount",
                            Image = File.Exists(simple_lamp) ? File.ReadAllBytes(simple_lamp) : null
                        },
                    }
                },
                new Category
                {
                    Name = "Monitors",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "Acer",
                            Amount = 3,
                            Available = false,
                            CategoryId = 2,
                            ModelID = 4024,
                            Price = 40,
                            Description = "Kinda works",
                            Image = File.Exists(monitor_1) ? File.ReadAllBytes(monitor_1) : null
                        },
                        new Product
                        {
                            Manufacturer = "HP",
                            Amount = 2,
                            Available = true,
                            CategoryId = 2,
                            ModelID = 1424,
                            Price = 80,
                            Description = "Used to work",
                            Image = File.Exists(monitor_2) ? File.ReadAllBytes(monitor_2) : null
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
