namespace Advent2023;

public static class ParseExtensions
{
    public static IEnumerable<T> ParseInputLines<T>(this string[] lines, Func<string, T> parseFunc)
    {
        return lines.Select(parseFunc);
    }
}