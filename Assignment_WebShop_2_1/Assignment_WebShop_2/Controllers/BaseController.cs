using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            //if (_accountService.CurrentUserName != null)
            //    ViewBag.CurrentGuestName = _accountService.GetGuest(_accountService.CurrentUserName).Name;
        }
    }
}