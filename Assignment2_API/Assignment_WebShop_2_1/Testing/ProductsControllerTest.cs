using Client.Model;
using Client.ViewModel;
using Data;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using WebAPI.Controllers;
using Xunit;

namespace Testing
{
    public class ProductsControllerTest : IDisposable
    {
        private readonly WebShopContext _context;
        private readonly WebShopServices _service;
        private readonly ProductsController _controller;

        public ProductsControllerTest()
        {
            var options = new DbContextOptionsBuilder<WebShopContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WebShopContext(options);
            TestDbInitializer.Initialize(_context);
            _service = new WebShopServices(_context);
            _controller = new ProductsController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void GetProductsTest()
        {
            // Act
            var result = _controller.GetProducts();

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var content = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(objectResult.Value);
        }

        [Theory]
        [InlineData(1)] // 15, 20
        [InlineData(2)]
        [InlineData(3)]
        public void GetProductByIDTest(int id)
        {
            // Act
            var result = _controller.GetProduct(id);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var content = Assert.IsAssignableFrom<ProductDTO>(objectResult.Value);
        }

        [Fact]
        public void GetInvalidProductTest()
        {
            // Arrange
            var id = 0;

            // Act
            var result = _controller.GetProduct(id);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetProductByCategoryTest(int id)
        {
            // Act
            var result = _controller.GetProductForCategory(id);

            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var content = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(objectResult.Value);
            foreach (var item in content)
            {
                Assert.Equal(item.Category.Id, id);
            }
        }

        [Theory]
        [InlineData(15)] // 15, 20
        public void PutProductTest(int id)
        {
            // Arrange
            var currentProduct = _service.GetProduct(id);
            Assert.Equal(3, currentProduct.Amount);

            var newProduct = new ProductDTO {
                Id = currentProduct.ID,
                Manufacturer = currentProduct.Manufacturer,
                Description = currentProduct.Description,
                ModelID = currentProduct.ModelID,
                Image = currentProduct.Image,
                Category = new CategoryDTO { Name = currentProduct.Category.Name, Id = currentProduct.Category.ID },
                Available = currentProduct.Available,
                Amount = currentProduct.Amount,
                Price = currentProduct.Price
            };
            newProduct.Amount = 5;

            // Act
            //_service.UpdateProduct(currentProduct);
            var result = _controller.PutProduct(id, newProduct);           
            
            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var newCurrentProduct = _service.GetProduct(id);
            Assert.Equal(5, newCurrentProduct.Amount);
        }
    }
}
