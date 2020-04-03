﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment_WebShop_2.Models;
using Assignment_WebShop_2.Services;
using System.Diagnostics;

namespace Assignment_WebShop_2.Controllers
{
    public class CategoriesController : BaseController
    {

        public CategoriesController(AccountService accountService, WebShopServices service)
            : base(accountService, service)
        {
        }

        public IActionResult AddProductToBasket(int id)
        {
            _service.AddToBasket(id, _accountService.CurrentUserName);
            //return Redirect(Request.Path);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromBasket(int id)
        {
            _service.RemoveFromBasket(id, _accountService.CurrentUserName);
            return RedirectToAction("Index");
        }

        public IActionResult EmptyBasket()
        {
            _service.EmptyBasket(_accountService.CurrentUserName);
            //return Redirect(Request.Path);
            return RedirectToAction("Index");
        }       

        // GET: Categories
        public IActionResult Index()
        {
            return View(_service.GetCategories());
        }

        public IActionResult BasketView()
        {
            return View(_service.GetBasketForUser(_accountService.CurrentUserName));
        }

        public IActionResult DisplayImage(int id)
        {
            var item = _service.GetProduct(id);
            return File(item.Image, "image/png");
        }

        public IActionResult DisplayRandomImage(string categoryName)
        {
            var item = _service.GetRandomProduct(categoryName);
            return File(item.Image, "image/png");
        }

        // GET: Categories/Details/5
        public IActionResult Details(int id, string sortOrder = "", int page = 1)
        {
            var list = _service.GetCategoryByID(id);

            int skip = (page - 1) * 20;
            int take = 20;

            if (list == null)
            {
                return NotFound();
            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParam"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            var tmp = list.Products.Skip(skip).Take(take);

            switch (sortOrder)
            {
                case "price_asc":
                    list.Products = tmp.OrderBy(i => i.Price).ToList();
                    break;
                case "price_desc":
                    list.Products = tmp.OrderByDescending(i => i.Price).ToList();
                    break;
                case "name_desc":
                    list.Products = tmp.OrderByDescending(i => i.Manufacturer).ToList();
                    break;
                default:
                    list.Products = tmp.OrderBy(i => i.Manufacturer).ToList();
                    break;
            }

            

            return View(list);
        }
    }
}
