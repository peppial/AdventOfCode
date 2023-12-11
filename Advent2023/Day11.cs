namespace Advent2023;

public class Day11 : IDay
{
    private string[] lines;
    private List<int> emptyRows;
    private List<int> emptyCols;
    public Day11(string[] lines)
    {
        this.lines = lines;
        List<string> linesPivot = Enumerable.Range(0, lines[0].Length)
            .Select(col => new string(lines.Select(row => row[col]).ToArray()))
            .ToList();
        
        emptyRows = Enumerable.Range(0, lines.Length)
            .Where(i => lines[i].IndexOf('#') == -1)
            .ToList();

        emptyCols = Enumerable.Range(0, lines.Length)
            .Where(i => linesPivot[i].IndexOf('#') == -1)
            .ToList();
    }
    
    public long GetTotalPartA()
    { 
        var galaxies = GetGalaxies();
        
        int totalDistance = 0;
        var points = galaxies.ConvertAll((x) =>new GridPoint(x.Item1,x.Item2));
        foreach ((GridPoint,GridPoint) a in points.Combinations())
        {
            totalDistance += Distance(a.Item1, a.Item2,1);
        }

        return totalDistance/2;
    }

   

    public long GetTotalPartB()
    {
        List<(int, int)> galaxies = GetGalaxies();
        int totalDist = 0;
        var points = galaxies.ConvertAll((x) =>new GridPoint(x.Item1,x.Item2)); 
        foreach ((GridPoint,GridPoint) a in points.Combinations())
        {
            totalDist += Distance(a.Item1, a.Item2,1000000 - 1);
        }

        return totalDist/2;
    }
    private List<(int, int)> GetGalaxies()
    {
        List<(int, int)> galaxies = new List<(int, int)>();
        for (int r = 0; r < lines.Length; r++)
        {
            for (int c = 0; c < lines[r].Length; c++)
            {
                if (lines[r][c] == '#')
                {
                    galaxies.Add((r, c));
                }
            }
        }

        return galaxies;
    }
    
    private int Distance(GridPoint g1, GridPoint g2, int exp)
    {
        int r1 = Math.Min(g1.X, g2.X);
        int c1 = Math.Min(g1.Y, g2.Y);
        int r2 = Math.Max(g1.X, g2.X);
        int c2 = Math.Max(g1.Y, g2.Y);

        int dr = r2 - r1 + exp * emptyRows.Count(r => r1 < r && r < r2);
        int dc = c2 - c1 + exp * emptyCols.Count(c => c1 < c && c < c2);

        return dr + dc;
    }
}