using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Configuration.Queries;

public abstract record QueryBase<TResult>(Guid Id) : IQuery<TResult>
{
    protected QueryBase() : this(Guid.NewGuid()) { }
}