using Autofac;
using ChangeMe.Shared.EventBus;
using ChangeMe.Shared.Infrastructure.EventBus;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.EventsBus;

internal class EventsBusModule : Module
{
    private readonly IEventsBus? _eventsBus;

    public EventsBusModule(IEventsBus? eventsBus)
    {
        _eventsBus = eventsBus;
    }

    protected override void Load(ContainerBuilder builder)
    {
        if (_eventsBus is not null)
            builder.RegisterInstance(_eventsBus).SingleInstance();
        else
            builder.RegisterType<InMemoryEventBusClient>()
                .As<IEventsBus>()
                .SingleInstance();
    }
}