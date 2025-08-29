using OPS.InventoryService.Data;

namespace OPS.InventoryService.Source.Repositories
{
    public interface IInventoryRepository
    {
        public Task<bool> UpdateInventoryAsync(Inventory inventory);
        public Task<Inventory> GetInventoryByIdAsync(string inventoryID);
        public Task<bool> CreateInventoryAsync(Inventory inventory);
        public Task<bool> DeleteInventoryByIdAsync(string inventoryID);
        public Task<List<Inventory>> GetAllInventoryAsync();
    }
}
