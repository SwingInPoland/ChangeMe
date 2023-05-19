namespace ChangeMe.Modules.Events.Application.Contracts;

public abstract record CommandBase(Guid Id) : ICommand
{
    protected CommandBase() : this(Guid.NewGuid()) { }
}

public abstract record CommandBase<TResult>(Guid Id) : ICommand<TResult>
{
    protected CommandBase() : this(Guid.NewGuid()) { }
}