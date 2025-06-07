using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Application.Orders.Commands.PlaceOrder;

public record PlaceOrderCommand(Name CustomerName, List<OrderItemRequest> Items) : ICommand<Guid>;