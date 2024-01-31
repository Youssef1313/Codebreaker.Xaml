namespace System.Linq;

internal static class LinqExtensions
{
    public static IEnumerable<T> Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var item in enumerable)
            action(item);

        return enumerable;
    }

    public static IEnumerable<T> Foreach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
    {
        int i = 0;

        foreach (var item in enumerable)
            action(item, i++);

        return enumerable;
    }
}
