using AdventOfCode.Extensions;

namespace AdventOfCode._2016;

public class Day8(string[] lines) : IDay
{
    bool[,] grid = new bool[6, 50];
    public long GetTotalPartA()
    {
        
        int maxRow = 6;
        int maxColumn = 50;
        foreach (string line in lines)
        {
            if (line.StartsWith("rect"))
            {
                int[] numbers = line.GetNumbers();
                for (int i = 0; i < numbers[1]; i++)
                {
                    for (int j = 0; j < numbers[0]; j++)
                    {
                        grid[i, j] = true;
                    }
                }
                continue;
            }

            if (line.Contains("row"))
            {
                int[] numbers = line.GetNumbers();
                int row = numbers[0];
                int move = numbers[1];
                bool[] temp = new bool[maxColumn];
                for (int i = 0; i < maxColumn; i++) temp[i] = grid[row, i];

                for (int i = 0; i < maxColumn; i++)
                {
                    int newLoc = move + i;
                    if (newLoc >= maxColumn) newLoc -=maxColumn;
                    grid[row, newLoc] = temp[i];

                }
            }
            if (line.Contains("column"))
            {
                int[] numbers = line.GetNumbers();
                int column = numbers[0];
                int move = numbers[1];
                bool[] temp = new bool[maxRow];
                for (int i = 0; i < maxRow; i++) temp[i] = grid[i,column];

                for (int i = 0; i < maxRow; i++)
                {
                    int newLoc = move + i;
                    if (newLoc >= maxRow) newLoc -=maxRow;
                    grid[newLoc,column] = temp[i];

                }
            }
        }

        int count = 0;
        for (int i = 0; i <maxRow; i++)
        {
            for (int j = 0; j < maxColumn; j++)
            {
                Console.Write(grid[i, j] ? "#" : ".");
                if (grid[i, j]) count++;
            }
            Console.WriteLine();
        }
        return count;
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}