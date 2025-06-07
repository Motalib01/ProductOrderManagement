using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Orders.DTOs;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;

namespace ProductOrderManagement.Application.Orders.Queries.GetOrderById;

internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(query.Id, cancellationToken);
        

        return Result.Success(OrderMapper.ToDto(order));
    }
}