using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Products.DTOs;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Application.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id, cancellationToken);
        if (product == null)
        {
            return Result.Failure<ProductDto>(ProductErrors.NotFound);
        }

        return Result.Success(ProductMapper.ToDto(product));
    }
}