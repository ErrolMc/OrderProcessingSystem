using OPS.InventoryService.Data;

namespace OPS.InventoryService.Source.Repositories
{
    public interface IProductRepository
    {
        public Task<bool> UpdateProductAsync(Product product);
        public Task<Product> GetProductByIdAsync(string productID);
        public Task<bool> CreateProductASync(Product product);
        public Task<List<Product>> GetAllProductsAsync();
    }
}
