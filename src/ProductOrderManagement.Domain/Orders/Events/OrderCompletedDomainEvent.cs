using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order.Events;

public sealed record OrderCompletedDomainEvent(Guid OrderId):IDomainEvent;