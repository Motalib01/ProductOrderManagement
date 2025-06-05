using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order.Events;

public sealed record OrderCancelledDomainEvent(Guid OrderId):IDomainEvent;