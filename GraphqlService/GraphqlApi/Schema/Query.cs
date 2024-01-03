namespace GraphqlApi.Schema;

public class Query
{
    public async Task<ICollection<Order>> GetOrdersAsync([Service] OrderService orderService, CancellationToken cancellationToken)
        => await orderService.OrderAllAsync(cancellationToken);

    public async Task<Order> GetOrderAsync([Service] OrderService orderService, Guid id, CancellationToken cancellationToken)
    => await orderService.OrderGETAsync(id, cancellationToken);

    public async Task<Product> GetProductAsync([Service] ProductService productService, Guid id, CancellationToken cancellationToken)
        => await productService.ProductGETAsync(id, cancellationToken);

    public async Task<ICollection<Product>> GetProductsAsync([Service] ProductService productService, CancellationToken cancellationToken)
        => await productService.ProductAllAsync(cancellationToken);
}
