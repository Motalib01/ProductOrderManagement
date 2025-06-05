using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Orders.DTOs;

namespace ProductOrderManagement.Application.Orders.Queries.GetOrders;

public sealed record OrderItemRequest(Guid ProductId, int Quantity);

public sealed record GetOrdersQuery(int Page, int PageSize) : IQuery<PaginatedList<OrderDto>>;

public sealed record GetOrderByIdQuery(Guid Id) : IQuery<OrderDto>;

