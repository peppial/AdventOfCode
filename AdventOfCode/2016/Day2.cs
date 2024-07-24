namespace AdventOfCode._2016;

public class Day2(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        int[,] grid = new int [3, 3]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        int column = 1, row = 1;
        string s = "";
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                if (c == 'L') column--;
                else if (c == 'R') column++;
                else if (c == 'U') row--;
                else if (c == 'D') row++;
                if (column < 0) column = 0;
                if (row < 0) row = 0;
                if (column > 2) column = 2;
                if (row > 2) row = 2;
            }
            
            s += grid[row,column];
        }
        return int.Parse(s);
    }

    public long GetTotalPartB()
    {
        char?[,] grid = new char? [5, 5]
        {
            { null, null, '1', null, null },
            { null, '2', '3', '4', null },
            { '5', '6', '7', '8', '9' },
            { null, 'A', 'B', 'C', null },
            { null, null, 'D', null, null },
        };

        int column = 0, row = 2;
        string s = "";
        
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                if (c == 'L' && (column-1)>=0 && grid[row,column-1]!=null) column--;
                else if (c == 'R'  && (column+1)<=4 && grid[row,column+1]!=null) column++;
                else if (c == 'U' && (row-1)>=0 && grid[row-1,column]!=null) row--;
                else if (c == 'D' && (row+1)<=4 && grid[row+1,column]!=null) row++;
            }
            
            s += grid[row,column];
        }

        Console.WriteLine(s);
        return 0;    
    }
}