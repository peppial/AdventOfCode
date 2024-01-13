namespace AdventOfCode._2022;

public class Day1(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        long max = 0;
        long localMax = 0;
        foreach (string line in lines)
        {
            if (line.Trim() == "")
            {
                if (localMax > max) max = localMax;
                localMax = 0;
                continue;
            }
            localMax += int.Parse(line);
        }

        return max;
    }

    public long GetTotalPartB()
    {
        long max = 0;
        long localMax = 0;
        List<long> maxes = [];
        foreach (string line in lines)
        {
            if (line.Trim() == "")
            {
                maxes.Add(localMax);
                localMax = 0;
                continue;
            }
            localMax += int.Parse(line);
        }

        return maxes.OrderDescending().Take(3).Sum();    
    }
}