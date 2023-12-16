namespace Advent2023;


public class Day14(string[] lines):IDay 
{
    enum Direction
    {
        North=0,
        West=1,
        South=2,
        East=3
    }
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
                        lines[gp.row] = lines[gp.row].Substring(0, column) + '.' + lines[gp.row].Substring(column + 1);
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
        List<string[]> cache = new();
        int cycle = 1;
        bool cyclefound = false;
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
                            lines[gp.row] = lines[gp.row].Substring(0, column) + '.' +
                                          lines[gp.row].Substring(column + 1);
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
                            lines[row] = lines[row].Substring(0, gp.column) + '.' +
                                         lines[row].Substring(gp.column + 1);
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
                            lines[gp.row] = lines[gp.row].Substring(0, column) + '.' +
                                          lines[gp.row].Substring(column + 1);
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
                            lines[row] = lines[row].Substring(0, gp.column) + '.' +
                                         lines[row].Substring(gp.column + 1);
                        }
                    }
                }
            }

            if (!cyclefound)
            {
                var index = GetIndexOf(cache, lines);

                if (index is not null)
                {
                    var cyclelen = cycle -1 - index.Value;
                    var delta = (1000000000 - cycle) % cyclelen;
                    var numberOfCycles = 1000000000 / cyclelen;
                    cache.Add([..lines]);
                    cycle = 1000000000 - delta;
                    cyclefound = true;

                }

            }
            
            cache.Add([..lines]);
            cycle++;
        }

        int total = 0;
        foreach (int row in lines.Length - 1)
        {
            total += lines[row].Count(x => x == 'O') * (lines.Length - row);
        }
        return total;

    }

    private int? GetIndexOf(List<string[]> cache, string[] lines)
    {
        for (int j = 0; j < cache.Count; j++)
        {
            if (cache[j].SequenceEqual(lines)) return j;
        }

        return null;
    }
    
    private GridPoint FindNextO(GridPoint gp, Direction direction = Direction.North)
    {
        if (direction == Direction.North)
        {
            int row = gp.row + 1;
            while (row < lines.Length)
            {
                if (lines[row][gp.column] == '#') break;
                if (lines[row][gp.column] == 'O') return new GridPoint(row, gp.column);
                row++;
            }
        }
        else if (direction == Direction.South)
        {
            int row = gp.row - 1;
            while (row >= 0)
            {
                if (lines[row][gp.column] == '#') break;
                if (lines[row][gp.column] == 'O') return new GridPoint(row, gp.column);
                row--;
            }
        }
        else if (direction == Direction.West)
        {
            int column = gp.column + 1;
            while (column < hLen)
            {
                if (lines[gp.row][column] == '#') break;
                if (lines[gp.row][column] == 'O') return new GridPoint(gp.row, column);
                column++;
            }
        }
        else if (direction == Direction.East)
        {
            int column = gp.column - 1;
            while (column >= 0)
            {
                if (lines[gp.row][column] == '#') break;
                if (lines[gp.row][column] == 'O') return new GridPoint(gp.row, column);
                column--;
            }
        }

        return null;
    }
}