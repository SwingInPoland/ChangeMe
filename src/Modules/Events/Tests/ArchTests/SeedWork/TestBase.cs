using System.Reflection;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Modules.Events.Infrastructure;
using ChangeMe.Shared.Extensions;
using NetArchTest.Rules;
using Xunit;

namespace ChangeMe.Modules.Events.ArchTests.SeedWork;

public abstract class TestBase
{
    protected static Assembly ApplicationAssembly => typeof(CommandBase).Assembly;

    protected static Assembly DomainAssembly => typeof(SingleEvent).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(EventsContext).Assembly;

    protected static void AssertAreImmutable(IEnumerable<Type> types)
    {
        IList<Type> failingTypes = new List<Type>();
        foreach (var type in types)
        {
            if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var field in fields)
                if (field.FieldType.FullName.StartsWith("System.Collections") &&
                    field.FieldType.Name.NotContains("ReadOnly") && field.FieldType.Name.NotContains("Immutable"))
                {
                    failingTypes.Add(type);
                    break;
                }
        }

        AssertFailingTypes(failingTypes);
    }

    protected static void AssertFailingTypes(IEnumerable<Type>? types) =>
        Assert.Empty(types ?? Enumerable.Empty<Type>());

    protected static void AssertArchTestResult(TestResult result) => AssertFailingTypes(result.FailingTypes);
}