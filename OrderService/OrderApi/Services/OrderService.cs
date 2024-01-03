using Microsoft.Extensions.Options;
using MongoDB.Driver;
using InventoryApi.Interfaces;
using InventoryApi.Models;
using Shared.Models;
using Shared.Interfaces;

namespace InventoryApi.Services;

public class OrderService : IOrderService
{
    private readonly IMongoCollection<Order> collection;
    private readonly IInventoryGrpcService productGrpcService;

    public OrderService(IOptions<OrdersDatabaseSettings> options, IInventoryGrpcService productGrpcService)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        collection = mongoDatabase.GetCollection<Order>(options.Value.CollectionName);

        this.productGrpcService = productGrpcService;
    }

    public async Task<List<Order>> GetAsync()
    {
        var order = await collection.Find(_ => true).ToListAsync();
        return order;
    }

    public async Task<Order?> GetAsync(Guid id)
    {
        var order = await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return order;
    }

    public async Task<Order?> CreateAsync(List<OrderItem> orderItems)
    {
        //Если список товаров пуст, то заказ не создается
        if (orderItems is null || orderItems.Count == 0)
            return null;
        Order newOrder = new();
        foreach (var item in orderItems)
        {
            //синхронный запрос к InventoryService, проверяем наличие товара на складе
            var product = await productGrpcService.GetProductAsync(new GetProductRequest { Id = item.Id});
            //если товаров меньше на складе меньше чем просят, или 0, то товар не добавляется в заказ
            if (product.Id == Guid.Empty || product.Count < item.RequestedCount)
                continue;
            product.Count -= item.RequestedCount;
            await productGrpcService.UpdateProductCountAsync(new UpdateProductRequest { Id = item.Id, NewCount = product.Count});
            newOrder.Items.Add(item);
            newOrder.TotalPrice += product.Price * item.RequestedCount;
        }
        if (newOrder.Items.Count == 0)
            return null;
        newOrder.CreatedAt = DateTime.UtcNow;
        await collection.InsertOneAsync(newOrder);
        return newOrder;
    }

    public async Task<Order?> DeleteAsync(Guid id)
    {
        var order = await GetAsync(id);
        if (order is null)
            return null;
        foreach (var item in order.Items)
        {
            var product = await productGrpcService.GetProductAsync(new GetProductRequest { Id = item.Id });
            product.Count += item.RequestedCount;

            await productGrpcService.UpdateProductCountAsync(new UpdateProductRequest { Id = item.Id, NewCount = product.Count });
        }
        await collection.DeleteOneAsync(x => x.Id == id);
        return order;
    }
}
