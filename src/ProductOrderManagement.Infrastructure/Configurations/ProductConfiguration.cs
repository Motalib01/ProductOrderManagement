using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasConversion(name => name.Value, value => new Name(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasConversion(price => price.Amount, amount => new Money(amount, Currency.Dzd)) // Default currency
            .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired();
    }
}
internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CustomerName)
            .HasConversion(name => name.Value, value => new Name(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(o => o.TotalPrice)
            .HasConversion(price => price.Amount, amount => new Money(amount, Currency.Dzd)) // Default currency
            .IsRequired();

        builder.Property(o => o.Status)
            .IsRequired();

        builder.Property(o => o.CreatedOnUtc)
            .IsRequired();

        // Configure the navigation property for OrderItems
        builder.HasMany(o => o.Items)
            .WithOne() // Assuming OrderItem does not have a navigation property back to Order
            .HasForeignKey("OrderId") // Foreign key column in the OrderItem table
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete if the order is deleted
    }
}
internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.HasKey(oi => oi.ProductId);

        builder.Property(oi => oi.ProductName)
            .HasConversion(name => name.Value, value => new Name(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(oi => oi.Price)
            .HasConversion(price => price.Amount, amount => new Money(amount, Currency.Dzd)) // Default currency
            .IsRequired();

        builder.Property(oi => oi.Quantity)
            .IsRequired();
    }
}