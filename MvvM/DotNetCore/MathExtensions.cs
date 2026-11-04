namespace DotNetCore;

public static class MathExtensions
{
    // Lifted directly from: https://stackoverflow.com/a/10738416/20570
    public static double? Median<TColl, TValue>(this IEnumerable<TColl> source, Func<TColl, TValue> selector) =>
        source.Select(selector).Median();

    // Lifted directly from: https://stackoverflow.com/a/10738416/20570
    public static double? Median<T>(this IEnumerable<T> source)
    {
        if (Nullable.GetUnderlyingType(typeof(T)) != null)
        {
            source = source.Where(x => x != null);
        }

        var count = source.Count();
        if (count == 0)
        {
            return null;
        }

        source = source.OrderBy(n => n);

        var midpoint = count / 2;
        return count % 2 == 0
            ? (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0
            : Convert.ToDouble(source.ElementAt(midpoint));
    }

    public static double StdDev(this IEnumerable<double> values)
    {
        var list = values as IReadOnlyCollection<double> ?? values.ToList();
        if (list.Count <= 1)
        {
            return 0;
        }

        var avg = list.Average();
        var sum = list.Sum(d => (d - avg) * (d - avg));

        return Math.Sqrt(sum / list.Count);
    }

    public static long SumOfSequence(this long n) =>
        n % 2 == 0 ? (n + 1) * (n / 2) : n * ((n + 1) / 2);

    // Lifted directly from: http://stackoverflow.com/a/239877/20570
    public static List<long> Factors(long number)
    {
        List<long> factors = [];
        var max = (int)Math.Sqrt(number);
        for (var factor = 1; factor <= max; ++factor)
        {
            if (number % factor == 0)
            {
                factors.Add(factor);
                if (factor != number / factor)
                {
                    factors.Add(number / factor);
                }
            }
        }

        return factors;
    }

    public static long GreatestCommonDivisor(this long a, long b) =>
        Factors(a).Intersect(Factors(b)).Max();

    public static long LeastCommonMultiple(this long a, long b) =>
        a * b / a.GreatestCommonDivisor(b);

    public static long LeastCommonMultiple(this IEnumerable<long> values) =>
        values.Aggregate(1L, (a, b) => a.LeastCommonMultiple(b));

    public static bool IsSequential(this IEnumerable<int> values) =>
        values.Zip(values.Skip(1), (a, b) => a + 1 == b).All(x => x);
}
