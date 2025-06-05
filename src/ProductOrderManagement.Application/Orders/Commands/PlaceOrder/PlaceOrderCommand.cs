// PlaceOrderCommand.cs
using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Application.Orders.Commands.PlaceOrder;

public record PlaceOrderCommand(Name CustomerName, List<OrderItemRequest> Items) : ICommand<Guid>;

public record OrderItemRequest(Guid ProductId, int Quantity);

public sealed class PlaceOrderCommandHandler : ICommandHandler<PlaceOrderCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly PricingService _pricingService;
    private readonly IUnitOfWork _unitOfWork;

    public PlaceOrderCommandHandler(
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        PricingService pricingService,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _pricingService = pricingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
    {
        var pendingOrders = await _orderRepository.GetPendingOrdersByCustomerAsync(command.CustomerName.Value, cancellationToken);
        if (pendingOrders.Any())
        {
            return Result.Failure<Guid>(OrderErrors.PendingOrderExists);
        }

        var productIds = command.Items.Select(item => item.ProductId).ToList();
        var products = await _productRepository.GetProductsByIdsAsync(productIds, cancellationToken);

        foreach (var item in command.Items)
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null || product.Status != Status.Active)
            {
                return Result.Failure<Guid>(OrderErrors.InactiveProduct);
            }

            if (item.Quantity <= 0)
            {
                return Result.Failure<Guid>(OrderErrors.InvalidQuantity);
            }
        }

        var orderItems = command.Items.Select(item =>
        {
            var product = products.First(p => p.Id == item.ProductId);
            return new OrderItem(product.Id, product.Name, product.Price, item.Quantity);
        }).ToList();

        var utcNow = DateTime.UtcNow;
        var result = Order.PlaceOrder(command.CustomerName, orderItems, utcNow, _pricingService);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        var order = result.Value;
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(order.Id);
    }
}