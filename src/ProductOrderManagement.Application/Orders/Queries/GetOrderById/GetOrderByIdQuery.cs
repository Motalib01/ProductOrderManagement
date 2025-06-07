using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Orders.DTOs;

namespace ProductOrderManagement.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto>;