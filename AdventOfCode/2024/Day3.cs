using System.Text.RegularExpressions;
using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day3(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        string pattern = @"mul\((\d+),(\d+)\)";

        long total = 0;
        foreach (string line in lines)
        {
            foreach (Match match in Regex.Matches(line, pattern))
            {
                Console.WriteLine(match.Value);
                int[] parts = match.Value.GetNumbers();
                total += parts[0] * parts[1];

            }
        }

        return total;
    }

    public long GetTotalPartB()
    {
        long total = 0;
        string pattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";
        
        
        bool enabled = true;
        foreach (string line in lines)
        {
            foreach (Match match in Regex.Matches(line, pattern))
            {
                if(match.Value == "do()") enabled = true;
                else if(match.Value == "don't()") enabled = false;
                else
                {
                    if (enabled)
                    {
                        int[] parts = match.Value.GetNumbers();
                        total += parts[0] * parts[1];
                    }
                }
            }
        }

        return total;    
    }
}