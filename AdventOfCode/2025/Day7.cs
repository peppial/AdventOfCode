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
        grid = new char[_lines.Length][];
        int idx = 0;
        foreach (var line in _lines)
        {
            grid[idx] = line.ToCharArray();
            idx++;
        }
        marker = _lines[0].IndexOf('S');
    }

    public long GetTotalPartA() => CountSplits(0, marker, new HashSet<(int, int)>());

    public long GetTotalPartB() => CountTimelines(0, marker, new Dictionary<(int, int), long>());

    private long CountSplits(int r, int c, HashSet<(int, int)> visited)
    {
        if (c < 0 || c >= grid[0].Length)
        {
            return 0;
        }

        for (int nextR = r + 1; nextR < grid.Length; nextR++)
        {
            if (grid[nextR][c] == '^')
            {
                if (visited.Contains((nextR, c)))
                {
                    return 0;
                }

                visited.Add((nextR, c));
                long left = CountSplits(nextR, c - 1, visited);
                long right = CountSplits(nextR, c + 1, visited);
                return 1 + left + right;
            }
        }

        return 0;
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
