namespace Advent2023;

public class Day16(string[] lines):IDay
{
    enum Direction
    {
        Right,
        Down,
        Left,
        Up
    }
    int len = lines[0].Length;
    int height = lines.Length;
    public long GetTotalPartA()
    {
        Stack<(GridPoint gp,Direction direction)> beam = new();
        Stack<GridPoint> seen = new();

        beam.Push((new GridPoint(0,0), Direction.Right));
        seen.Push(new GridPoint(0,0));
        
        HashSet<GridPoint> energizers = new();
        
        while (beam.Any())
        {
            var (gp, direction) = beam.Pop();
            Console.WriteLine("pop");
            /*foreach (int i in height)
            {
                foreach (int j in len)
                {
                    if(energizers.Contains(new GridPoint(i,j)))
                        Console.Write("#");
                    else
                        Console.Write(".");
                }

                Console.WriteLine();
            }*/
            while (true)
            {
                energizers.Add(gp);
                char symbol = lines[gp.row][gp.column];
                Console.WriteLine($"{gp.row},{gp.column} -> {direction}, symbol {symbol}");
                //check for end
                if (symbol == '.' || symbol == '-' && IsDirectionHorizontal(direction)
                                  || symbol == '|' && IsDirectionVertical(direction))
                {
                    if (!IsFeasible(gp, direction)) break;
                    gp = GetNextPoint(gp, direction);

                }

                if (symbol == '/')
                {
                    Direction newDirection = GetMirrorDirectionRight(direction);
                    if (!IsFeasible(gp, newDirection)) break;
                    gp = GetNextPoint(gp, newDirection);
                    direction = newDirection;
                }

                if (symbol == '\\')
                {
                    Direction newDirection = GetMirrorDirectionLeft(direction);
                    if (!IsFeasible(gp, newDirection)) break;
                    gp = GetNextPoint(gp, newDirection);
                    direction = newDirection;
                }

                if (symbol == '|' && IsDirectionHorizontal(direction))
                {
                    var gpNew = new GridPoint(gp.row - 1, gp.column);
                    if (gpNew.row >= 0 && !seen.Contains(gpNew))
                    {
                        beam.Push((gpNew, Direction.Up));
                        seen.Push(gpNew);
                    }

                    gpNew = new GridPoint(gp.row + 1, gp.column);
                    
                    if (gpNew.row <height && !seen.Contains(gpNew))
                    {
                        beam.Push((gpNew, Direction.Down));
                        seen.Push(gpNew);
                    }
                    break;
                }

                if (symbol == '-' && IsDirectionVertical(direction))
                {
                    var gpNew = new GridPoint(gp.row, gp.column-1);
                    if (gpNew.column >= 0 && !seen.Contains(gpNew))
                    {
                        beam.Push((gpNew, Direction.Left));
                        seen.Push(gpNew);
                    }
                    
                    gpNew = new GridPoint(gp.row, gp.column+1);
                    if (gpNew.column <len && !seen.Contains(gpNew))
                    {
                        beam.Push((gpNew, Direction.Right));
                        seen.Push(gpNew);
                    }

                    break;
                }
            }
        }

        foreach (int i in height)
        {
            foreach (int j in len)
            {
                if(energizers.Contains(new GridPoint(i,j)))
                    Console.Write("#");
                else
                    Console.Write(".");
            }

            Console.WriteLine();
        }
        return energizers.Count;
    }

    private bool IsDirectionHorizontal(Direction direction) => (direction == Direction.Left || direction == Direction.Right);
    private bool IsDirectionVertical(Direction direction) => (direction == Direction.Up || direction == Direction.Down);

    private Direction GetMirrorDirectionLeft(Direction direction)
    {
       return direction switch
        {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Left
        };
    }
    
    private Direction GetMirrorDirectionRight(Direction direction)
    {
        return direction switch
        {
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Up => Direction.Right
        };
    }
    private bool IsFeasible(GridPoint gp, Direction newDirection)
    {
        return !((newDirection == Direction.Left && gp.column == 0)
                          || (newDirection == Direction.Right && gp.column == len-1)
                          || (newDirection == Direction.Up && gp.row == 0)
                          || (newDirection == Direction.Down && gp.row == height-1));
    }
    private GridPoint GetNextPoint(GridPoint gp, Direction newDirection)
    {
        int row = gp.row;
        int column = gp.column;
        if (newDirection == Direction.Left) column--;
        if (newDirection == Direction.Right) column++;
        if (newDirection == Direction.Down) row++;
        if (newDirection == Direction.Up) row--;
        return new GridPoint(row, column);
    }
    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
}