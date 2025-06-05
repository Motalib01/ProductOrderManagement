using ProductOrderManagement.Application.Abstractions;

namespace ProductOrderManagement.Domain.Order;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Order>> GetPendingOrdersByCustomerAsync(string customerName, CancellationToken cancellationToken = default);
    Task<PaginatedList<Order>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    void Add(Order order);
}