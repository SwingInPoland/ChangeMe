using Autofac;
using ChangeMe.Modules.Events.Infrastructure.Configuration.DataAccess;
using ChangeMe.Modules.Events.Infrastructure.Configuration.EventsBus;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Logging;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Mediation;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Outbox;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Quartz;
using ChangeMe.Shared.Application;
using ChangeMe.Shared.Infrastructure;
using ChangeMe.Shared.Infrastructure.EventBus;
using Serilog;
using Serilog.Extensions.Logging;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration;

public class EventsStartup
{
    private static IContainer _container;

    public static void Initialize(
        string connectionString,
        IExecutionContextAccessor executionContextAccessor,
        ILogger logger,
        IEventsBus? eventsBus,
        long? internalProcessingPoolingInterval = null)
    {
        var moduleLogger = logger.ForContext("Module", "Events");

        ConfigureCompositionRoot(
            connectionString,
            executionContextAccessor,
            moduleLogger,
            eventsBus);

        QuartzStartup.Initialize(moduleLogger, internalProcessingPoolingInterval);

        EventsBusStartup.Initialize(moduleLogger);
    }

    public static void Stop() => QuartzStartup.StopQuartz();

    private static void ConfigureCompositionRoot(
        string connectionString,
        IExecutionContextAccessor executionContextAccessor,
        ILogger logger,
        IEventsBus? eventsBus)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "Meetings")));

        var loggerFactory = new SerilogLoggerFactory(logger);
        containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
        containerBuilder.RegisterModule(new ProcessingModule());
        containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
        containerBuilder.RegisterModule(new MediatorModule());

        var domainNotificationsMap = new BiDictionary<string, Type>();
        // domainNotificationsMap.Add("MemberCreatedNotification", typeof(MemberCreatedNotification));
        containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

        containerBuilder.RegisterModule(new QuartzModule());

        containerBuilder.RegisterInstance(executionContextAccessor);

        _container = containerBuilder.Build();

        EventsCompositionRoot.SetContainer(_container);
    }
}