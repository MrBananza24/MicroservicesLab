namespace InventoryApi.Models;

public class ProductItem
{
    public Guid Id { get; set; }
    public int Count { get; set; }
    public decimal Price { get; set; }
}
