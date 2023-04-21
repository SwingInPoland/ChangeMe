using System.Reflection;

namespace ChangeMe.Shared.Extensions;

public static class ReflectionExtensions
{
    public static bool IsProtected(this ConstructorInfo constructor) =>
        constructor.IsFamily || constructor.IsFamilyAndAssembly;
}