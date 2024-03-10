namespace AdventOfCode._2022;

public class Day6(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int start = 0;
        while (true)
        {

            if (lines[0].Substring(start, 4).Distinct().Count() == 4) return start + 4;
            start++;
        }

        return 0;
    }

    public long GetTotalPartB()
    {
        int start = 0;
        while (true)
        {

            if (lines[0].Substring(start, 14).Distinct().Count() == 14) return start + 14;
            start++;
        }

        return 0;    }
}