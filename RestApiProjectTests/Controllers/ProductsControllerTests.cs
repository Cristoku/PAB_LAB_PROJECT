using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiProject.Controllers;
using RestApiProject.Data;
using RestApiProject.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RestApiProjectTests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly AppDbContext _context;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _controller = new ProductsController(_context);


            _context.Products.AddRange(
                new Product { Id = 1, Name = "Product1", Price = 10 },
                new Product { Id = 2, Name = "Product2", Price = 20 }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            var result = _controller.GetProducts();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var products = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public void GetProduct_ReturnsProduct()
        {
            var result = _controller.GetProduct(1);
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            var product = Assert.IsAssignableFrom<Product>(actionResult.Value);
            Assert.Equal("Product1", product.Name);
        }

        [Fact]
        public void PostProduct_AddsProduct()
        {
            var newProduct = new Product { Id = 3, Name = "Product3", Price = 30 };
            var result = _controller.PostProduct(newProduct);
            Assert.IsType<CreatedAtActionResult>(result.Result);

            var products = _context.Products.ToList();
            Assert.Equal(3, products.Count);
            Assert.Equal("Product3", products.Last().Name);
        }

        [Fact]
        public void PutProduct_UpdatesProduct()
        {
            var updatedProduct = new Product { Id = 1, Name = "UpdatedProduct1", Price = 100 };
            var result = _controller.PutProduct(1, updatedProduct);
            Assert.IsType<NoContentResult>(result);

            var product = _context.Products.Find(1);
            Assert.Equal("UpdatedProduct1", product.Name);
        }

        [Fact]
        public void DeleteProduct_DeletesProduct()
        {
            var result = _controller.DeleteProduct(1);
            Assert.IsType<NoContentResult>(result);

            var product = _context.Products.Find(1);
            Assert.Null(product);
        }
    }
}
