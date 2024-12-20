namespace AdventOfCode._2024;
using static AdventOfCode.Utils.EnumUtils;
public class Day20: IDay
{
    int rows;
    int cols;
    private string[] lines;
  
    (int row,int col) SPos = (0, 0);
    (int row,int col) EPos = (0, 0);
    
    static (int dr, int dc)[] dirs = [(-1, 0), (1, 0), (0, -1), (0, 1) ];

    public Day20(string[] lines)
    {
        this.lines = lines;
        rows = lines.Length;
        cols = lines[0].Length;
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
            {
                if(lines[i][j] == 'S') SPos = (i, j);
                if(lines[i][j] == 'E') EPos = (i, j);
            }
    }

    public long GetTotalPartA()
    {
        return BestSavings(2);
    }

    public long GetTotalPartB()
    {
        return BestSavings(20);
    }
    
    IEnumerable<(int row, int col)> Neighbours(int row, int col)
    {
        foreach (var (drow, dcol) in dirs)
        {
            int nrow = row + drow;
            int ncol = col + dcol;
            if (0 <= nrow && nrow < rows && 0 <= ncol && ncol < cols)
                yield return (nrow, ncol);
        }
    }
    bool IsTrack(int row, int col) => ".SE".Contains(lines[row][col]);

    int[][] Bfs((int r, int c) startPos)
    {
        var dist = Enumerable.Range(0, rows)
            .Select(_ => Enumerable.Repeat(-1, cols).ToArray())
            .ToArray();

        dist[startPos.r][startPos.c] = 0;
        var queue = new Queue<(int r, int c)>();
        queue.Enqueue(startPos);

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            foreach (var (nr, nc) in Neighbours(r, c))
            {
                if (dist[nr][nc] == -1 && IsTrack(nr, nc))
                {
                    dist[nr][nc] = dist[r][c] + 1;
                    queue.Enqueue((nr, nc));
                }
            }
        }
        return dist;
    }

    int BestSavings(int pico)
    {
        var distStart = Bfs(SPos);
        int normalTime = distStart[EPos.row][EPos.col];
     
        var distEnd = Bfs(EPos);
        var bestSavings = new Dictionary<((int x, int y), (int x, int y)), int>();
        var startReachableCells = new List<(int r, int c)>();

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                if (distStart[i][j] != -1 && IsTrack(i, j))
                    startReachableCells.Add((i, j));

        foreach (var (xr, xc) in startReachableCells)
        {
            int baseTime = distStart[xr][xc];
            var ignoreDist = Enumerable.Range(0, rows)
                .Select(_ => Enumerable.Repeat(-1, cols).ToArray())
                .ToArray();

            ignoreDist[xr][xc] = 0;
            
            var queue = new Queue<(int r, int c)>();
            queue.Enqueue((xr, xc));

            while (queue.Count > 0)
            {
                var (r, c) = queue.Dequeue();
                int steps = ignoreDist[r][c];
                if (steps == pico) continue;

                foreach (var (nr, nc) in Neighbours(r, c))
                {
                    if (ignoreDist[nr][nc] == -1)
                    {
                        ignoreDist[nr][nc] = steps + 1;
                        queue.Enqueue((nr, nc));
                    }
                }
            }

            for (int yr = 0; yr < rows; yr++)
            {
                for (int yc = 0; yc < cols; yc++)
                {
                    int steps = ignoreDist[yr][yc];
                    if (steps == -1 || steps == 0) continue;

                    if ((yr, yc) == EPos)
                    {
                        int cheatedTime = baseTime + steps;
                        int saving = normalTime - cheatedTime;
                        if (saving > 0)
                        {
                            var cheatKey = ((xr, xc), (yr, yc));
                            if (!bestSavings.ContainsKey(cheatKey) || bestSavings[cheatKey] < saving)
                                bestSavings[cheatKey] = saving;
                        }
                    }
                    else if (IsTrack(yr, yc) && distEnd[yr][yc] != -1)
                    {
                        int cheatedTime = baseTime + steps + distEnd[yr][yc];
                        int saving = normalTime - cheatedTime;
                        if (saving > 0)
                        {
                            var cheatKey = ((xr, xc), (yr, yc));
                            if (!bestSavings.ContainsKey(cheatKey) || bestSavings[cheatKey] < saving)
                                bestSavings[cheatKey] = saving;
                        }
                    }
                }
            }
        }

        return bestSavings.Values.Count(x => x >= 100);
    }

}