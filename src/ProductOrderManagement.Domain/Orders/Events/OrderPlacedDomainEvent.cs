using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order.Events;

public sealed record OrderPlacedDomainEvent(Guid OrderId):IDomainEvent;