namespace Advent2023;

public enum Direction
{
    North=0,
    West=1,
    South=2,
    East=3
}
public class Day14(string[] lines):IDay 
{
    int hLen = lines[0].Length;
    
    public long GetTotalPartA()
    {
        foreach (int column in hLen-1)
        {
            foreach (int row in lines.Length-1)
            {
                if (lines[row][column] == '.')
                {
                    GridPoint gp = FindNextO(new GridPoint(row, column));
                    if (gp is not null)
                    {
                        lines[row] = lines[row].Substring(0, column) + 'O' + lines[row].Substring(column + 1);
                        lines[gp.X] = lines[gp.X].Substring(0, column) + '.' + lines[gp.X].Substring(column + 1);
                    }
                }
            }
        }

        int total = 0;
        foreach (int row in lines.Length - 1)
        {
            total+= lines[row].Count(x => x == 'O')*(lines.Length-row);
        }

        return total;
    }

    
    
    public long GetTotalPartB()
    {
        int total = 0;
        int cycle = 1;
        while (cycle <= 1000000000)
        {
            //north
            foreach (int column in hLen - 1)
            {
                foreach (int row in lines.Length - 1)
                {
                    if (lines[row][column] == '.')
                    {
                        GridPoint gp = FindNextO(new GridPoint(row, column), Direction.North);
                        if (gp is not null)
                        {
                            lines[row] = lines[row].Substring(0, column) + 'O' + lines[row].Substring(column + 1);
                            lines[gp.X] = lines[gp.X].Substring(0, column) + '.' +
                                          lines[gp.X].Substring(column + 1);
                        }
                    }
                }
            }

            //west
            foreach (int row in lines.Length - 1)
            {
                foreach (int column in hLen - 1)
                {
                    if (lines[row][column] == '.')
                    {
                        GridPoint gp = FindNextO(new GridPoint(row, column), Direction.West);
                        if (gp is not null)
                        {
                            lines[row] = lines[row].Substring(0, column) + 'O' + lines[row].Substring(column + 1);
                            lines[row] = lines[row].Substring(0, gp.Y) + '.' +
                                         lines[row].Substring(gp.Y + 1);
                        }
                    }
                }
            }

            //south
            foreach (int column in hLen - 1)
            {
                for (int row = lines.Length - 1; row > 0; row--)
                {
                    if (lines[row][column] == '.')
                    {
                        GridPoint gp = FindNextO(new GridPoint(row, column), Direction.South);
                        if (gp is not null)
                        {
                            lines[row] = lines[row].Substring(0, column) + 'O' + lines[row].Substring(column + 1);
                            lines[gp.X] = lines[gp.X].Substring(0, column) + '.' +
                                          lines[gp.X].Substring(column + 1);
                        }
                    }
                }
            }

            //east
            foreach (int row in lines.Length - 1)
            {
                for (int column = hLen - 1; column > 0; column--)
                {
                    if (lines[row][column] == '.')
                    {
                        GridPoint gp = FindNextO(new GridPoint(row, column), Direction.East);
                        if (gp is not null)
                        {
                            lines[row] = lines[row].Substring(0, column) + 'O' + lines[row].Substring(column + 1);
                            lines[row] = lines[row].Substring(0, gp.Y) + '.' +
                                         lines[row].Substring(gp.Y + 1);
                        }
                    }
                }
            }

            cycle++;
        }

        foreach (int row in lines.Length - 1)
        {
            total += lines[row].Count(x => x == 'O') * (lines.Length - row);
        }
        return total;
    }
    private GridPoint FindNextO(GridPoint gp, Direction direction = Direction.North)
    {
        if (direction == Direction.North)
        {
            int row = gp.X + 1;
            while (row < lines.Length)
            {
                if (lines[row][gp.Y] == '#') break;
                if (lines[row][gp.Y] == 'O') return new GridPoint(row, gp.Y);
                row++;
            }
        }
        else if (direction == Direction.South)
        {
            int row = gp.X - 1;
            while (row >= 0)
            {
                if (lines[row][gp.Y] == '#') break;
                if (lines[row][gp.Y] == 'O') return new GridPoint(row, gp.Y);
                row--;
            }
        }
        else if (direction == Direction.West)
        {
            int column = gp.Y + 1;
            while (column < hLen)
            {
                if (lines[gp.X][column] == '#') break;
                if (lines[gp.X][column] == 'O') return new GridPoint(gp.X, column);
                column++;
            }
        }
        else if (direction == Direction.East)
        {
            int column = gp.Y - 1;
            while (column >= 0)
            {
                if (lines[gp.X][column] == '#') break;
                if (lines[gp.X][column] == 'O') return new GridPoint(gp.X, column);
                column--;
            }
        }

        return null;
    }
}