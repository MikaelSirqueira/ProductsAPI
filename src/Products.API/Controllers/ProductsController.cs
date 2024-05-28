using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.Data;

namespace Products.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsDbContext _context;

    public ProductsController(ProductsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var productsList = _context.Products;

        if (productsList == null)
        {
            return NotFound();
        }
        
        return Ok(productsList.ToList());
    }
}
