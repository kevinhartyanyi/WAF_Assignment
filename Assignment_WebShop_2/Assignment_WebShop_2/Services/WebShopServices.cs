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
