using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Product;

public static class ProductErrors
{
    public static readonly Error NotActive = new(
        "Order.NotPending",
        "The order must be in a pending state to perform this action.");

    public static readonly Error NotInActive = new(
        "Order.NotConfirmed",
        "The order must be confirmed to perform this action.");

    public static readonly Error NotFound = new(
        "Order.NotDelivered",
        "The order must be delivered to complete it.");

    public static readonly Error CannotCancel = new(
        "Order.CannotCancel",
        "The order cannot be cancelled in its current state.");

    public static readonly Error EmptyOrder = new(
        "Order.Empty",
        "The order must contain at least one item.");
}