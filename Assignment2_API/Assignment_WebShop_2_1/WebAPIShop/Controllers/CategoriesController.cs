using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WebShop_2.Services;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriesController : ControllerBase
    {
        protected readonly AccountService _accountService;
        protected readonly WebShopServices _service;

        public CategoriesController(AccountService accountService, WebShopServices service)
        {
            _accountService = accountService;
            _service = service;
        }


        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(_service.GetCategories().Select(cat => new CategoryDTO
                {
                    Id = cat.ID,
                    Name = cat.Name
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }       
    }
}
