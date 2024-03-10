using System.Xml.Schema;
using Advent2023;
using System.Linq;
using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day6(string[] lines):IDay
{
    enum Command
    {
        TurnOff,
        TurnOn,
        Toggle
    }
    public long GetTotalPartA()
    {
        bool[,] grid = new bool[1000,1000];
        foreach (string line in lines)
        {
           
            int[] numbers = line.GetNumbers();
            for (int i=numbers[0];i<=numbers[2];i++)
            for (int j = numbers[1]; j <=numbers[3]; j++)
            {
                if (line.StartsWith("turn on"))
                {
                    grid[i,j] = true;
                }
                else if (line.StartsWith("turn off"))
                {
                    grid[i,j] = false;
                }
                else if (line.StartsWith("toggle"))
                {
                    grid[i,j] = !grid[i,j];
                }
            }
        }

        int count = 0;
        for (int i = 0; i < 1000; i++)
        for (int j = 0; j < 1000; j++)
        {
            if (grid[i, j]) count++;
        }
        return count;
    }

    public long GetTotalPartB()
    {
        int[,] grid = new int[1000,1000];
        foreach (string line in lines)
        {
           
            int[] numbers = line.GetNumbers();
            for (int i=numbers[0];i<=numbers[2];i++)
            for (int j = numbers[1]; j <=numbers[3]; j++)
            {
                if (line.StartsWith("turn on"))
                {
                    grid[i,j] +=1;
                }
                else if (line.StartsWith("turn off"))
                {
                    if(grid[i,j]>0) grid[i,j] -=1;
                }
                else if (line.StartsWith("toggle"))
                {
                    grid[i, j] += 2;
                }
            }
        }

        int count = 0;
        for (int i = 0; i < 1000; i++)
        for (int j = 0; j < 1000; j++)
        {
            count+=grid[i, j];
        }
        return count;
        
    }
}