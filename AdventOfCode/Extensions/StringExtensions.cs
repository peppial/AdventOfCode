using System.Text.RegularExpressions;

namespace AdventOfCode.Extensions;

public static class StringExtensions
{
    public static int[] GetNumbers(this string line)
    {
        return Regex.Matches(line, "-?\\d+").Select(m => int.Parse(m.Value)).ToArray();
    }
    public static long[] GetNumbersLong(this string line)
    {
        return Regex.Matches(line, "-?\\d+").Select(m => long.Parse(m.Value)).ToArray();
    }
    public static ulong[] GetNumbersULong(this string line)
    {
        return Regex.Matches(line, "-?\\d+").Select(m => ulong.Parse(m.Value)).ToArray();
    }
    public static string[] SplitDefault(this string line, string chars)
    {
        return line.Split(chars,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    public static int[] GetNumbersAfterSeparator(this string line)
    {
        return line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1].Trim()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
    }

    public static int HasNumber(this string s, int starIndex)
    {
        if (char.IsDigit(s[starIndex])) return GetNumber(s, starIndex);
        if (starIndex > 0 && char.IsDigit(s[starIndex - 1])) return GetNumber(s, starIndex - 1);
        if (starIndex < s.Length - 1 && char.IsDigit(s[starIndex + 1])) return GetNumber(s, starIndex + 1);
        return 0;
    }

    public static int HasNumberBefore(this string s, int starIndex)
    {
        if (starIndex == 0) return 0;
        if (char.IsDigit(s[starIndex - 1])) return GetNumber(s, starIndex - 1);
        return 0;
    }
    public static int HasNumberAfter(this string s, int starIndex)
    {
        if (starIndex == s.Length - 1) return 0;
        if (char.IsDigit(s[starIndex + 1]))
        {
            return GetNumber(s, starIndex + 1);
        }

        return 0;
    }

    public static int GetNumber(this string line, int index)
    {
        int startIndex = index;
        int endIndex = index;
        while (startIndex > 0 && char.IsDigit(line[startIndex - 1])) startIndex--;
        while (endIndex + 1 < line.Length && char.IsDigit(line[endIndex + 1])) endIndex++;
        return int.Parse(line.Substring(startIndex, endIndex - startIndex + 1));
    }

    public static int LevenshteinDistance(this string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (int j = 0; j <= m; d[0, j] = j++)
        {
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }

    public static int CountDuplicates(this string str) =>
        (from c in str.ToLower()
         group c by c
            into grp
         where grp.Count() > 1
         select grp.Key).Count();
}
