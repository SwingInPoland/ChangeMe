using ChangeMe.Shared.Domain;

namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsAccessor;

public interface IDomainEventsAccessor
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

    void ClearAllDomainEvents();
}