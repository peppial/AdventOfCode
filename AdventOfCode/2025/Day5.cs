using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day5 : IDay
{
    List<(long start, long end)> ranges = [];
    int i = 0;
    string[] lines;
    public Day5(string[] lines)
    {
        this.lines = lines;
        for (; i < lines.Length; i++)
        {
            if (lines[i] == "") break;
            var parts = lines[i].Split('-');
            ranges.Add((long.Parse(parts[0]), long.Parse(parts[1])));

            //Console.WriteLine(lines[i]);
        }
    }
    public long GetTotalPartA() => CalculateA();

    public long GetTotalPartB() => CalculateB();


    private long CalculateA()
    {
        int count = 0;

        for (int j = i + 1; j < lines.Length; j++)
        {
            long number = long.Parse(lines[j]);
            foreach (var (start, end) in ranges)
            {
                if (number >= start && number <= end)
                {
                    count++;
                    break;
                }
            }
        }

        return count;
    }
    private long CalculateB()
    {

        ranges.Sort((a, b) => a.start.CompareTo(b.start));

        List<(long start, long end)> mergedRanges = [];
        var currentRange = ranges[0];

        for (int j = 1; j < ranges.Count; j++)
        {
            var nextRange = ranges[j];
            if (nextRange.start <= currentRange.end + 1)
            {
                currentRange.end = Math.Max(currentRange.end, nextRange.end);
            }
            else
            {
                mergedRanges.Add(currentRange);
                currentRange = nextRange;
            }
        }
        mergedRanges.Add(currentRange);

        long count = 0;
        foreach (var range in mergedRanges)
        {
            count += range.end - range.start + 1;
        }

        return count;
    }

}
