namespace ProductOrderManagement.Application.Orders.Queries.GetOrders;

public sealed record OrderItemRequest(Guid ProductId, int Quantity);