using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.API.Data;
using Products.API.Entities;
using Products.API.Services;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll(string? sortBy, string? searchByName)
        {
            var products = _productService.GetAll(sortBy, searchByName);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product.FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            try
            {
                var createdProduct = _productService.Create(product);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Product product)
        {
            try
            {
                _productService.Update(id, product);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }
    }
}
