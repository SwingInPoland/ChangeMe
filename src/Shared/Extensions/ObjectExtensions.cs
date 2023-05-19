namespace ChangeMe.Shared.Extensions;

public static class ObjectExtensions
{
    // for safe cast use "as" operator
    public static T To<T>(this object obj) => (T)obj;
}