using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2024;

public class Day19: IDay
{
    private string[] patterns;
    private List<string> designs=[];
    public Day19(string[] lines)
    {
        patterns=lines[0].Split([' ', ','], StringSplitOptions.RemoveEmptyEntries);
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
            if(IsPossible(design)) count++;
        }

        bool IsPossible(string design)
        {
            List<string> poss = [];
            bool isPossible = false;
            foreach (string p in patterns)
            {
                if(p == design) return true;
                if (design.StartsWith(p))
                {
                    isPossible=isPossible||IsPossible(design[p.Length..]);
                }
            }

            return isPossible;
        }
        
        return count;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        Console.WriteLine(designs.Sum(design => CalculatePatterns(design, patterns)));
        
        return count;

        static long CalculatePatterns(string pat, string[] patterns)
        {
            long[] dp = new long[pat.Length + 1];
            
            dp[0] = 1;

            for (int i = 1; i <= pat.Length; i++)
            {
                dp[i] = 0;
                
                foreach (string pattern in patterns)
                {
                    if (pattern.Length > i) continue;
                
                    string subPattern = pat.Substring(i - pattern.Length, pattern.Length);
                    
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