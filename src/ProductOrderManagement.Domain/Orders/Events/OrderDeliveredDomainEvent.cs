using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order.Events;

public sealed record OrderDeliveredDomainEvent(Guid OrderId):IDomainEvent;