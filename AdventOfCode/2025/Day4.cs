using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day4(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(false);

    public long GetTotalPartB() => Calculate(true);


    private long Calculate(bool partB)
    {
        long total = 0;
        int rows = lines.Length;
        int cols = lines[0].Length;

        int[] dirY = [-1, -1, -1, 0, 0, 1, 1, 1];
        int[] dirX = [-1, 0, 1, -1, 1, -1, 0, 1];

        HashSet<(int, int)> rolls = [];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                if (lines[x][y] == '@')
                {
                    rolls.Add((x, y));
                }
            }
        }

        bool changed = true;
        while (changed)
        {
            List<(int, int)> remove = [];
            foreach (var (x, y) in rolls)
            {
                int surr = 0;
                for (int i = 0; i < 8; i++)
                {
                    int nx = x + dirX[i];
                    int ny = y + dirY[i];

                    if (nx < 0 || nx >= cols || ny < 0 || ny >= rows) continue;

                    if (rolls.Contains((nx, ny)))
                    {
                        surr++;
                    }
                }

                if (surr < 4) remove.Add((x, y));
            }

            if (remove.Count > 0)
            {
                total += remove.Count;
                foreach (var (y, x) in remove)
                {
                    rolls.Remove((y, x));
                }

                if (!partB) return total;
            }
            else changed = false;
        }

        return total;
    }

}
