using Shared.Models;
using Shared.Interfaces;
using InventoryApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc;
using InventoryApi.Models;

namespace InventoryApi.Services;

public class InventoryService : IInventoryService, IInventoryGrpcService
{
    private readonly InventoryContext context;

    public InventoryService(InventoryContext context)
    {
        this.context = context;
    }

    public async Task<List<ProductItem>> GetAllProductsAsync()
    {
        var products = await context.Inventory.ToListAsync();
        return products;
    }

    public async Task<ProductItem?> GetProductAsync(Guid id)
    {
        var product = await context.Inventory.FirstOrDefaultAsync(x => x.Id == id);
        return product;
    }

    public async Task<List<ProductItem>> AddProductAsync(ProductItem product)
    { 
        context.Inventory.Add(product);
        await context.SaveChangesAsync();
        return await context.Inventory.ToListAsync();
    }

    public async Task<List<ProductItem>?> DeleteProductAsync(Guid id)
    {
        var product = await context.Inventory.FindAsync(id);
        if (product is null)
            return null;

        context.Inventory.Remove(product);
        await context.SaveChangesAsync();

        return await context.Inventory.ToListAsync();
    }

    public async Task<List<ProductItem>?> UpdateProductAsync(Guid id, ProductItem newProduct)
    {
        var product = await context.Inventory.FindAsync(id);
        if (product is null)
            return null;

        product.Price = newProduct.Price;
        product.Count = newProduct.Count;
        await context.SaveChangesAsync();

        return await context.Inventory.ToListAsync();
    }

    public async Task<ProductItem?> UpdateProductCountAsync(Guid id, int newCount)
    {
        var product = await context.Inventory.FindAsync(id);
        if (product is null)
            return null;

        product.Count = newCount;
        await context.SaveChangesAsync();

        return product;
    }

    public async Task<GetProductReply> GetProductAsync(GetProductRequest request, CallContext context = default)
    {
        var product = await GetProductAsync(request.Id);
        return new GetProductReply
        {
            Id = product?.Id ?? Guid.Empty,
            Count = product?.Count ?? 0,
            Price = product?.Price ?? 0,
        };
    }

    public async Task<UpdateProductReply> UpdateProductCountAsync(UpdateProductRequest request, CallContext context = default)
    {
        var product = await UpdateProductCountAsync(request.Id, request.NewCount);
        return new UpdateProductReply { Id = product.Id, NewCount = product.Count};
    }
}
