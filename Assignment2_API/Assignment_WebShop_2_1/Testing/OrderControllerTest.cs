using Data;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Controllers;
using Xunit;

namespace Testing
{
    public class OrderControllerTest : IDisposable
    {
        private readonly WebShopContext _context;
        private readonly WebShopServices _service;
        private readonly OrderController _controller;

        public OrderControllerTest()
        {
            var options = new DbContextOptionsBuilder<WebShopContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WebShopContext(options);
            TestDbInitializer.Initialize(_context);
            _service = new WebShopServices(_context);
            _controller = new OrderController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetOrdersTest()
        {
            // Act
            var result = _controller.GetOrders();

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var content = Assert.IsAssignableFrom<IEnumerable<OrderDTO>>(objectResult.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetOrderTest(int id)
        {
            // Act
            var result = _controller.GetOrder(id);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var content = Assert.IsAssignableFrom<OrderDTO>(objectResult.Value);
        }

        [Fact]
        public void GetInvalidOrderTest()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _controller.GetOrder(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)] // 15, 20
        public void PutOrderTest(int id)
        {
            // Arrange
            var currentOrder = _service.GetOrderByID(id);
            Assert.False(currentOrder.Delivered);

            var newOrderedProducts = new List<BasketElemDTO>();
            foreach (var x in currentOrder.OrderedProducts)
            {
                newOrderedProducts.Add(new BasketElemDTO
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
                });
            }

            var newOrder = new OrderDTO
            {
                ID = currentOrder.ID,
                UserName = currentOrder.UserName,
                Address = currentOrder.Address,
                PhoneNumber = currentOrder.PhoneNumber,
                Delivered = currentOrder.Delivered,
                Email = currentOrder.Email,
                OrderedProducts = newOrderedProducts
            };
            newOrder.Delivered = true;

            // Act
            //_service.UpdateProduct(currentProduct);
            var result = _controller.PutOrder(id, newOrder);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkResult>(result);
            var newCurrentProduct = _service.GetOrderByID(id);
            Assert.True(currentOrder.Delivered);
        }
    }
}
