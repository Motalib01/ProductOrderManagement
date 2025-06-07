namespace ProductOrderManagement.Application.Orders.Commands.PlaceOrder;

public record OrderItemRequest(Guid ProductId, int Quantity);