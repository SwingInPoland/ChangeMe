using System.Collections.Immutable;

namespace ChangeMe.Shared.Extensions;

public static class EnumerableExtensions
{
    public static bool NotContains<T>(this IEnumerable<T> enumerable, T item) => !enumerable.Contains(item);

    public static bool NotAny<T>(this IEnumerable<T> enumerable) => !enumerable.Any();

    public static bool NotAny<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => !enumerable.Any(predicate);

    public static bool NotAll<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) => !enumerable.All(predicate);

    public static IReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> collection) => collection.ToImmutableArray();

    public static IEnumerable<T> ExceptOne<T>(this IEnumerable<T> source, T? element) =>
        element is null ? source : source.Except(new[] { element });

    public static bool IsUnique<T>(this IEnumerable<T> enumerable)
    {
        var hashSet = new HashSet<T>();
        return enumerable.All(item => hashSet.Add(item));
    }
}