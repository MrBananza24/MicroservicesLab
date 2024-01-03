using InventoryApi.Models;

namespace InventoryApi.Interfaces;

public interface IOrderService
{
    public Task<List<Order>> GetAsync();
    public Task<Order?> GetAsync(Guid id);
    public Task<Order?> CreateAsync(List<OrderItem> orderItems);
    public Task<Order?> DeleteAsync(Guid id);
}
