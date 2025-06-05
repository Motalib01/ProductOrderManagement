using Moq;
using ProductOrderManagement.Application.Orders.Commands.PlaceOrder;
using ProductOrderManagement.Application.Orders.Queries.GetOrders;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Domain.Shared;
using OrderItemRequest = ProductOrderManagement.Application.Orders.Commands.PlaceOrder.OrderItemRequest;

namespace ProductOrderManagement.Tests;

public class PlaceOrderTests
{
    [Fact]
    public async Task Cannot_Add_Inactive_Product_To_Order()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), new Name("Inactive Product"), new Money(100, Currency.Dzd), Status.Inactive);
        var orderItems = new List<OrderItemRequest>
        {
            new OrderItemRequest(product.Id, 1)
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetProductsByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product> { product });

        var handler = new PlaceOrderCommandHandler(
            productRepositoryMock.Object,
            Mock.Of<IOrderRepository>(),
            Mock.Of<PricingService>(),
            Mock.Of<IUnitOfWork>());

        var command = new PlaceOrderCommand(new Name("John Doe"), orderItems);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(OrderErrors.InactiveProduct, result.Error);
    }

    

    [Fact]
    public async Task Cannot_Add_Product_With_Invalid_Quantity()
    {
        // Arrange
        var product = new Product(Guid.NewGuid(), new Name("Valid Product"), new Money(100, Currency.Dzd), Status.Active);
        var orderItems = new List<OrderItemRequest>
        {
            new OrderItemRequest(product.Id, 0) // Invalid quantity
        };

        var productRepositoryMock = new Mock<IProductRepository>();
        productRepositoryMock.Setup(repo => repo.GetProductsByIdsAsync(It.IsAny<IEnumerable<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Product> { product });

        var handler = new PlaceOrderCommandHandler(
            productRepositoryMock.Object,
            Mock.Of<IOrderRepository>(),
            Mock.Of<PricingService>(),
            Mock.Of<IUnitOfWork>());

        var command = new PlaceOrderCommand(new Name("John Doe"), orderItems);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(OrderErrors.InvalidQuantity, result.Error);
    }
}