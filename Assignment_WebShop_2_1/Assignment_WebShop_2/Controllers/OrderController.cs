using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WebShop_2.Models;
using Assignment_WebShop_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_WebShop_2.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController(AccountService accountService, WebShopServices service)
            : base(accountService, service)
        {
        }

        [HttpGet]
        public IActionResult Buy()
        {
            // létrehozunk egy foglalást csak az alapadatokkal (apartman, dátumok)
            BasketOrder order = _service.NewOrder();

            return View("Buy", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // védelem XSRF támadás ellen
        public IActionResult Buy(BasketOrder order)
        {
            if (!ModelState.IsValid)
                return View("Buy", order);

            String userName = _accountService.CurrentUserName;

            _service.SaveOrder(userName, order);

            // kiszámoljuk a teljes árat
            order.TotalPrice = _service.GetPrice(userName);
            
            _service.EmptyBasket(userName);

            return View("Result", order);
        }
    }
}