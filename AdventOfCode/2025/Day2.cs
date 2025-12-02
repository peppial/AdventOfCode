using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day2(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(false);

    public long GetTotalPartB() => Calculate(true);


    private long Calculate(bool partB)
    {
        var ranges = lines[0].Split(',');
        long totalInvalidIds = 0;

        foreach (var range in ranges)
        {
            var parts = range.Split('-');
            long start = long.Parse(parts[0]);
            long end = long.Parse(parts[1]);

            for (long i = start; i <= end; i++)
            {
                string s = i.ToString();
                if (!partB && s.Length % 2 != 0) continue;
                if (partB ? IsInvalidPartB(s) : IsInvalidPartA(s))
                {
                    totalInvalidIds += i;
                }
            }
        }

        return totalInvalidIds;
    }

    private bool IsInvalidPartA(string s)
    {
        string first = s.Substring(0, s.Length / 2);
        string second = s.Substring(s.Length / 2);

        return first == second;
    }
    private bool IsInvalidPartB(string s)
    {
        for (int len = 1; len <= s.Length / 2; len++)
        {
            if (s.Length % len != 0) continue;

            string pattern = s[..len];
            if (string.Concat(Enumerable.Repeat(pattern, s.Length / len)) == s)
            {
                return true;
            }
        }
        return false;
    }

}
