using MediatR;
using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> 
{
    
}