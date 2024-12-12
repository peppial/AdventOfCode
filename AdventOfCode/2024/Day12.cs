namespace AdventOfCode._2024;

public class Day12(string[] lines): IDay
{
    enum Direction
    {
        None = -1,
        Right,
        Down,
        Left,
        Up
    }


    public long GetTotalPartA()
    {
        return GetPrice();
    }
    public long GetTotalPartB()
    {
        long result = 0;
        (var regions, var grid) = GetGrid();
        int rows = lines.Length;
        int cols = lines[0].Length;

        HashSet<(int, int)> visited = [];

        for (int row = 0; row < rows; ++row)
        {
            for (int col = 0; col < cols; ++col)
            {
                if (!visited.Contains((row, col)))
                {
                    long area = 0;
                    HashSet<(int row, int col, Direction side)> sides = [];
                    GetSides(ref area, sides, grid[row, col], row, col);
                    result += area * sides.Count;
                    Console.WriteLine(grid[row, col]);
                    Console.WriteLine(area);
                    Console.WriteLine(area * sides.Count);
                    Console.WriteLine("--------");
                }
            }
        }

        return result;
        
        void GetSides(ref long area, HashSet<(int row, int col, Direction direction)> sides, char type, int row, int col)
        {
            if (row < 0 || col < 0 || row >= rows || col >= cols) return;

            if ((grid[row,col] != type) || visited.Contains((row, col))) return;
            
            visited.Add((row, col));
            area++;
            var start = FindStart(row, col, Direction.Right);
            if (start.direction >= 0) sides.Add(start);
            start = FindStart(row, col, Direction.Down);
            if (start.direction >= 0) sides.Add(start);
            start = FindStart(row, col, Direction.Left);
            if (start.direction >= 0) sides.Add(start);
            start = FindStart(row, col, Direction.Up);
            if (start.direction >= 0) sides.Add(start);

            GetSides(ref area, sides, type, row, col + 1);
            GetSides(ref area, sides, type, row, col - 1);
            GetSides(ref area, sides, type, row + 1, col);
            GetSides(ref area, sides, type, row - 1, col);

        }

        (int row, int col, Direction direction) FindStart(int row, int col, Direction direction)
        {
            char ch = grid[row,col];
            switch (direction)
            {
                case Direction.Right:
                {
                    if (!IsInArea(row, col + 1, ch))
                    {
                        int startRow = row;
                        while (IsInArea(startRow - 1, col, ch) && !IsInArea(startRow - 1, col + 1, ch))
                            startRow--;
                        return (startRow, col + 1, direction);
                    }

                    return (row, col, Direction.None);
                }
                case Direction.Down:
                {
                    if (!IsInArea(row + 1, col, ch))
                    {
                        int startCol = col;
                        while (IsInArea(row, startCol - 1, ch) && !IsInArea(row + 1, startCol - 1, ch))
                            startCol--;
                        return (row + 1, startCol, direction);
                    }

                    return (row, col, Direction.None);
                }
                case Direction.Left:
                {
                    if (!IsInArea(row, col - 1, ch))
                    {
                        int startRow = row;
                        while (IsInArea(startRow - 1, col, ch) && !IsInArea(startRow - 1, col - 1, ch))
                            startRow--;
                        return (startRow, col, direction);
                    }

                    return (row, col, Direction.None);
                }
                case Direction.Up:
                {
                    if (!IsInArea(row - 1, col, ch))
                    {
                        int startC = col;
                        while (IsInArea(row, startC - 1, ch) && !IsInArea(row - 1, startC - 1, ch))
                            startC--;
                        return (row, startC, direction);
                    }

                    return (row, col, Direction.None);
                }
            }

            return (row, col, Direction.None);
        }

        bool IsInArea(int l, int c, char type)
        {
            if (l < 0 || c < 0 || l >= rows || c >= cols) return false;
            return grid[l,c] == type;
        }
    }

    private long GetPrice()
    {
        (var regions, var grid) = GetGrid();
        int rows = lines.Length;
        int cols = lines[0].Length;

        long sum = 0;

        void FindAdjacent(int row, int col, HashSet<(int x, int y)> set, HashSet<(int x, int y)> cset)
        {
            if (set.Contains((row + 1, col)))
            {
                cset.Add((row + 1, col));
                set.Remove((row + 1, col));
                FindAdjacent(row + 1, col, set, cset);
            }

            if (set.Contains((row - 1, col)))
            {
                cset.Add((row - 1, col));
                set.Remove((row - 1, col));
                FindAdjacent(row - 1, col, set, cset);
            }

            if (set.Contains((row, col + 1)))
            {
                cset.Add((row, col + 1));
                set.Remove((row, col + 1));
                FindAdjacent(row, col + 1, set, cset);
            }

            if (set.Contains((row, col - 1)))
            {
                cset.Add((row, col - 1));
                set.Remove((row, col - 1));
                FindAdjacent(row, col - 1, set, cset);
            }
        }

        foreach (var region in regions)
        {
            foreach (var loc in region.Value)
            {
                HashSet<(int row, int col)> cset = [(loc.row, loc.col)];
                FindAdjacent(loc.row, loc.col, region.Value, cset);
                int area = cset.Count;
                int perimeter = 0;
               
                foreach (var loc1 in cset)
                {
                    if (loc1.row == 0 || grid[loc1.row - 1, loc1.col] != region.Key) perimeter++;
                    if (loc1.row == rows - 1 || grid[loc1.row + 1, loc1.col] != region.Key) perimeter++;
                    if (loc1.col == 0 || grid[loc1.row, loc1.col - 1] != region.Key) perimeter++;
                    if (loc1.col == cols - 1 || grid[loc1.row, loc1.col + 1] != region.Key) perimeter++;
                }

                Console.WriteLine(area);
                Console.WriteLine(perimeter * area);
                Console.WriteLine("--------");
                sum += perimeter * area;
            }
        }

        return sum;
       
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