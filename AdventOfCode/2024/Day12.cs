namespace AdventOfCode._2024;

public class Day12(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        (var regions ,var grid) = GetGrid();
        int rows = lines.Length;
        int cols = lines[0].Length;

        long sum = 0;
        
       void FindAdjacent(int row, int col, HashSet<(int x, int y)> set, HashSet<(int x, int y)> cset)
        {
            
            if (set.Contains((row + 1, col)))
            {
                cset.Add((row + 1, col));
                set.Remove((row + 1, col));
                FindAdjacent(row+1,col, set, cset);
            }
            if (set.Contains((row - 1, col)))
            {
                cset.Add((row - 1, col));
                set.Remove((row - 1, col));
                FindAdjacent(row-1,col, set, cset);
            }
            if (set.Contains((row, col+1)))
            {
                cset.Add((row, col+1));
                set.Remove((row, col+1));
                FindAdjacent(row,col+1, set, cset);
            }
            if (set.Contains((row, col-1)))
            {
                cset.Add((row, col-1));
                set.Remove((row, col-1));
                FindAdjacent(row,col-1, set, cset);
            }
        }
        foreach (var region in regions)
        {
           
            List<HashSet<(int x, int y)>> sets = [];

            foreach (var loc in region.Value)
            {
                HashSet<(int row, int col)> cset = [(loc.row, loc.col)];
                FindAdjacent(loc.row, loc.col, region.Value, cset);
                int area = cset.Count;
                int per = 0;
                foreach (var loc1 in cset)
                {
                    if (loc1.row == 0 || grid[loc1.row - 1, loc1.col] != region.Key) per++;
                    if (loc1.row == rows-1 || grid[loc1.row + 1, loc1.col] != region.Key) per++;
                    if (loc1.col == 0 || grid[loc1.row, loc1.col-1] != region.Key) per++;
                    if (loc1.col == cols-1 || grid[loc1.row, loc1.col+1] != region.Key) per++;
                }
                
                Console.WriteLine(per);
            
                Console.WriteLine(area);
                Console.WriteLine(per*area);
                Console.WriteLine("--------");
                sum += per * area;
            }

            
            
        }
        return sum;
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
    private (Dictionary<char, HashSet<(int row,int col)>>,char[,]) GetGrid()
    {
        char[,] grid = new char[lines.Length, lines[0].Length];
        Dictionary<char, HashSet<(int row,int col)>> locs = [];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                grid[i, j] = lines[i][j];
                if(locs.ContainsKey(grid[i,j])) locs[grid[i,j]].Add((i,j));
                else locs.Add(grid[i,j], [(i,j) ]);
            }
        }

        return (locs,grid);
    }
}