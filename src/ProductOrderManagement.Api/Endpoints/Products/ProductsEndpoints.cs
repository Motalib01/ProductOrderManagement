using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOrderManagement.Application.Products.Commands.CreateProduct;
using ProductOrderManagement.Application.Products.Commands.UpdateProductStatus;
using ProductOrderManagement.Application.Products.Queries.GetProductById;
using ProductOrderManagement.Application.Products.Queries.GetProducts;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Presentation.Endpoints;

public static class ProductsEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        // Create a product
        group.MapPost("/", async (
            [FromBody] CreateProductCommand command,
            [FromServices] IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Created($"/api/products/{command.Name}", null);
        })
        .WithName("CreateProduct")
        .WithOpenApi();

        // Update product status
        group.MapPut("/{id}/status", async (
            Guid id,
            [FromQuery] Status status,
            [FromServices] IMediator mediator) =>
        {
            var command = new UpdateProductStatusCommand(id, status);
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("UpdateProductStatus")
        .WithOpenApi();

        // Get paginated list of products
        group.MapGet("/", async (
            [FromServices] IMediator mediator,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) =>
        {
            var query = new GetProductsQuery(page, pageSize);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .WithOpenApi();

        // Get product by ID
        group.MapGet("/{id}", async (
            Guid id,
            [FromServices] IMediator mediator) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProductById")
        .WithOpenApi();
    }
}
