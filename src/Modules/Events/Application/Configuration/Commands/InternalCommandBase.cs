using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Configuration.Commands;

public abstract record InternalCommandBase(Guid Id) : ICommand;

public abstract record InternalCommandBase<TResult>(Guid Id) : ICommand<TResult>
{
    protected InternalCommandBase() : this(Guid.NewGuid()) { }
}