using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOrder.Application.Services;

namespace SmartOrder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, decimal price, string currency)
        {
            var id = await _productService.CreateProductAsync(name, price, currency);
            return CreatedAtAction(nameof(Create), new {id}, id);
        }
    }
}
