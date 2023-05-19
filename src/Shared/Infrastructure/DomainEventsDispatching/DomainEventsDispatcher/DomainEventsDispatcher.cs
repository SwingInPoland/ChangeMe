using Autofac;
using Autofac.Core;
using ChangeMe.Shared.Application.Events;
using ChangeMe.Shared.Application.Outbox;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsAccessor;
using ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainNotificationsMapper;
using ChangeMe.Shared.Infrastructure.Serialization;
using MediatR;
using Newtonsoft.Json;

namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsDispatcher;

public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILifetimeScope _scope;
    private readonly IOutbox _outbox;
    private readonly IDomainEventsAccessor _domainEventsProvider;
    private readonly IDomainNotificationsMapper _domainNotificationsMapper;

    public DomainEventsDispatcher(
        IMediator mediator,
        ILifetimeScope scope,
        IOutbox outbox,
        IDomainEventsAccessor domainEventsProvider,
        IDomainNotificationsMapper domainNotificationsMapper)
    {
        _mediator = mediator;
        _scope = scope;
        _outbox = outbox;
        _domainEventsProvider = domainEventsProvider;
        _domainNotificationsMapper = domainNotificationsMapper;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEvents = _domainEventsProvider.GetAllDomainEvents();

        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
        foreach (var domainEvent in domainEvents)
        {
            var domainEvenNotificationType = typeof(IDomainEventNotification<>);
            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
            {
                new NamedParameter("id", domainEvent.Id),
                new NamedParameter("domainEvent", domainEvent)
            });

            if (domainNotification is not null)
                // Should not be null because of the if statement above
                domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
        }

        _domainEventsProvider.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);

        foreach (var domainEventNotification in domainEventNotifications)
        {
            var type = _domainNotificationsMapper.GetName(domainEventNotification.GetType());
            var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            });

            var outboxMessage = new OutboxMessage(
                domainEventNotification.Id,
                domainEventNotification.DomainEvent.OccurredOn,
                type,
                data);

            _outbox.Add(outboxMessage);
        }
    }
}