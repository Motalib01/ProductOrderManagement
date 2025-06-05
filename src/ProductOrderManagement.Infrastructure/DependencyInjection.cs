using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Order;
using ProductOrderManagement.Domain.Product;
using ProductOrderManagement.Infrastructure.Clock;
using ProductOrderManagement.Infrastructure.Repositories;
using System.Configuration;

namespace ProductOrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);

       
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("ProductOrderManagementDb"));

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        
    }

}