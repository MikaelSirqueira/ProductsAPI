using Microsoft.EntityFrameworkCore;
using Products.API.Data;
using Products.API.Entities;
using Products.API.Enums;
using Products.API.Services;

namespace Products.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDbContext _context;

        public ProductService(ProductsDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll(string? paramSort, string? searchByName)
        {
            var iqProducts = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchByName))
            {
                iqProducts = iqProducts.Where(p => p.Nome.Contains(searchByName));
            }

            if (!string.IsNullOrEmpty(paramSort))
            {
                iqProducts = SortedList(paramSort, iqProducts);
            }

            return iqProducts.ToList();
        }

        private IQueryable<Product> SortedList(string paramSort, IQueryable<Product> iqProducts)
        {
            switch (paramSort.ToUpper())
            {
                case SortBy.NOME:
                    iqProducts = iqProducts.OrderBy(p => p.Nome);
                    break;
                case SortBy.VALOR:
                    iqProducts = iqProducts.OrderBy(p => p.Valor);
                    break;
                case SortBy.ESTOQUE:
                    iqProducts = iqProducts.OrderBy(p => p.Estoque);
                    break;
            }

            return iqProducts;
        }

        public IQueryable<Product> GetById(int id)
        {
            return _context.Products.Where(x => x.Id == id);
        }

        public Product Create(Product product)
        {
            if (product.Valor < 0) 
            { 
                throw new Exception("O valor do produto não pode ser negativo."); 
            }

            var existingProduct = _context.Products.FirstOrDefault(p => p.Nome == product.Nome);
            if (existingProduct != null)
            {
                throw new Exception("Esse produto já existe");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Update(int id, Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

            if (product.Valor < 0)
            {
                throw new Exception("O valor do produto não pode ser negativo.");
            }


            existingProduct.Nome = product.Nome;
            existingProduct.Valor = product.Valor;
            existingProduct.Estoque = product.Estoque;

            _context.Entry(existingProduct).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
