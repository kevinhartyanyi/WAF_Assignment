using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WebShop_2.Models;
using Assignment_WebShop_2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Assignment_WebShop_2.Controllers
{
    public class BaseController : Controller
    {
        // a logikát modell osztályok mögé rejtjük
        protected readonly AccountService _accountService;
        protected readonly WebShopServices _service;

        public BaseController(AccountService accountService, WebShopServices service)
        {
            _accountService = accountService;
            _service = service;
        }

        /// <summary>
        /// Egy akció meghívása után végrehajtandó metódus.
        /// </summary>
        /// <param name="context">Az akció kontextus argumentuma.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            // a minden oldalról elérhető információkat össze gyűjtjük

            if (_accountService.CurrentUserName != null)
            {
                ViewBag.CurrentName = _accountService.CurrentUserName;
                List<BasketElem> elems = _service.GetBasketForUser(_accountService.CurrentUserName).elems.ToList();
                float price = 0;
                foreach (var item in elems)
                {
                    if (item.product != null)
                    {
                        price += item.product.Price * item.amount;
                    }
                }
                ViewBag.BasePrice = price;
                ViewBag.Price = price * 1.27;
            }
            else
            {
                ViewBag.CurrentName = "Nothing";
            }

        }
    }
}