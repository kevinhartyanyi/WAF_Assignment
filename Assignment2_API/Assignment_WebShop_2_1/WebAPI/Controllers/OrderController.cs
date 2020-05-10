using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        protected readonly WebShopServices _service;
        public OrderController(WebShopServices service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetOrders()
        {
            try
            {
                var tmp = _service.GetOrders().ToList();
                tmp[0].OrderedProducts.Count();
                return Ok(_service.GetOrders().ToList().Select(o => new OrderDTO
                {
                    ID = o.ID,
                    UserName = o.UserName,
                    Email = o.Email,
                    Address = o.Address,
                    PhoneNumber = o.PhoneNumber,
                    Delivered = o.Delivered,
                    OrderedProducts = new List<BasketElemDTO>(o.OrderedProducts.Select(x => new BasketElemDTO
                    { 
                        amount = x.amount,
                        Id = x.ID,
                        product = new ProductDTO
                        {
                            Id = x.product.ID,
                            Amount = x.product.Amount,
                            Available = x.product.Available,
                            Category = new CategoryDTO { Id = x.product.CategoryId, Name = _service.GetCategoryByID(x.product.CategoryId).Name},
                            Description = x.product.Description,
                            Image = x.product.Image,
                            Manufacturer = x.product.Manufacturer,
                            ModelID = x.product.ModelID,
                            Price = x.product.Price
                        }
                    }))
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
        public IActionResult GetOrder(int id)
        {
            try
            {
                var o = _service.GetOrderByID(id);

                return Ok(new OrderDTO
                {
                    ID = o.ID,
                    UserName = o.UserName,
                    Email = o.Email,
                    Address = o.Address,
                    PhoneNumber = o.PhoneNumber,
                    Delivered = o.Delivered,
                    OrderedProducts = new List<BasketElemDTO>(o.OrderedProducts.Select(x => new BasketElemDTO
                    {
                        amount = x.amount,
                        Id = x.ID,
                        product = new ProductDTO
                        {
                            Id = x.product.ID,
                            Amount = x.product.Amount,
                            Available = x.product.Available,
                            Category = new CategoryDTO { Id = x.product.CategoryId, Name = _service.GetCategoryByID(x.product.CategoryId).Name },
                            Description = x.product.Description,
                            Image = x.product.Image,
                            Manufacturer = x.product.Manufacturer,
                            ModelID = x.product.ModelID,
                            Price = x.product.Price
                        }
                    }))
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
        [HttpPut("{id}")]
        public IActionResult PutOrder(Int32 id, OrderDTO o)
        {
            if (id != o.ID)
            {
                return BadRequest();
            }

            Order ord = new Order
            {
                OrderedProducts = new List<BasketElem>(o.OrderedProducts.Select(x => new BasketElem
                {
                    ID = x.Id,
                    amount = x.amount,
                    product = new Product
                    {
                        ID = x.product.Id,
                        Amount = x.product.Amount,
                        Available = x.product.Available,
                        Category = new Category { ID = x.product.Category.Id, Name = x.product.Category.Name },
                        Description = x.product.Description,
                        Image = x.product.Image,
                        Manufacturer = x.product.Manufacturer,
                        ModelID = x.product.ModelID,
                        Price = x.product.Price,
                        CategoryId = x.product.Category.Id
                    }
                })),
                Address = o.Address,
                Delivered = o.Delivered,
                Email = o.Email,
                ID = o.ID,
                PhoneNumber = o.PhoneNumber,
                UserName = o.UserName
            };

            if (_service.UpdateOrder(ord))
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