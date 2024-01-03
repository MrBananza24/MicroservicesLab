namespace GraphqlApi.Schema;

public class Mutation
{
    public async Task<Order> AddOrderAsync([Service] OrderService orderService, IEnumerable<OrderItem> orderItems, CancellationToken cancellationToken) => await orderService.OrderPOSTAsync(orderItems, cancellationToken);
}
