using ProductOrderManagement.Domain.Order;

namespace ProductOrderManagement.Application.Orders.DTOs;

public record OrderDto(
    Guid Id,
    string CustomerName,
    decimal TotalPrice,
    string Status,
    DateTime CreatedOnUtc);

public static class OrderMapper
{
    public static OrderDto ToDto(Order order)
    {
        return new OrderDto(
            order.Id,
            order.CustomerName.Value,
            order.TotalPrice.Amount,
            order.Status.ToString(),
            order.CreatedOnUtc);
    }
}