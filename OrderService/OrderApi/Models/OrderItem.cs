namespace InventoryApi.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public int RequestedCount { get; set; }
}