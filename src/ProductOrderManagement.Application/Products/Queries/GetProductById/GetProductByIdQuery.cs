using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Products.DTOs;

namespace ProductOrderManagement.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;