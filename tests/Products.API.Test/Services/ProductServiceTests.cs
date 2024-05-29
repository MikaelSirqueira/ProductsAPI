using Products.API.Entities;
using Products.API.Services;
using Products.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Products.API.Test.Services
{
    public class ProductServiceTests
    {
        private readonly DbContextOptions<ProductsDbContext> _options;

        public ProductServiceTests()
        {
            _options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        private ProductsDbContext CreateContext()
        {
            var context = new ProductsDbContext(_options);
            context.Database.EnsureDeleted();  // Clean up before each test
            context.Database.EnsureCreated();
            context.Products.AddRange(
                new List<Product>
                {
                    new Product { Nome = "Product1", Valor = 10, Estoque = 100 },
                    new Product { Nome = "Product2", Valor = 20, Estoque = 200 }
                }
            );
            context.SaveChanges();
            return context;
        }


        [Fact]
        public void GetAll_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var products = productService.GetAll(null, null);

            Assert.True(products.Count() > 0);
        }

        [Fact]
        public void GetAll_SortByName_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var products = productService.GetAll("nome", null).ToList();

            Assert.True(products.Count > 0);
        }

        [Fact]
        public void GetAll_SortByValue_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var products = productService.GetAll("valor", null).ToList();

            Assert.True(products.Count > 0);
        }

        [Fact]
        public void GetAll_SearchByName_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var products = productService.GetAll(null, "Product2").ToList();

            Assert.True(products.Count > 0);
        }

        [Fact]
        public void GetById_ExistingId_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var product = productService.GetById(1).FirstOrDefault();

            Assert.NotNull(product);
        }

        [Fact]
        public void GetById_NonExistingId_Error()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var iqProduct = productService.GetById(999);
            var product = iqProduct.FirstOrDefault();

            Assert.Null(product);
        }

        [Fact]
        public void Create_ProductWithDuplicateName_Error()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var newProduct = new Product { Nome = "Product1", Valor = 30, Estoque = 300 };

            var exception = Assert.Throws<Exception>(() => productService.Create(newProduct));
            Assert.Equal("Esse produto já existe", exception.Message);
        }

        [Fact]
        public void Create_ProductWithNegativeValue_Error()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var newProduct = new Product { Nome = "Product3", Valor = -10, Estoque = 300 };

            var exception = Assert.Throws<Exception>(() => productService.Create(newProduct));
            Assert.Equal("O valor do produto não pode ser negativo.", exception.Message);
        }

        [Fact]
        public void Create_ValidProduct_Success()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var newProduct = new Product { Nome = "Product3", Valor = 30, Estoque = 300 };

            var product = productService.Create(newProduct);

            Assert.NotNull(product);
        }

        [Fact]
        public void Update_ExistingProduct_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var updatedProduct = new Product { Nome = "UpdatedProduct", Valor = 50, Estoque = 10 };

            productService.Update(1, updatedProduct);

            var product = context.Products.Find(1);
            Assert.Equal("UpdatedProduct", product.Nome);
            Assert.Equal(50, product.Valor);
            Assert.Equal(10, product.Estoque);
        }

        [Fact]
        public void Update_NonExistingProduct_Error()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var updatedProduct = new Product { Nome = "UpdatedProduct", Valor = 50, Estoque = 500 };

            var exception = Assert.Throws<Exception>(() => productService.Update(999, updatedProduct));
            Assert.Equal("Product not found.", exception.Message);
        }

        [Fact]
        public void Delete_ExistingProduct_Sucess()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            productService.Delete(1);

            var product = context.Products.Find(1);
            Assert.Null(product);
        }

        [Fact]
        public void Delete_NonExistingProduct_Error()
        {
            using var context = CreateContext();
            var productService = new ProductService(context);

            var exception = Assert.Throws<Exception>(() => productService.Delete(999));
            Assert.Equal("Product not found.", exception.Message);
        }
    }
}
