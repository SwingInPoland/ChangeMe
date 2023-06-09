﻿using Autofac;
using ChangeMe.Modules.Events.Infrastructure.Outbox;
using ChangeMe.Shared.Application.Events;
using ChangeMe.Shared.Application.Outbox;
using ChangeMe.Shared.Infrastructure;
using ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainNotificationsMapper;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Outbox;

internal class OutboxModule : Module
{
    private readonly BiDictionary<string, Type> _domainNotificationsMap;

    public OutboxModule(BiDictionary<string, Type> domainNotificationsMap)
    {
        _domainNotificationsMap = domainNotificationsMap;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<OutboxAccessor>()
            .As<IOutbox>()
            .FindConstructorsWith(new AllConstructorFinder())
            .InstancePerLifetimeScope();

        CheckMappings();

        builder.RegisterType<DomainNotificationsMapper>()
            .As<IDomainNotificationsMapper>()
            .FindConstructorsWith(new AllConstructorFinder())
            .WithParameter("domainNotificationsMap", _domainNotificationsMap)
            .SingleInstance();
    }

    private void CheckMappings()
    {
        var domainEventNotifications = Assemblies.Application
            .GetTypes()
            .Where(x => x.GetInterfaces().Contains(typeof(IDomainEventNotification)))
            .ToList();

        var notMappedNotifications = new List<Type>();
        foreach (var domainEventNotification in domainEventNotifications)
        {
            _domainNotificationsMap.TryGetBySecond(domainEventNotification, out var name);

            if (name is null)
                notMappedNotifications.Add(domainEventNotification);
        }

        if (notMappedNotifications.Any())
            throw new ApplicationException(
                $"Domain Event Notifications {notMappedNotifications.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
    }
}