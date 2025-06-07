using ProductOrderManagement.Application.Abstraction.Messaging;

namespace ProductOrderManagement.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, decimal Price) : ICommand;