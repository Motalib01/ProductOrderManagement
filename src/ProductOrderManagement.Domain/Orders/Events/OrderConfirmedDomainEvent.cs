using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order.Events;

public sealed record OrderConfirmedDomainEvent(Guid OrderId):IDomainEvent;