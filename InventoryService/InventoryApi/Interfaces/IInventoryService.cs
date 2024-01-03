using InventoryApi.Models;
using Shared.Models;

namespace InventoryApi.Interfaces;

public interface IInventoryService
{
    Task<ProductItem?> GetProductAsync(Guid id);
    Task<List<ProductItem>> GetAllProductsAsync();
    Task<List<ProductItem>> AddProductAsync(ProductItem product);
    Task<List<ProductItem>?> UpdateProductAsync(Guid id, ProductItem newProduct);
    Task<List<ProductItem>?> DeleteProductAsync(Guid id);
}