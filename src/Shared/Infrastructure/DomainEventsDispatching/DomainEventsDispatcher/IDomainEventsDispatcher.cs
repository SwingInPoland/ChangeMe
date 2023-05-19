namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsDispatcher;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}