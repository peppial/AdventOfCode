namespace AdventOfCode._2024;

public class Day6(string[] lines): IDay
{
    enum Direction { Up, Right, Down, Left }
    
    public long GetTotalPartA()
    {
        (int columnX, int rowX) = GetStart();

        HashSet<(int, int)> visited = [];
        visited.Add((rowX, columnX));
        
        Direction direction = Direction.Up;
        while (true)
        {
            (direction, columnX, rowX) = GetValue(direction, columnX, rowX);
            if (rowX < 0 || columnX < 0 || rowX > lines.Length - 1 || columnX > lines[0].Length - 1) break;

            visited.Add((rowX, columnX));
            Console.WriteLine($"{columnX}, {rowX}");
        }

        return visited.Count();
    }
    public long GetTotalPartB()
    {
        (int columnX, int rowX) = GetStart();
        Direction direction = Direction.Up;
        int count = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                int rowX1 = rowX;
                int columnX1 = columnX;
                direction = Direction.Up;
                
                if (lines[i][j] != '.') continue;
                string old = lines[i];
                lines[i] = lines[i][0..j] + "#" + lines[i][(j+1)..];
                
                int cc = 0;
                while (true)
                {
                    (direction, columnX, rowX) = GetValue(direction, columnX, rowX);
                    if (rowX < 0 || columnX < 0 || rowX > lines.Length - 1 || columnX > lines[0].Length - 1) break;
                    cc++;
                    if (cc>10000)
                    {
                        count++;
                        break;
                    }
                }

                lines[i] = old;
                rowX = rowX1;
                columnX = columnX1;
            }
        }
        return count;
    }

    private (int columnX, int rowX) GetStart()
    {
        int rowX = 0;
        int columnX = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            int pos = lines[i].IndexOf('^');
            if (pos >= 0) {
                columnX = pos;
                rowX=i;
                break;
            }
        }

        return (columnX, rowX);
    }

    private (Direction direction, int columnX, int rowX) GetValue(Direction direction, int columnX,
        int rowX)
    {
        (int columnX, int rowX, Direction direction) newPos = direction switch
        {
            Direction.Up => (columnX, rowX - 1, Direction.Up),
            Direction.Down => (columnX, rowX + 1, Direction.Down),
            Direction.Left => (columnX - 1, rowX, Direction.Left),
            Direction.Right => (columnX + 1, rowX, Direction.Right),
        };
        if (newPos.rowX < 0 || newPos.columnX < 0 || newPos.rowX > lines.Length - 1 || newPos.columnX > lines[0].Length - 1) return (direction, newPos.columnX, newPos.rowX);

        if (lines[newPos.rowX][newPos.columnX] == '#')
        {
            direction += 1;
            if((int)direction == 4) direction = Direction.Up;
        }
        else
        {
            
            rowX = newPos.rowX;
            columnX = newPos.columnX;
        }

        return (direction, columnX, rowX);
    }

   
}