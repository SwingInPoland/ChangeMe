using Events.ArchTests.SeedWork;
using NetArchTest.Rules;
using Xunit;

namespace Events.ArchTests.Api;

public class ApiTests : TestBase
{
    [Fact]
    public void EventsApi_DoesNotHaveDependency_ToOtherModules()
    {
        var otherModules = new string[]
        {
            // EventsNamespace - like it, but not it
        };

        var result = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("ChangeMe.API.Modules.Events")
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult();

        AssertArchTestResult(result);
    }
}