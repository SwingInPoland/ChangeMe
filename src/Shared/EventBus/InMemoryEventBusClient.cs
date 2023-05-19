using ChangeMe.Shared.Infrastructure.EventBus;
using Serilog;

namespace ChangeMe.Shared.EventBus;

public class InMemoryEventBusClient : IEventsBus
{
    private readonly ILogger _logger;

    public InMemoryEventBusClient(ILogger logger)
    {
        _logger = logger;
    }

    public void Dispose() => GC.SuppressFinalize(this);

    public async Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent
    {
        _logger.Information("Publishing {Event}", @event.GetType().FullName);
        await InMemoryEventBus.Instance.Publish(@event);
    }

    public void Subscribe<TIntegrationEvent>(IIntegrationEventHandler<TIntegrationEvent> handler)
        where TIntegrationEvent : IntegrationEvent =>
        InMemoryEventBus.Instance.Subscribe(handler);

    public void StartConsuming() { }
}