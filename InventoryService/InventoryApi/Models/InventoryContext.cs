using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Models;

public class InventoryContext : DbContext
{
    public DbSet<ProductItem> Inventory { get; set; } = null!;

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {

    }
}
