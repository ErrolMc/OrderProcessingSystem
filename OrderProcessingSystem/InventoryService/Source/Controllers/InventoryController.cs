using Microsoft.AspNetCore.Mvc;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;

namespace OPS.InventoryService.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController: ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryRepository inventoryRepository, ILogger<InventoryController> logger)
        {
            _inventoryRepository = inventoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Inventory> items = await _inventoryRepository.GetAllInventoryAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Inventory item = await _inventoryRepository.GetInventoryByIdAsync(id);
            if (item is null)
                return NotFound("Inventory not found");

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inventory inventory)
        {
            inventory.ID = Guid.NewGuid().ToString();
            inventory.CreatedAt = DateTime.UtcNow;
            inventory.UpdatedAt = DateTime.UtcNow;

            bool created = await _inventoryRepository.CreateInventoryAsync(inventory);
            if (!created)
                return BadRequest("Failed to create inventory record");

            _logger.LogInformation($"Inventory {inventory.ID} created for product {inventory.ProductID}");
            return Ok(inventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Inventory inventory)
        {
            inventory.ID = id;
            inventory.UpdatedAt = DateTime.UtcNow;

            bool updated = await _inventoryRepository.UpdateInventoryAsync(inventory);
            if (!updated)
                return NotFound("Inventory not found or not updated");

            _logger.LogInformation($"Inventory {inventory.ID} updated");
            return Ok(inventory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool deleted = await _inventoryRepository.DeleteInventoryByIdAsync(id);
            if (!deleted)
                return NotFound("Inventory not found");

            _logger.LogInformation($"Inventory {id} deleted");
            return Ok("Inventory deleted");
        }
    }
}
