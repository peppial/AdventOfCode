namespace AdventOfCode._2024;

public class Day10(string[] lines): IDay
{
    public long GetTotalPartA()
    {
        (HashSet<(int, int)> zeroes,int[,] grid)  = GetGrid();

        HashSet<(int,int,int)> nines = [];
        int zc = 0;
        foreach ((int row, int column) zero in zeroes)
        {
            ReachNine(zero.row, zero.column,0);
            zc++;

        }
        return nines.Count;

        void ReachNine(int row, int column, int val)
        {
            if(grid[row, column] == 9) nines.Add((row, column,zc));
            val++;
            Console.WriteLine(val);
            if (row>0 && grid[row - 1, column] == val)
            {
                ReachNine(row - 1,column,val);
            }
            if (row<lines.Length-1 && grid[row + 1, column] == val)
            {
                ReachNine(row + 1,column,val);
            }
            if (column>0 && grid[row, column-1] == val)
            {
                ReachNine(row,column-1,val);
            }
            if (column<lines[0].Length-1 && grid[row, column+1] == val)
            {
                ReachNine(row,column+1,val);
            }
        }
    }
    public long GetTotalPartB()
    {
        (HashSet<(int, int)> zeroes,int[,] grid)  = GetGrid();

        HashSet<(int,string)> nines = [];
        int zc = 0;
        foreach ((int row, int column) zero in zeroes)
        {
            ReachNine(zero.row, zero.column,0,$"{zero.row}{zero.column}");
            zc++;

        }
        return nines.Count;

        void ReachNine(int row, int column, int val, string path)
        {
            if(grid[row, column] == 9) nines.Add((zc, path));
            val++;
            Console.WriteLine(val);
            if (row>0 && grid[row - 1, column] == val)
            {
                ReachNine(row - 1,column,val, $"{path}{row - 1}{column}");
            }
            if (row<lines.Length-1 && grid[row + 1, column] == val)
            {
                ReachNine(row + 1,column,val,$"{path}{row +1}{column}");
            }
            if (column>0 && grid[row, column-1] == val)
            {
                ReachNine(row,column-1,val,$"{path}{row}{column-1}");
            }
            if (column<lines[0].Length-1 && grid[row, column+1] == val)
            {
                ReachNine(row,column+1,val,$"{path}{row}{column+1}");
            }
        }
    }
    private (HashSet<(int, int)>,int[,]) GetGrid()
    {
        int[,] grid = new int[lines.Length, lines[0].Length];
        
        HashSet<(int,int)> zeroes = [];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                grid[i, j] = lines[i][j] - '0';
                if(grid[i,j] == 0) zeroes.Add((i,j));
            }
        }

        return (zeroes,grid);
    }

   
}