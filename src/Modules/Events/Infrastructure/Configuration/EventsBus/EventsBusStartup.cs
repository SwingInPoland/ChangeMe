using Autofac;
using ChangeMe.Modules.Events.IntegrationEvents;
using ChangeMe.Shared.Infrastructure.EventBus;
using Serilog;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.EventsBus;

public static class EventsBusStartup
{
    public static void Initialize(ILogger logger) => SubscribeToIntegrationEvents(logger);

    private static void SubscribeToIntegrationEvents(ILogger logger)
    {
        var eventBus = EventsCompositionRoot.BeginLifetimeScope().Resolve<IEventsBus>();
        SubscribeToIntegrationEvent<SampleIntegrationEvent>(eventBus, logger);
    }

    private static void SubscribeToIntegrationEvent<TIntegrationEvent>(IEventsBus eventBus, ILogger logger)
        where TIntegrationEvent : IntegrationEvent
    {
        logger.Information("Subscribe to {@IntegrationEvent}", typeof(TIntegrationEvent).FullName);
        eventBus.Subscribe(new IntegrationEventGenericHandler<TIntegrationEvent>());
    }
}