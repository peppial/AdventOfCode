using AdventOfCode.Extensions;
using AdventOfCode.Utils;

namespace AdventOfCode._2024;

public class Day18: IDay
{
    private List<(int, int)> inputA=[];
    private List<(int, int)> inputB=[];
    private const int size = 70;
    private (int, int)[] Directions = [ (0, 1), (0, -1), (-1, 0), (1, 0)];
    
    public Day18(string[] lines)
    {
        int count = 0;
        foreach (var line in lines)
        {
            var parts = line.GetNumbers();
           
            inputB.Add((parts[0], parts[1]));
            count++;
            if (count < 1024)
            {
                inputA.Add((parts[0], parts[1]));
            };
            
        }

        for (int i = 0; i <= size; i++)
        {
            for (int j = 0; j <= size; j++)
            {
                if(inputA.Contains((i,j))) Console.Write("#");
                else Console.Write(".");
            }
            Console.WriteLine();
        }
    }
    
    public long GetTotalPartA()
    {  
        return FindMinPath(inputA, 1024);
    }

    public long GetTotalPartB()
    {
        int low = 1;
        int high = inputB.Count;
        while (low < high)
        {
            int mid = (low + high) / 2;
            if (FindMinPath(inputB, mid) == -1)
                high = mid;
            else
                low = mid + 1;
        }

        Console.WriteLine(string.Join(", ", inputB[low - 1]));
        return 0;
    }
    
    int FindMinPath(List<(int, int)> blockedCells, int count)
    {
        var blocked = new HashSet<(int, int)>(blockedCells.Take(count));
        var queue = new PriorityQueue<(int Distance, int X, int Y), int>();
        queue.Enqueue((0, 0, 0), 0);

        var found = new Dictionary<(int, int), int>();

        while (queue.TryDequeue(out var current, out _))
        {
            var (d, x, y) = current;

            if (found.ContainsKey((x, y))) continue;
            if (blocked.Contains((x, y))) continue;

            found[(x, y)] = d;

            if ((x, y) == (size, size)) break;

            foreach (var (dx, dy) in Directions)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (newX >= 0 && newX <= size && newY >= 0 && newY <= size)
                {
                    queue.Enqueue((d + 1, newX, newY), d + 1);
                }
            }
        }

        return found.ContainsKey((size, size)) ? found[(size, size)] : -1;
    }
}