namespace ChangeMe.Shared.Extensions;

public static class NullableExtensions
{
    public static bool HasNoValue<T>(this Nullable<T> nullable) where T : struct => !nullable.HasValue;
}