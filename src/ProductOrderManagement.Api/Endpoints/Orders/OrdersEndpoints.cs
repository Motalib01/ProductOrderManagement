using Microsoft.AspNetCore.Mvc;
using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Application.Abstractions;
using ProductOrderManagement.Application.Orders.Commands.PlaceOrder;
using ProductOrderManagement.Application.Orders.DTOs;
using ProductOrderManagement.Application.Orders.Queries.GetOrders;

namespace ProductOrderManagement.Presentation.Endpoints;

public static class OrdersEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/orders").WithTags("Orders");



        group.MapPost("/", async (
            [FromBody] PlaceOrderCommand command,
            [FromServices] ICommandHandler<PlaceOrderCommand, Guid> handler) =>
        {
            var orderId = await handler.Handle(command, CancellationToken.None);
            return Results.Created($"/api/orders/{orderId}", null);
        })
        .WithName("PlaceOrder")
        .WithOpenApi();



        group.MapGet("/", async (
            [FromServices] IQueryHandler<GetOrdersQuery, PaginatedList<OrderDto>> handler,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10) =>
        {
            var query = new GetOrdersQuery(page, pageSize);
            var result = await handler.Handle(query, CancellationToken.None);
            return Results.Ok(result);
        })
        .WithName("GetOrders")
        .WithOpenApi();



        group.MapGet("/{id}", async (
            Guid id,
            [FromServices] IQueryHandler<GetOrderByIdQuery, OrderDto> handler) =>
        {
            var query = new GetOrderByIdQuery(id);
            var result = await handler.Handle(query, CancellationToken.None);
            return Results.Ok(result);
        })
        .WithName("GetOrderById")
        .WithOpenApi();
    }
}
