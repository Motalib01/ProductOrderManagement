using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Products.DTOs;

namespace ProductOrderManagement.Application.Products.Queries.GetProducts;

public record GetProductsQuery(int Page = 1, int PageSize = 10) : IQuery<PaginatedList<ProductDto>>;