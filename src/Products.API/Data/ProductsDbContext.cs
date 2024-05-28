using Microsoft.EntityFrameworkCore;
using Products.API.Entities;

namespace Products.API.Data;

public class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Nome = "Intel i9-14900K", Estoque = 10, Valor = 4000 },
            new Product { Id = 2, Nome = "Intel i7-13700K", Estoque = 20, Valor = 3000 },
            new Product { Id = 3, Nome = "Ryzen 5 5600X", Estoque = 30, Valor = 1500 },
            new Product { Id = 4, Nome = "Ryzen 5 3400G", Estoque = 40, Valor = 1000 },
            new Product { Id = 5, Nome = "Intel i3-12100F", Estoque = 50, Valor = 800 }
        );
    }
}
