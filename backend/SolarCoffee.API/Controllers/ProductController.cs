using Microsoft.AspNetCore.Mvc;
using SolarCoffee.API.Serialization;
using SolarCoffee.API.ViewModels;
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

        /// <summary>
        /// Return all products
        /// </summary>
        [HttpGet]   
        public IActionResult GetProduct()
        {
           _logger.LogInformation("Getting all products");
           var products = _productService.GetAll();
           var productViewModel = products.Select(ProductMapper.SerializeProductModel); 
           return Ok(productViewModel);
        }

        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <param name="product"></param>
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
               return BadRequest(ModelState);

            _logger.LogInformation("Adding product");
            var newProduct = ProductMapper.SerializeProductModel(product);
            var newProductResponse = _productService.Create(newProduct);
            return Ok(newProductResponse);
        }

        /// <summary>
        /// Archives an existing product
        /// </summary>
        /// <param name="id"></param>
        [HttpPatch("{id:int}")]   
        public IActionResult ArchiveProduct(int id)
        {
           _logger.LogInformation("Archiving product");
           var archiveResult = _productService.Archive(id);
           return Ok(archiveResult);
        }
        
    }
}