using MongoDB.Bson.Serialization.Attributes;

namespace InventoryApi.Models;

public class Order
{
    [BsonId]
    public Guid Id { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}