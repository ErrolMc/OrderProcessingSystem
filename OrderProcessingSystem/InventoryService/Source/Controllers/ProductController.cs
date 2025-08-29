using Microsoft.AspNetCore.Mvc;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;

namespace OPS.InventoryService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Product product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            product.ID = Guid.NewGuid().ToString();
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            bool created = await _productRepository.CreateProductASync(product);
            if (!created)
                return BadRequest("Failed to create product");

            _logger.LogInformation($"Product {product.Name} ({product.ID}) created");
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Product product)
        {
            product.ID = id;
            product.UpdatedAt = DateTime.UtcNow;

            bool updated = await _productRepository.UpdateProductAsync(product);
            if (!updated)
                return NotFound("Product not found or not updated");

            _logger.LogInformation($"Product {product.ID} updated");
            return Ok(product);
        }
    }
}
