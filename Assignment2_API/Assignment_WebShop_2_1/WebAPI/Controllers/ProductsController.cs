﻿using System;
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
    public class ProductsController : ControllerBase
    {
        protected readonly WebShopServices _service;

        public ProductsController(WebShopServices service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                return Ok(_service.GetAllProducts().ToList().Select(pro => new ProductDTO
                {
                    Id = pro.ID,
                    Manufacturer = pro.Manufacturer,
                    Amount = pro.Amount,
                    Available = pro.Available,
                    Price = pro.Price,
                    Description = pro.Description,
                    ModelID = pro.ModelID, // TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
                    Category = new CategoryDTO { Id = pro.CategoryId, Name = pro.Category.Name},
                    Image = pro.Image
                }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var pro = _service.GetProduct(id);

                return Ok(new ProductDTO
                {
                    Id = pro.ID,
                    Manufacturer = pro.Manufacturer,
                    Amount = pro.Amount,
                    Available = pro.Available,
                    Price = pro.Price,
                    Description = pro.Description,
                    ModelID = pro.ModelID, // TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
                    Category = new CategoryDTO { Id = pro.CategoryId, Name = pro.Category.Name },
                    Image = pro.Image
                });
            }
            catch (InvalidOperationException)
            {
                // Single() nem adott eredményt
                return NotFound();
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpGet("category/{id}")]
        public IActionResult GetProductForCategory(int id)
        {
            try
            {
                return Ok(_service
                    .GetProductsForCategory(id)
                    .ToList()
                    .Select(pro => new ProductDTO
                    {
                        Id = pro.ID,
                        Manufacturer = pro.Manufacturer,
                        Amount = pro.Amount,
                        Available = pro.Available,
                        Price = pro.Price,
                        Description = pro.Description,
                        ModelID = pro.ModelID, // TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
                        Category = new CategoryDTO { Id = pro.CategoryId, Name = pro.Category.Name },
                        Image = pro.Image
                    }));
            }
            catch
            {
                // Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Authorize]
        [HttpPut("{id}")]
        public IActionResult PutProduct(Int32 id, ProductDTO pro)
        {
            if (id != pro.Id)
            {
                return BadRequest();
            }

            Product change = _service.GetProduct(pro.Id);
            change.Amount = pro.Amount;
            change.Available = pro.Available;

            if (_service.UpdateProduct(change))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
