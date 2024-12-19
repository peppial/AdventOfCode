namespace AdventOfCode._2024;

public class Day19 : IDay
{
    private readonly string[] patterns;
    private readonly List<string> designs = [];

    public Day19(string[] lines)
    {
        patterns = lines[0].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
        for (int i = 2; i < lines.Length; i++)
        {
            designs.Add(lines[i]);
        }
    }

    public long GetTotalPartA()
    {
        int count = 0;
        foreach (var design in designs)
        {
            if (IsPossible(design)) count++;
        }

        bool IsPossible(string design)
        {
            foreach (string p in patterns)
            {
                if (p == design) return true;
                if (design.StartsWith(p))
                {
                    if(IsPossible(design[p.Length..])) return true;
                }
            }

            return false;
        }

        return count;
    }

    public long GetTotalPartB()
    {
        long count = 0;

        foreach (var design in designs)
        {
            Dictionary<string, long> dict = [];
            count += CountPossible(design, dict);
        }

        return count;

        long CountPossible(string design, Dictionary<string, long> dict)
        {
            if (dict.ContainsKey(design)) return dict[design];

            long c = 0;

            foreach (string p in patterns)
            {
                if (p == design)
                {
                    c++;
                }

                if (design.StartsWith(p))
                {
                    c += CountPossible(design[p.Length..], dict);
                }
            }

            dict.Add(design, c);
            return c;
        }
    }

    public long GetTotalPartB_DP()
    {
        int count = 0;
        Console.WriteLine(designs.Sum(design => CalculatePatterns(design, patterns)));

        return count;

        static long CalculatePatterns(string design, string[] patterns)
        {
            long[] dp = new long[design.Length + 1];

            dp[0] = 1;

            for (int i = 1; i <= design.Length; i++)
            {
                dp[i] = 0;

                foreach (string pattern in patterns)
                {
                    if (pattern.Length > i) continue;

                    string subPattern = design.Substring(i - pattern.Length, pattern.Length);

                    if (subPattern == pattern)
                    {
                        dp[i] += dp[i - pattern.Length];
                    }
                }
            }

            return dp[^1];
        }
    }
}