using Autofac;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration;

internal static class EventsCompositionRoot
{
    private static IContainer _container;

    internal static void SetContainer(IContainer container) => _container = container;

    internal static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
}