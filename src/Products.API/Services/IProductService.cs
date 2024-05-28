using Products.API.Entities;

namespace Products.API.Services
{
    public interface IProductService
    {
        List<Product> GetAll(string? sortBy, string? searchByName);
        IQueryable<Product> GetById(int id);
        Product Create(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
