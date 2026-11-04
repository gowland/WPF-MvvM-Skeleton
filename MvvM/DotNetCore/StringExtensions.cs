namespace DotNetCore;

public static class StringExtensions
{
    private static IEnumerable<int> SplitStringToInts(string numbers) =>
        numbers.Split(' ').Select(int.Parse);

    public static string? GetUntil(this string? text, string stopAt = "-")
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            var charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

            if (charLocation > 0)
            {
                return text[..charLocation];
            }
        }

        return text;
    }
}
