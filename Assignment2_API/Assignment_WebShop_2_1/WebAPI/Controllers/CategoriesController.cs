using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Data.Services;
using Data;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriesController : ControllerBase
    {
        protected readonly WebShopServices _service;

        public CategoriesController(WebShopServices service)
        {
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
