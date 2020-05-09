using Data;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebAPI.Controllers;
using Xunit;

namespace Testing
{
    public class CategoriesControllerTest : IDisposable
    {
        private readonly WebShopContext _context;
        private readonly WebShopServices _service;
        private readonly CategoriesController _controller;

        public CategoriesControllerTest()
        {
            var options = new DbContextOptionsBuilder<WebShopContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new WebShopContext(options);
            TestDbInitializer.Initialize(_context);
            _service = new WebShopServices(_context);
            _controller = new CategoriesController(_service);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [Fact]
        public void GetCategoriesTest()
        {
            // Act
            var result = _controller.GetCategories();
            
            // Assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);

            var content = Assert.IsAssignableFrom<IEnumerable<CategoryDTO>>(objectResult.Value);
        }
    }
}
