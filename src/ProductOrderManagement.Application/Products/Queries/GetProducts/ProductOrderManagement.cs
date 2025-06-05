using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Products.DTOs;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Application.Products.Queries.GetProducts;

public record GetProductsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<ProductDto>>, IQuery<Result<PaginatedList<ProductDto>>>;


internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<PaginatedList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository; 
    }

    public async Task<Result<Result<PaginatedList<ProductDto>>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetPaginatedAsync(query.Page, query.PageSize, cancellationToken);

        var productDtos = products.Items.Select(ProductMapper.ToDto).ToList();

        var paginatedDtos = PaginatedList<ProductDto>.ToPaginatedList(productDtos, query.Page, query.PageSize, products.TotalCount);

        return Result.Success(paginatedDtos);
    }
}