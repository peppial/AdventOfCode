using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day7 : IDay
{
    private char[][] grid;
    private string[] _lines;
    private int marker = 0;

    public Day7(string[] lines)
    {
        _lines = lines;
        ResetGrid();
    }

    private void ResetGrid()
    {
        grid = new char[_lines.Length][];
        int idx = 0;
        foreach (var line in _lines)
        {
            grid[idx] = line.ToCharArray();
            idx++;
        }
        marker = _lines[0].IndexOf('S');
    }

    public long GetTotalPartA()
    {
        ResetGrid();
        return CalculatePartA();
    }

    public long GetTotalPartB()
    {
        ResetGrid();
        return CountTimelines(0, marker, new Dictionary<(int, int), long>());
    }

    private long CalculatePartA()
    {
        long total = 0;
        if (grid.Length > 1)
        {
            grid[1][marker] = '|';
        }

        for (int i = 2; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i - 1][j] == '|')
                {
                    if (grid[i][j] == '.')
                    {
                        grid[i][j] = '|';
                    }
                    else if (grid[i][j] == '^')
                    {
                        if (j + 1 < grid[0].Length) grid[i][j + 1] = '|';
                        if (j - 1 >= 0) grid[i][j - 1] = '|';
                        total++;
                    }
                }
            }
        }
        return total;
    }

    private long CountTimelines(int r, int c, Dictionary<(int, int), long> memo)
    {
        if (c < 0 || c >= grid[0].Length)
        {
            return 1;
        }

        if (memo.ContainsKey((r, c)))
        {
            return memo[(r, c)];
        }

        for (int nextR = r + 1; nextR < grid.Length; nextR++)
        {
            if (grid[nextR][c] == '^')
            {
                long leftPaths = CountTimelines(nextR, c - 1, memo);
                long rightPaths = CountTimelines(nextR, c + 1, memo);

                long result = leftPaths + rightPaths;
                memo[(r, c)] = result;
                return result;
            }
        }

        return 1;
    }
}
