namespace ChangeMe.Shared.Infrastructure.EventBus;

public interface IEventsBus : IDisposable
{
    Task Publish<TIntegrationEvent>(TIntegrationEvent @event) where TIntegrationEvent : IntegrationEvent;

    void Subscribe<TIntegrationEvent>(IIntegrationEventHandler<TIntegrationEvent> handler)
        where TIntegrationEvent : IntegrationEvent;

    void StartConsuming();
}