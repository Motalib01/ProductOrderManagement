using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Application.Products.Commands.UpdateProductStatus;

internal sealed class UpdateProductStatusCommandHandler : ICommandHandler<UpdateProductStatusCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductStatusCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProductStatusCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product == null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        product.UpdateStatus(command.Status);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}