using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day12(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(false);

    public long GetTotalPartB() => Calculate(true);


    private long Calculate(bool partB)
    {
        int count = 0;
        for (int i = 30; i < lines.Length; i++)
        {
            var numbers = lines[i].GetNumbers();
            var size = numbers[0] * numbers[1];
            long sum = numbers[2..].Sum() * 9;
            if (size >= sum) count++;

        }

        return count;
    }

}
