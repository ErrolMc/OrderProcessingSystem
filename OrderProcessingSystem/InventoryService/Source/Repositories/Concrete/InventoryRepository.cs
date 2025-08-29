using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;
using OPS.Shared;
using OPS.Shared.Constants;

namespace OPS.InventoryService.Repositories.Concrete
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventoryCollection;

        public InventoryRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _inventoryCollection = database.GetCollection<Inventory>(TableNames.INVENTORY_TABLE_NAME);
        }
        
        public async Task<bool> UpdateInventoryAsync(Inventory inventory)
        {
            FilterDefinition<Inventory> filter = Builders<Inventory>.Filter.Eq(p => p.ID, inventory.ID);
            ReplaceOneResult result = await _inventoryCollection.ReplaceOneAsync(
                filter: filter,
                replacement: inventory,
                options: new ReplaceOptions { IsUpsert = false });

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
        
        public async Task<Inventory> GetInventoryByIdAsync(string inventoryID)
        {
            return await _inventoryCollection.Find(u => u.ID == inventoryID).FirstOrDefaultAsync();
        }
        
        public async Task<bool> CreateInventoryAsync(Inventory inventory)
        {
            try
            {
                await _inventoryCollection.InsertOneAsync(inventory);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteInventoryByIdAsync(string inventoryID)
        {
            DeleteResult res = await _inventoryCollection.DeleteOneAsync(u => u.ID == inventoryID);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<List<Inventory>> GetAllInventoryAsync()
        {
            return await _inventoryCollection.Find(u => true).ToListAsync();
        }
    }
}
