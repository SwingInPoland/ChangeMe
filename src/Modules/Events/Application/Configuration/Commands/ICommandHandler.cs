using ChangeMe.Modules.Events.Application.Contracts;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Configuration.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand { }

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult> { }