namespace DotNetCore;

public static class EnumerableExtensions
{
    public static int IndexOf<T>(this IEnumerable<T> source, T value)
    {
        var index = 0;
        var comparer = EqualityComparer<T>.Default;
        foreach (var item in source)
        {
            if (comparer.Equals(item, value))
            {
                return index;
            }

            index++;
        }

        return -1;
    }

    public static IEnumerable<int> IndicesOf<T>(this IEnumerable<T> list, T element) =>
        list.Select((value, index) => (value, index))
            .Where(tuple => Equals(tuple.value, element))
            .Select(tuple => tuple.index);

    public static int NthIndexOf<T>(this IEnumerable<T> list, T element, int n)
    {
        var indices = list.IndicesOf(element).ToList();
        return n > 0 && indices.Count >= n ? indices[n - 1] : -1;
    }

    public static T UnlikeValue<T, U>(this IEnumerable<T> numbers, Func<T, U> groupMethod) =>
        numbers.GroupBy(groupMethod)
            .Where(g => g.Count() == 1)
            .Select(g => g.Single())
            .Single();

    // Lifted from: https://stackoverflow.com/a/3670089/20570
    public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2) where T : notnull =>
        list1.ScrambledEquals(list2, EqualityComparer<T>.Default);

    // Lifted from: https://stackoverflow.com/a/3670089/20570
    public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2, IEqualityComparer<T> comparer) where T : notnull
    {
        var cnt = new Dictionary<T, int>(comparer);
        foreach (var s in list1)
        {
            cnt[s] = cnt.GetValueOrDefault(s) + 1;
        }

        foreach (var s in list2)
        {
            if (!cnt.TryGetValue(s, out var count))
            {
                return false;
            }

            cnt[s] = count - 1;
        }

        return cnt.Values.All(c => c == 0);
    }

    public static IEnumerable<T> SkipUntil<T>(this IEnumerable<T> list, Func<T, bool> predicate) =>
        list.SkipWhile(item => !predicate(item));

    public static IEnumerable<IEnumerable<T>> Pairs<T>(this IEnumerable<T> source)
    {
        var count = source.Count();
        return source
            .Take(count - 1)
            .SelectMany((element, index) =>
                source
                    .Skip(index + 1)
                    .Take(1)
                    .Select(c => new[] { element, c }));
    }

    /// <summary>
    ///   Returns all combinations of a chosen amount of selected elements in the sequence.
    /// </summary>
    /// <typeparam name = "T">The type of the elements of the input sequence.</typeparam>
    /// <param name = "source">The source for this extension method.</param>
    /// <param name = "select">The amount of elements to select for every combination.</param>
    /// <param name = "repetition">True when repetition of elements is allowed.</param>
    /// <returns>All combinations of a chosen amount of selected elements in the sequence.</returns>
    public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int select, bool repetition = false) =>
        select == 0
            ? [[]]
            : source.SelectMany((element, index) =>
                source
                    .Skip(repetition ? index : index + 1)
                    .Combinations(select - 1, repetition)
                    .Select(c => new[] { element }.Concat(c)));

    public static IEnumerable<T> NullAsEmpty<T>(this IEnumerable<T>? list) => list ?? [];

    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        foreach (var item in list)
        {
            action(item);
        }
    }
}
