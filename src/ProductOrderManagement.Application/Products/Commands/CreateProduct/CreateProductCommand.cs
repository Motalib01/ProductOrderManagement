using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, decimal Price) : ICommand;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var name = new Name(command.Name);
        var price = new Money(command.Price, Currency.Dzd); // Default currency
        var product = new Product(Guid.NewGuid(), name, price, Status.Active);

        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}