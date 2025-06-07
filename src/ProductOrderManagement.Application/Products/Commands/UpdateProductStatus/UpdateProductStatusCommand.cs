using ProductOrderManagement.Application.Abstraction.Messaging;
using ProductOrderManagement.Domain.Product;

namespace ProductOrderManagement.Application.Products.Commands.UpdateProductStatus;

public record UpdateProductStatusCommand(Guid Id, Status Status) : ICommand;