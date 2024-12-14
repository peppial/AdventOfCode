using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day14(string[] lines): IDay
{
    int rows = 103;
    int cols = 101;
    public long GetTotalPartA()
    {
        int total1 = 0;
        int total2 = 0;
        int total3 = 0;
        int total4 = 0;
        
        foreach (var line in lines)
        {
            var parts = line.GetNumbers();
            int r = parts[1];
            int c = parts[0];
            for (int i = 1; i < 101; i++)
            {
                
                int nc = parts[2] + c;
                if (nc >= cols) nc -= cols;
                if (nc <0) nc += cols;

                int nr = parts[3] + r;
                if (nr >= rows) nr -= rows;
                if (nr < 0) nr += rows;

                r = nr;
                c = nc;
            }

            if (r != rows / 2 || c != cols / 2)
            {
                if(r<rows / 2 && c < cols / 2) total1++;
                else if(r > rows / 2 && c > cols / 2) total4++;
                else if(r < rows / 2 && c > cols / 2) total2++;
                else if(r > rows / 2 && c < cols / 2) total3++;
            }
        }

        return total1*total2*total3*total4;
    }

    public long GetTotalPartB()
    {
        List<(int row, int col)> robots = [];
        List<(int right, int down)> velocities = [];

        foreach (var line in lines)
        {
            var parts = line.GetNumbers();
            int r = parts[1];
            int c = parts[0];
            robots.Add((r, c));
            velocities.Add((parts[2], parts[3]));
        }

        int count = 0;

        while (true)
        {
            List<(int row, int col)> robots1 = [];
            for(int i = 0; i < robots.Count; i++)
            {
                var robot = robots[i];
                int nc = velocities[i].right + robot.col;
                if (nc >= cols) nc -= cols;
                if (nc < 0) nc += cols;

                int nr = velocities[i].down + robot.row;
                if (nr >= rows) nr -= rows;
                if (nr < 0) nr += rows;

                robots1.Add((nr,nc));
            }

            robots = robots1;

            if (IsChristmasTree())
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (robots.Contains((i, j))) 
                            Console.Write('1');
                        else 
                            Console.Write('.');
                    }

                    Console.WriteLine();
                }

                break;
            }

            count++;
        }

        return count+1;
        
        bool IsChristmasTree()
        {
            return robots.Exists(r =>
            {
                var surroundingCols = Enumerable.Range(1, 8)
                    .ToArray();
                return surroundingCols.All(pos => robots.Contains((r.row, r.col + pos)));
            });
        }

    }
}


    