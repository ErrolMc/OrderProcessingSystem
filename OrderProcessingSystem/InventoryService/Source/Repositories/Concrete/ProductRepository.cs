using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OPS.InventoryService.Data;
using OPS.InventoryService.Source.Repositories;
using OPS.Shared;
using OPS.Shared.Constants;

namespace OPS.InventoryService.Repositories.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            _productsCollection = database.GetCollection<Product>(TableNames.PRODUCTS_TABLE_NAME);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.ID, product.ID);
            ReplaceOneResult result = await _productsCollection.ReplaceOneAsync(
                filter: filter,
                replacement: product,
                options: new ReplaceOptions { IsUpsert = false });

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<Product> GetProductByIdAsync(string productID)
        {
            return await _productsCollection.Find(u => u.ID == productID).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateProductASync(Product product)
        {
            try
            {
                await _productsCollection.InsertOneAsync(product);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productsCollection.Find(u => true).ToListAsync();
        }
    }
}
