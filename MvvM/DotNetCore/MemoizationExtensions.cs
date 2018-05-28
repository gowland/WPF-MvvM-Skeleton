using System;

namespace DotNetCore
{
    public static class MemoizationExtensions
    {
        // Lifted directly from: https://stackoverflow.com/a/20544642/20570
        public static Func<TA, TR> ThreadsafeMemoize<TA, TR>(this Func<TA, TR> f)
        {
            var cache = new System.Collections.Concurrent.ConcurrentDictionary<TA, TR>();

            return argument => cache.GetOrAdd(argument, f);
        }
    }
}