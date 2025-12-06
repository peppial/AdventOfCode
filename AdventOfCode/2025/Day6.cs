using System.Formats.Tar;
using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day6(string[] lines) : IDay
{
    public long GetTotalPartA() => CalculateA();

    public long GetTotalPartB() => CalculateB();

    private long CalculateB()
    {
        int maxLineLength = 0;
        foreach (var line in lines)
        {
            if (line.Length > maxLineLength) maxLineLength = line.Length;
        }

        bool[] isOccupied = new bool[maxLineLength];
        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != ' ')
                {
                    isOccupied[i] = true;
                }
            }
        }

        List<(int, int)> columns = [];
        int start = -1;
        for (int i = 0; i < maxLineLength; i++)
        {
            if (isOccupied[i])
            {
                if (start == -1) start = i;
            }
            else
            {
                if (start != -1)
                {
                    columns.Add((start, i - start));
                    start = -1;
                }
            }
        }
        if (start != -1)
        {
            columns.Add((start, maxLineLength - start));
        }

        long total = 0;
        var lastLine = lines[^1];

        foreach (var (colStart, colWidth) in columns)
        {
            string c = "";
            if (colStart < lastLine.Length)
            {
                int len = Math.Min(colWidth, lastLine.Length - colStart);
                c = lastLine.Substring(colStart, len).Trim();
            }

            List<long> numbers = [];

            for (int j = 0; j < colWidth; j++)
            {
                string num = "";
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    string line = lines[i];
                    string s = "";
                    if (colStart < line.Length)
                    {
                        int len = Math.Min(colWidth, line.Length - colStart);
                        s = line.Substring(colStart, len);
                    }

                    s = s.PadRight(colWidth);

                    if (s.Length > j)
                    {
                        num += s[j];
                    }
                }

                if (long.TryParse(num, out long res))
                {
                    numbers.Add(res);
                }
            }

            if (c == "*")
            {
                long product = 1;
                foreach (var n in numbers)
                {
                    product *= n;
                }
                total += product;
            }
            else
            {
                foreach (var n in numbers)
                {
                    total += n;
                }
            }
        }
        return total;
    }

    private long CalculateA()
    {
        long[][] grid = new long[lines.Length - 1][];
        int i = 0;
        foreach (var line in lines[..^1])
        {
            grid[i++] = line.GetNumbersLong();
        }
        var lastLine = lines[^1];
        long total = 0;
        int ix = 0;
        foreach (string c in lastLine.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            if (c == "*")
            {
                long product = 1;
                for (int j = 0; j < lines.Length - 1; j++)
                {
                    product *= grid[j][ix];
                }
                total += product;
            }
            else
            {
                for (int j = 0; j < lines.Length - 1; j++)
                {
                    total += grid[j][ix];
                }
            }
            ix++;
        }
        return total;
    }

}
