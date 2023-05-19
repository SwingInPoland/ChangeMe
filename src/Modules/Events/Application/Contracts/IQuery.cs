using MediatR;

namespace ChangeMe.Modules.Events.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult> { }