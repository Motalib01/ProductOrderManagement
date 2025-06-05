using Microsoft.EntityFrameworkCore;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Infrastructure.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
    }


    public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(product => ids.Contains(product.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedList<Product>> GetPaginatedAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Products.CountAsync(cancellationToken);
        var items = await _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return PaginatedList<Product>.ToPaginatedList(items, page, pageSize, totalCount);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
}

internal sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<List<Order>> GetPendingOrdersByCustomerAsync(string customerName, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Where(order => order.CustomerName.Value == customerName && order.Status == OrderStatus.Pending)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginatedList<Order>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await _context.Orders.CountAsync(cancellationToken);
        var items = await _context.Orders
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return PaginatedList<Order>.ToPaginatedList(items, page, pageSize, totalCount);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

}