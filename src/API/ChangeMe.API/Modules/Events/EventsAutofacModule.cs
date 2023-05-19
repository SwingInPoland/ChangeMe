using Autofac;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Infrastructure;

namespace ChangeMe.API.Modules.Events;

public class EventsAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder) =>
        builder.RegisterType<EventsModule>()
            .As<IEventsModule>()
            .InstancePerLifetimeScope();
}