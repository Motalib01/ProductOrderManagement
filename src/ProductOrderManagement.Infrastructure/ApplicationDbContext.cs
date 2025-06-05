using Microsoft.EntityFrameworkCore;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProductOrderManagement.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
    }
}