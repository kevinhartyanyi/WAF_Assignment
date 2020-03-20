using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment_WebShop_2.Models;
using Assignment_WebShop_2.Services;

namespace Assignment_WebShop_2.Controllers
{
    public class CategoriesController : Controller
    {
        //private readonly WebShopContext _context;
        private readonly WebShopServices _service;

        public CategoriesController(WebShopServices context)
        {
            _service = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_service.GetCategories());
        }

        public IActionResult DisplayRandomImage()
        {
            var item = _service.GetRandomProduct();
            return File(item.Image, "image/png");
        }

        // GET: Categories/Details/5
        public IActionResult Details(int id, string sortOrder = "")
        {
            var list = _service.GetCategoryByID(id);
            if (list == null)
            {
                return NotFound();
            }


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DeadlineSortParm"] = sortOrder == "deadline_asc" ? "deadline_desc" : "deadline_asc";

            switch (sortOrder)
            {
                case "deadline_asc":
                    list.Products = list.Products.OrderBy(i => i.Price).ToList();
                    break;
                case "deadline_desc":
                    list.Products = list.Products.OrderByDescending(i => i.Price).ToList();
                    break;
                case "name_desc":
                    list.Products = list.Products.OrderByDescending(i => i.Manufacturer).ToList();
                    break;
                default:
                    list.Products = list.Products.OrderBy(i => i.Manufacturer).ToList();
                    break;
            }


            return View(list);
        }
    }
}
