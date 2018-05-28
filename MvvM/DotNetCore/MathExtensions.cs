using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore
{
    public static class MathExtensions
    {
        // Lifted directly from: https://stackoverflow.com/a/10738416/20570
        public static double? Median<TColl, TValue>(
            this IEnumerable<TColl> source,
            Func<TColl, TValue> selector)
        {
            return source.Select<TColl, TValue>(selector).Median();
        }

        // Lifted directly from: https://stackoverflow.com/a/10738416/20570
        public static double? Median<T>(this IEnumerable<T> source)
        {
            if (Nullable.GetUnderlyingType(typeof(T)) != null)
                source = source.Where(x => x != null);

            int count = source.Count();
            if (count == 0)
                return null;

            source = source.OrderBy(n => n);

            int midpoint = count / 2;
            if (count % 2 == 0)
                return (Convert.ToDouble(source.ElementAt(midpoint - 1)) + Convert.ToDouble(source.ElementAt(midpoint))) / 2.0;
            else
                return Convert.ToDouble(source.ElementAt(midpoint));
        }

        public static double StdDev(this IEnumerable<double> values)
        {
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }

        public static long SumOfSequence(this long n)
        {
            return (n % 2 == 0) ? ((n + 1) * (n / 2)) : (n * ((n + 1) / 2));
        }

        // Lifted directly from: http://stackoverflow.com/a/239877/20570
        public static List<long> Factors(long number)
        {
            List<long> factors = new List<long>();
            int max = (int)Math.Sqrt(number);
            for (int factor = 1; factor <= max; ++factor)
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

        public static long GreatestCommonDivisor(this long a, long b)
        {
            return Factors(a).Intersect(Factors(b)).Max();
        }

        public static long LeastCommonMultiple(this long a, long b)
        {
            return a * b / a.GreatestCommonDivisor(b);
        }

        public static long LeastCommonMultiple(this IEnumerable<long> values)
        {
            return values.Aggregate(1L, (a, b) => a.LeastCommonMultiple(b));
        }

        public static bool IsSequential(this IEnumerable<int> values)
        {
            return values.Zip(values.Skip(1), (a, b) => (a + 1) == b).All(x => x);
        }
    }
}