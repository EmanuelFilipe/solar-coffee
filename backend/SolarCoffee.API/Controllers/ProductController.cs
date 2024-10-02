using Microsoft.AspNetCore.Mvc;
using SolarCoffee.API.Serialization;
using SolarCoffee.Services.Product;

namespace SolarCoffee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, 
                                 IProductService productService)
        {
           _logger = logger;
           _productService = productService;
        }

        [HttpGet]   
        public IActionResult GetProduct()
        {
           _logger.LogInformation("Getting all products");
           var products = _productService.GetAll();
           var productViewModel = products.Select(ProductMapper.SerializeProductModel); 
           return Ok(productViewModel);
        }
        
    }
}