using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day2(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int total = 0;
        foreach (var line in lines)
        {
            int[] parts = line.GetNumbers();
            int p1 = parts[0] * parts[1];
            int p2 = parts[1] * parts[2];
            int p3 = parts[0] * parts[2];

            total += 2 * (p1 + p2 + p3) + int.Min(int.Min(p1, p2), p3);
        }

        return total;
    }

    public long GetTotalPartB()
    {
        int total = 0;
        foreach (var line in lines)
        {
            int[] parts = line.GetNumbers();
            parts = parts.Order().ToArray();
            total += 2 * (parts[0] + parts[1]) + parts[0] * parts[1] * parts[2];
        }

        return total;    }
}