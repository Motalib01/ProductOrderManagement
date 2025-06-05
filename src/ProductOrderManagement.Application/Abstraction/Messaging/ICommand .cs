using MediatR;
using ProductOrderManagement.Domain.Abstractions;

namespace ProductOrderManagement.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}