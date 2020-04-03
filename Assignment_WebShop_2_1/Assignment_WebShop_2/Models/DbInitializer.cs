using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

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

            var fridge = Path.Combine(imageDirectory, "fridge.png");
            var wash = Path.Combine(imageDirectory, "wash.png");
            var laptop = Path.Combine(imageDirectory, "laptop.png");



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
                            ModelID = 1324,
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
                },
                new Category
                {
                    Name = "Other",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "Ford",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 1521,
                            Price = 300,
                            Description = "Nice",
                            Image = File.Exists(fridge) ? File.ReadAllBytes(fridge) : null
                        },
                        new Product
                        {
                            Manufacturer = "Wash",
                            Amount = 31,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 5135,
                            Price = 250,
                            Description = "Good",
                            Image = File.Exists(wash) ? File.ReadAllBytes(wash) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0001,
                            Price = 10,
                            Description = "Laptop1",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0002,
                            Price = 20,
                            Description = "Laptop2",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0003,
                            Price = 30,
                            Description = "Laptop3",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0004,
                            Price = 10,
                            Description = "Laptop4",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0005,
                            Price = 15,
                            Description = "Laptop5",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0006,
                            Price = 100,
                            Description = "Laptop6",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0007,
                            Price = 110,
                            Description = "Laptop7",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0008,
                            Price = 108,
                            Description = "Laptop8",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0009,
                            Price = 109,
                            Description = "Laptop9",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0010,
                            Price = 1010,
                            Description = "Laptop10",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0011,
                            Price = 1011,
                            Description = "Laptop11",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0012,
                            Price = 130,
                            Description = "Laptop12",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0013,
                            Price = 13,
                            Description = "Laptop13",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0014,
                            Price = 1020,
                            Description = "Laptop14",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0015,
                            Price = 181,
                            Description = "Laptop15",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0016,
                            Price = 106,
                            Description = "Laptop16",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0017,
                            Price = 107,
                            Description = "Laptop17",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0018,
                            Price = 150,
                            Description = "Laptop18",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0019,
                            Price = 109,
                            Description = "Laptop19",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0020,
                            Price = 100,
                            Description = "Laptop20",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0021,
                            Price = 10,
                            Description = "Lapto21",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0022,
                            Price = 10,
                            Description = "Laptop22",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0023,
                            Price = 1023,
                            Description = "Laptop23",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0024,
                            Price = 102,
                            Description = "Laptop24",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0025,
                            Price = 105,
                            Description = "Laptop25",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        },
                        new Product
                        {
                            Manufacturer = "Some Company",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0026,
                            Price = 102,
                            Description = "Laptop26",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        }
                    }
                }
            };


            foreach (var cat in categories)
            {
                if(!context.Categories.Contains(cat))
                {
                    context.Categories.Add(cat);

                }
            }
            context.SaveChanges();
        }
    }
}
