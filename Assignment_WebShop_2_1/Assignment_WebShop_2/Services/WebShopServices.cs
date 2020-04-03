using Assignment_WebShop_2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_WebShop_2.Services
{
    public class WebShopServices
    {
        WebShopContext context;
        Random random;

     

        public WebShopServices(WebShopContext Context)
        {
            context = Context;
            random = new Random();
        }

        public Basket GetBasketForUser(string userName)
        {
            return context.Baskets
                .Include(b => b.elems)
                    .ThenInclude(elems => elems.product)
                .FirstOrDefault(g => g.UserName == userName);
        }

        public void RemoveFromBasket(int productId, string userName)
        {
            var basket = GetBasketForUser(userName);

            if(basket.elems.FirstOrDefault(x => x.product.ID == productId).amount > 0)
            {
                basket.elems.FirstOrDefault(x => x.product.ID == productId).amount -= 1;
            }

            context.SaveChanges();
        }

        public void EmptyBasket(string userName)
        {
            var basket = GetBasketForUser(userName);

            basket.elems.Clear();

            context.SaveChanges();
        }

        public void AddToBasket(int productId, string userName)
        {
            Product p = context.Products
                .FirstOrDefault(i => i.ID == productId);
            
            var basket = GetBasketForUser(userName);
            
            var t = basket.elems.Where(i => i.product != null && i.product.ID == productId);
            if(t.Count() == 0)
            {
                BasketElem elem = new BasketElem();
                elem.product = p;
                elem.amount = 1;
                basket.elems.Add(elem);
                
            }
            else
            {
                basket.elems.FirstOrDefault(x => x.product.ID == productId).amount += 1;
            }

            context.SaveChanges();
        }

        public BasketOrder NewOrder()
        {
            var order = new BasketOrder();

            return order;
        }

        public void SaveOrder(string userName, BasketOrder bOrder)
        {
            context.Orders.Add(new Order
            {
                UserName = bOrder.Name,
                Email = bOrder.Email,
                Address = bOrder.Address,
                PhoneNumber = bOrder.PhoneNumber
            });

            context.SaveChanges();            
        }

        public double GetPrice(string userName)
        {
            List<BasketElem> elems = GetBasketForUser(userName).elems.ToList();
            return elems.Where(x => x.amount > 0).Sum(x => x.amount * x.product.Price) * 1.27;
        }

        public void AddProductToCategory(Product p)
        {
            if (p != null)
            {
                context.Products.Add(p);
            }
            else
            {
                throw new ArgumentNullException(nameof(p), "The item to add must not be null.");
            }

            context.SaveChanges();
        }

        public List<Category> GetCategories(string contain = "")
        {
            return context.Categories
                .Where(x => x.Name.Contains(contain))
                .OrderBy(x => x.Name)
                .ToList();
        }

        public Category GetCategoryByID(Int32 id)
        {
            return context.Categories
                .Include(x => x.Products)
                .Single(x => x.ID == id);
        }

        public Category GetCategoryByName(string categoryName)
        {
            
            return context.Categories
                .Include(x => x.Products)
                .Single(x => x.Name == categoryName);
        }

        public Product GetProduct(int id)
        {
            return context.Products
                .Include(x => x.Category)
                .Single(x => x.ID == id);
        }

        public Product GetRandomProduct(string categoryName)
        {
            var category = GetCategoryByName(categoryName);
            int ind = random.Next(category.Products.Count());
            return category.Products.Skip(ind).First();
        }
    }
}
