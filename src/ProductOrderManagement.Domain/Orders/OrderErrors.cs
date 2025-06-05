using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Domain.Order;

public static class OrderErrors
{
    public static readonly Error NotPending = new(
        "Order.NotPending",
        "The order must be in a pending state to perform this action.");

    public static readonly Error NotConfirmed = new(
        "Order.NotConfirmed",
        "The order must be confirmed to perform this action.");

    public static readonly Error NotDelivered = new(
        "Order.NotDelivered",
        "The order must be delivered to complete it.");

    public static readonly Error CannotCancel = new(
        "Order.CannotCancel",
        "The order cannot be cancelled in its current state.");

    public static readonly Error EmptyOrder = new(
        "Order.Empty",
        "The order must contain at least one item.");

    public static readonly Error InactiveProduct = new(
        "Order.Empty",
        "The Product must have active product.");

    public static readonly Error InvalidQuantity = new(
        "Order.Empty",
        "The order must have valide quantity.");

    public static readonly Error PendingOrderExists = new(
        "Order.Empty",
        "The order must clear the epending.");


}