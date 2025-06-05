using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Infrastructure;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        // Seed Products
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product(Guid.NewGuid(), new Name("Laptop"), new Money(1000, Currency.Dzd), Status.Active),
                new Product(Guid.NewGuid(), new Name("Mouse"), new Money(50, Currency.Dzd), Status.Inactive),
                new Product(Guid.NewGuid(), new Name("Keyboard"), new Money(75, Currency.Dzd), Status.Active)
            };

            context.Products.AddRange(products);
        }

        // Seed Orders
        if (!context.Orders.Any())
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem(Guid.NewGuid(), new Name("Laptop"), new Money(1000, Currency.Dzd), 1),
                new OrderItem(Guid.NewGuid(), new Name("Keyboard"), new Money(75, Currency.Dzd), 2)
            };

            var totalPrice = new Money(1150, Currency.Dzd);

            var orderResult = Order.Create(
                Guid.NewGuid(),
                new Name("John Doe"),
                orderItems,
                totalPrice,
                OrderStatus.Pending,
                DateTime.UtcNow
            );

            if (orderResult.IsSuccess)
            {
                context.Orders.Add(orderResult.Value);
            }
        }

        context.SaveChanges();
    }
}