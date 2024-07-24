namespace AdventOfCode._2015;

public class Day25(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        int tRow = 2947, tColumn = 3029;
          
        long number = 20151125;
        long[,] grid = new long[9000, 9000];
        grid[1, 1] = 1;

        for (int rr = 2; rr < 9000; rr++)
        {
            int row = rr;
            int column = 1;
            while(column<=rr)
            {
                number *= 252533;
                number %=33554393;
                grid[row, column] = number;
                row--;
                column++;
            }
        }
        return grid[tRow,tColumn];
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}