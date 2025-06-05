namespace ProductOrderManagement.Domain.Order;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    InProgress = 3,
    Completed = 4,
    Cancelled = 5,
    Delivered = 6
}