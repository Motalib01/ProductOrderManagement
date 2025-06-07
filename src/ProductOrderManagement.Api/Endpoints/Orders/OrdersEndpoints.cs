using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductOrderManagement.Application.Orders.Commands.PlaceOrder;
using ProductOrderManagement.Application.Orders.Queries.GetOrderById;
using ProductOrderManagement.Application.Orders.Queries.GetOrders;

namespace ProductOrderManagement.Presentation.Endpoints;

public static class OrdersEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/orders").WithTags("Orders");

        // Place order
        group.MapPost("/", async (
                [FromBody] PlaceOrderCommand command,
                [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/api/orders/{result}", null);
            })
            .WithName("PlaceOrder")
            .WithOpenApi();

        // Get orders (paginated)
        group.MapGet("/", async (
                [FromServices] IMediator mediator,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10) =>
            {
                var query = new GetOrdersQuery(page, pageSize);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetOrders")
            .WithOpenApi();

        // Get order by ID
        group.MapGet("/{id}", async (
                Guid id,
                [FromServices] IMediator mediator) =>
            {
                var query = new GetOrderByIdQuery(id);
                var result = await mediator.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetOrderById")
            .WithOpenApi();
    }
}