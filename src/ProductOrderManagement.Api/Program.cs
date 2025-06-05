using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using ProductOrderManagement.Application;
using ProductOrderManagement.Infrastructure;
using ProductOrderManagement.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product & Order Management API",
        Version = "v1",
        Description = "A simple API for managing products and customer orders.",
        Contact = new OpenApiContact
        {
            Name = "Abd El-Motalib Chemouri",
            Email = "abdelmotaliv.chemouri@gmail.com"
        }
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product & Order Management API v1");
    });
}

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var errorMessage = exception?.Message ?? "An unexpected error occurred.";

        await context.Response.WriteAsync(new
        {
            error = errorMessage
        }.ToString());
    });
});

app.UseHttpsRedirection();

ProductsEndpoints.MapEndpoints(app);
OrdersEndpoints.MapEndpoints(app);

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(dbContext);
}

app.Run();