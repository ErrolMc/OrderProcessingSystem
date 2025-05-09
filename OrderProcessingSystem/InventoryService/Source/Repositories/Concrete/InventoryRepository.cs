using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;
using OPS.Shared;

namespace OPS.InventoryService.Repositories.Concrete
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoCollection<Inventory> _inventoryCollection;

        public InventoryRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _inventoryCollection = database.GetCollection<Inventory>("Inventory");
        }
    }
}

