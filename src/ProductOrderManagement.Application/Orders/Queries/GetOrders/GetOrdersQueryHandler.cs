using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Orders.DTOs;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;

namespace ProductOrderManagement.Application.Orders.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<PaginatedList<OrderDto>>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetPaginatedAsync(query.Page, query.PageSize, cancellationToken);

        var orderDtos = orders.Items.Select(OrderMapper.ToDto).ToList();

        var paginatedDtos = PaginatedList<OrderDto>.ToPaginatedList(orderDtos, query.Page, query.PageSize, orders.TotalCount);

        return Result.Success(paginatedDtos);
    }
}