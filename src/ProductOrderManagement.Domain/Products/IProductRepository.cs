using ProductOrderManagement.Application.Abstractions;

namespace ProductOrderManagement.Domain.Product;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Product product);
    Task<List<Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<PaginatedList<Product>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}