namespace Advent2023;

public static class Extensions
{
    public static IEnumerable<T> ParseInputLines<T>(this string[] lines, Func<string, T> parseFunc)
    {
        return lines.Select(parseFunc);
    }
}