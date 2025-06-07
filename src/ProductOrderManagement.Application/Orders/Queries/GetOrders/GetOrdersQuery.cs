using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Orders.DTOs;

namespace ProductOrderManagement.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(int Page, int PageSize) : IQuery<PaginatedList<OrderDto>>;