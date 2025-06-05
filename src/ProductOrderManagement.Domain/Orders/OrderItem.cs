using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Domain.Order;

public sealed class OrderItem
{
    public OrderItem(Guid productId, Name productName, Money price, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        ProductId = productId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    public Guid ProductId { get; private set; }
    public Name ProductName { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }
}