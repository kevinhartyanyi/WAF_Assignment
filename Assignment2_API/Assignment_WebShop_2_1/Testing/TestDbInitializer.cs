using Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Testing
{
    public static class TestDbInitializer
    {
        public static void Initialize(WebShopContext context)
        {
            var tabble_lamp = Path.Combine("App_Data", "tabble_lamp.png");
            var simple_lamp = Path.Combine("App_Data", "simple_lamp.png");
            var dark_lamp = Path.Combine("App_Data", "dark_lamp.png");

            var monitor_1 = Path.Combine("App_Data", "monitor_1.png");
            var monitor_2 = Path.Combine("App_Data", "monitor_2.png");

            var fridge = Path.Combine("App_Data", "fridge.png");
            var wash = Path.Combine("App_Data", "wash.png");
            var laptop = Path.Combine("App_Data", "laptop.png");



            var categories = new Category[]
            {
                new Category
                {
                    Name = "Lamps",
                    Products = new List<Product>()
                    {
                        new Product
                        {
                            Manufacturer = "Light2",
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
                            Manufacturer = "Light2",
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
                            Manufacturer = "Dark2",
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
                            Manufacturer = "Dark2",
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
                            Manufacturer = "Dark2",
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
                            ID = 15,
                            // Category? TODO
                            Manufacturer = "Acer2",
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
                            ID = 20,
                            // Category? TODO
                            Manufacturer = "HP2",
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
                            Manufacturer = "Ford2",
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
                            Manufacturer = "Wash2",
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
                            Manufacturer = "Some Company2",
                            Amount = 7,
                            Available = true,
                            CategoryId = 3,
                            ModelID = 0001,
                            Price = 10,
                            Description = "Laptop1",
                            Image = File.Exists(laptop) ? File.ReadAllBytes(laptop) : null
                        }
                    }
                }
            };


            var orders = new Order[]
            {
                new Order
                {
                    Address = "Utca1",
                    Delivered = false,
                    Email = "TesztMail",
                    PhoneNumber = "31975915",
                    UserName = "TesztName",
                    OrderedProducts = new List<BasketElem>
                    {
                        new BasketElem
                        {
                            amount = 2,
                            product = new Product
                            {
                                //ID = 15,
                                Manufacturer = "Acer2",
                                Amount = 3,
                                Available = false,
                                CategoryId = 2,
                                ModelID = 4024,
                                Price = 40,
                                Description = "Kinda works",
                                Image = File.Exists(monitor_1) ? File.ReadAllBytes(monitor_1) : null
                            }
                        },
                        new BasketElem
                        {
                            amount = 1,
                            product = new Product
                            {
                                //ID = 20,
                                Manufacturer = "HP2",
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
                },
                new Order
                {
                    Address = "Utca2",
                    Delivered = true,
                    Email = "TesztMail2",
                    PhoneNumber = "1975135",
                    UserName = "TesztName2",
                    OrderedProducts = new List<BasketElem>
                    {
                        new BasketElem
                        {
                            amount = 5,
                            product = new Product
                            {
                                //ID = 15,
                                Manufacturer = "Acer2",
                                Amount = 3,
                                Available = false,
                                CategoryId = 2,
                                ModelID = 4024,
                                Price = 40,
                                Description = "Kinda works",
                                Image = File.Exists(monitor_1) ? File.ReadAllBytes(monitor_1) : null
                            }
                        }
                    }
                }
            };

            foreach (var cat in categories)
            {
                    context.Categories.Add(cat);
            }
            context.SaveChanges();

            foreach (var ord in orders)
            {
                context.Orders.Add(ord);
            }
            context.SaveChanges();
        }
    }
}
