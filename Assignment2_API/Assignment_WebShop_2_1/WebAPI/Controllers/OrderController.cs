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

            Order change = _service.GetOrderByID(o.ID);
            for (int i = 0; i < o.OrderedProducts.Count(); i++)
            {
                var x = o.OrderedProducts[i];
                change.OrderedProducts[i].amount = x.amount;
                //change.OrderedProducts.Single(x => x.ID == x.ID).product.ID = x.product.Id;
                change.OrderedProducts[i].product.Amount = x.product.Amount;
                change.OrderedProducts[i].product.Available = x.product.Available;
                //change.OrderedProducts.Single(x => x.ID == x.ID).Category = new Category { ID = x.product.Category.Id, Name = x.product.Category.Name },
                change.OrderedProducts[i].product.Description = x.product.Description;
                change.OrderedProducts[i].product.Image = x.product.Image;
                change.OrderedProducts[i].product.Manufacturer = x.product.Manufacturer;
                change.OrderedProducts[i].product.ModelID = x.product.ModelID;
                change.OrderedProducts[i].product.Price = x.product.Price;
                change.OrderedProducts[i].product.CategoryId = x.product.Category.Id;
            } 

            change.Address = o.Address;
            change.Delivered = o.Delivered;
            change.Email = o.Email;
            change.PhoneNumber = o.PhoneNumber;
            change.UserName = o.UserName;




            if (_service.UpdateOrder(change))
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