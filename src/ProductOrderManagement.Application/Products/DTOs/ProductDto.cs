using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Application.Products.DTOs;

public record ProductDto(Guid Id, string Name, decimal Price, string Status);


public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto(
            product.Id,
            product.Name.Value,
            product.Price.Amount,
            product.Status.ToString());
    }
}