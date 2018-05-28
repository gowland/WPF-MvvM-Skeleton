using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore
{
    public static class StringExtensions
    {
        private static IEnumerable<int> SplitStringToInts(string numbers)
        {
            return numbers.Split(' ')
                .Select(s => int.Parse(s));
        }

        public static string GetUntil(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return text;
        }
    }
}