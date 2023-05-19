using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsAccessor;

public class DomainEventsAccessor : IDomainEventsAccessor
{
    private readonly DbContext _context;

    public DomainEventsAccessor(DbContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents() =>
        _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any())
            // Should not be null because of the Where clause
            .SelectMany(x => x.Entity.DomainEvents)
            .ToReadOnly();

    public void ClearAllDomainEvents()
    {
        var domainEntities = _context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents is not null && x.Entity.DomainEvents.Any());

        foreach (var entity in domainEntities)
            entity.Entity.ClearDomainEvents();
    }
}