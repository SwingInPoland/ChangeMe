namespace ChangeMe.Shared.Extensions;

public static class NumberExtensions
{
    public static DateTimeOffset ToDateTimeOffset(this long unixTimeSeconds) =>
        DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
}