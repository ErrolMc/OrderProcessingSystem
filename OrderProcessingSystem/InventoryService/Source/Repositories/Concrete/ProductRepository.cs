using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;
using OPS.Shared;

namespace OPS.InventoryService.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _productsCollection = database.GetCollection<Product>("Products");
        }
    }
}

