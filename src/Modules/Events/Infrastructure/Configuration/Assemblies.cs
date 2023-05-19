using System.Reflection;
using ChangeMe.Modules.Events.Application.Configuration.Commands;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
}