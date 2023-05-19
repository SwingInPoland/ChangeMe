using ChangeMe.Shared.Infrastructure.EventBus;

namespace ChangeMe.Shared.EventBus;

public sealed class InMemoryEventBus
{
    static InMemoryEventBus() { }

    private InMemoryEventBus()
    {
        _handlersDictionary = new Dictionary<string, List<IIntegrationEventHandler>>();
    }

    public static InMemoryEventBus Instance { get; } = new();

    private readonly IDictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

    public void Subscribe<TIntegrationEvent>(IIntegrationEventHandler<TIntegrationEvent> handler)
        where TIntegrationEvent : IntegrationEvent
    {
        var eventType = typeof(TIntegrationEvent).FullName;
        if (eventType is null)
            return;

        if (_handlersDictionary.TryGetValue(eventType, out var handlers))
            handlers.Add(handler);
        else
            _handlersDictionary.Add(eventType, new List<IIntegrationEventHandler> { handler });
    }

    public async Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
    {
        var eventType = @event.GetType().FullName;

        if (eventType is null)
            return;

        var integrationEventHandlers = _handlersDictionary[eventType];

        foreach (var integrationEventHandler in integrationEventHandlers)
            if (integrationEventHandler is IIntegrationEventHandler<TIntegrationEvent> handler)
                await handler.Handle(@event);
    }
}