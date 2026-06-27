using MediatR;

namespace TakweneMusic.Application.Common.Interfaces;

public interface ICommand
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>, ICommand
{
}
