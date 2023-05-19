using ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainEventsDispatcher;
using MediatR;

namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching;

public class DomainEventsDispatcherNotificationHandlerDecorator<TNotification> : INotificationHandler<TNotification>
    where TNotification : INotification
{
    private readonly INotificationHandler<TNotification> _decorated;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public DomainEventsDispatcherNotificationHandlerDecorator(
        IDomainEventsDispatcher domainEventsDispatcher,
        INotificationHandler<TNotification> decorated)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
        _decorated = decorated;
    }

    public async Task Handle(TNotification notification, CancellationToken cancellationToken)
    {
        await _decorated.Handle(notification, cancellationToken);
        await _domainEventsDispatcher.DispatchEventsAsync();
    }
}