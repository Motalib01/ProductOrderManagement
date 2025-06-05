using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order.Events;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Domain.Order;

public sealed class Order : Entity
{
    
    private Order(
      Guid id,
      Name customerName,
      List<OrderItem> items,
      Money totalPrice,
      OrderStatus status,
      DateTime createdOnUtc)
      : base(id)
    {
        CustomerName = customerName;
        Items = items;
        TotalPrice = totalPrice;
        Status = status;
        CreatedOnUtc = createdOnUtc;
    }
    private Order() : base(Guid.Empty)
    {
        Items = new List<OrderItem>();
    }


    public Name CustomerName { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    public Money TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime? ConfirmedOnUtc { get; private set; }
    public DateTime? CancelledOnUtc { get; private set; }
    public DateTime? DeliveredOnUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }

    public static Result<Order> Create(
        Guid id,
        Name customerName,
        List<OrderItem> items,
        Money totalPrice,
        OrderStatus status,
        DateTime createdOnUtc)
    {
        if (items == null || !items.Any())
        {
            return Result.Failure<Order>(OrderErrors.EmptyOrder);
        }

        foreach (var item in items)
        {
            if (item.Quantity <= 0)
            {
                return Result.Failure<Order>(OrderErrors.InvalidQuantity);
            }
        }

        var order = new Order(id, customerName, items, totalPrice, status, createdOnUtc);
        return Result.Success(order);
    }
    public static Result<Order> PlaceOrder(
        Name customerName,
        List<OrderItem> items,
        DateTime utcNow,
        PricingService pricingService)
    {
        if (items == null || !items.Any())
        {
            return Result.Failure<Order>(OrderErrors.EmptyOrder);
        }

        var totalPriceResult = pricingService.CalculateTotal(items);

        if (!totalPriceResult.IsSuccess)
            return Result.Failure<Order>(totalPriceResult.Error);

        var totalPrice = totalPriceResult.Value;

        var order = new Order(
            Guid.NewGuid(),
            customerName,
            items,
            totalPrice,
            OrderStatus.Pending,
            utcNow);

        order.RaiseDomainEvent(new OrderPlacedDomainEvent(order.Id));

        return Result.Success(order);
    }

    public Result Confirm(DateTime utcNow)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(OrderErrors.NotPending);
        }

        Status = OrderStatus.Confirmed;
        ConfirmedOnUtc = utcNow;

        RaiseDomainEvent(new OrderConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel(DateTime utcNow)
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.Confirmed)
        {
            return Result.Failure(OrderErrors.CannotCancel);
        }

        Status = OrderStatus.Cancelled;
        CancelledOnUtc = utcNow;

        RaiseDomainEvent(new OrderCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result Deliver(DateTime utcNow)
    {
        if (Status != OrderStatus.Confirmed)
        {
            return Result.Failure(OrderErrors.NotConfirmed);
        }

        Status = OrderStatus.Delivered;
        DeliveredOnUtc = utcNow;

        RaiseDomainEvent(new OrderDeliveredDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete(DateTime utcNow)
    {
        if (Status != OrderStatus.Delivered)
        {
            return Result.Failure(OrderErrors.NotDelivered);
        }

        Status = OrderStatus.Completed;
        CompletedOnUtc = utcNow;

        RaiseDomainEvent(new OrderCompletedDomainEvent(Id));

        return Result.Success();
    }

    public static object PlaceOrder(Name customerName, List<OrderItem> orderItems, DateTime utcNow, Result<Money> result)
    {
        throw new NotImplementedException();
    }
}