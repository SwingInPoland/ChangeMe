namespace ChangeMe.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);
    public static bool IsNotNullOrWhiteSpace(this string? value) => !string.IsNullOrWhiteSpace(value);
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);
    public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);
    public static bool NotContains(this string value, string @string) => !value.Contains(@string);
    public static bool NotContains(this string value, char @char) => !value.Contains(@char);
    public static string NormalizeUpperInvariant(this string value) => value.Normalize().ToUpperInvariant();
}