using MediatR;
using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}