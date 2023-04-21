using System.Collections.Immutable;

namespace ChangeMe.Shared.Extensions;

public static class EnumerableExtensions
{
    public static bool NotContains<T>(this IEnumerable<T> enumerable, T item) => !enumerable.Contains(item);
    public static bool NotAny<T>(this IEnumerable<T> enumerable) => !enumerable.Any();
    public static IReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> collection) => collection.ToImmutableArray();

    public static IEnumerable<T> ExceptOne<T>(this IEnumerable<T> source, T? element) =>
        element is null ? source : source.Except(new[] { element });
}