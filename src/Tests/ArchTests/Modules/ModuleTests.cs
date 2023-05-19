using System.Reflection;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Infrastructure;
using Events.ArchTests.SeedWork;
using MediatR;
using NetArchTest.Rules;
using Xunit;

namespace Events.ArchTests.Modules;

public class ModuleTests : TestBase
{
    [Fact]
    public void EventsModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        var otherModules = new string[]
        {
            // EventsNamespace - like it, but not it
        };

        var eventsAssemblies = new List<Assembly>
        {
            typeof(IEventsModule).Assembly,
            typeof(EventLocation).Assembly,
            typeof(EventsContext).Assembly
        };

        var result = Types.InAssemblies(eventsAssemblies)
            .That()
            .DoNotImplementInterface(typeof(INotificationHandler<>))
            .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
            .And().DoNotHaveName("EventsBusStartup")
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult();

        AssertArchTestResult(result);
    }
}