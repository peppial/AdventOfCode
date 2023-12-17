namespace Advent2023;

public class Day17(string[] lines) : IDay //Dijkstra with priority queue
{
    enum Direction
    {
        None,
        North,
        East,
        South,
        West
    }

    private int len = lines[0].Length;

    private int height = lines.Length;
    private int[,] map;
    public long GetTotalPartA()
    {
        InitMap();

        return FindMinHeatLoss(0, 3);
    }

    public long GetTotalPartB()
    {
        InitMap();

        return FindMinHeatLoss(4, 10);
    }

    private void InitMap()
    {
        map=new int[len, height];

        foreach (var row in height-1)
        {
            foreach (var column in len-1)
            {
                map[row, column] = lines[column][row] - '0'; //convert char to int
            }
        }
    }
    
    private int FindMinHeatLoss(int minSteps, int maxSteps)
    {
        var queue = new PriorityQueue<(int row, int column, Direction direction, int Steps, int Cost), int>();

        var visited = new HashSet<string>();

        queue.Enqueue((0, 0, Direction.East, 1, 0), map[0, 0]);
        queue.Enqueue((0, 0, Direction.South, 1, 0), map[0, 0]);

        while (queue.Count > 0)
        {
            var item = queue.Dequeue();

            if (item.row == len - 1 && item.column == height - 1 && item.Steps >= minSteps - 1)
            {
                return item.Cost;
            }

            var directions = new[] {Direction.North, Direction.East, Direction.South, Direction.West };

            if (item.Steps < minSteps - 1)
            {
                switch (item.direction)
                {
                    case Direction.North:
                        if (item.column - (minSteps - item.Steps) < 0)
                        {
                            continue;
                        }

                        directions[1] = directions[2] = directions[3] = Direction.None;
                        break;

                    case Direction.East:
                        if (item.row + (minSteps - item.Steps) > len)
                        {
                            continue;
                        }
                        directions[0] = directions[2] = directions[3] = Direction.None;

                        break;

                    case Direction.South:
                        if (item.column + (minSteps - item.Steps) > height)
                        {
                            continue;
                        }
                        directions[0] = directions[1] = directions[3] = Direction.None;
                        
                        break;

                    case Direction.West:
                        if (item.row + (minSteps - item.Steps) < 0)
                        {
                            continue;
                        }
                        directions[0] = directions[1] = directions[2] = Direction.None;

                        break;
                }
            }

            switch (item.direction)
            {
                case Direction.North:
                    directions[2] = Direction.None;
                    break;

                case Direction.East:
                    directions[3] = Direction.None;
                    break;

                case Direction.South:
                    directions[0] = Direction.None;
                    break;

                case Direction.West:
                    directions[1] = Direction.None;
                    break;
            }

            for (var i = 0; i < 4; i++)
            {
                if (directions[i] == Direction.None)
                {
                    continue;
                }

                var newSteps = directions[i] == item.direction ? item.Steps + 1 : 0;

                if (newSteps == maxSteps)
                {
                    continue;
                }

                if (directions[i] == Direction.East && item.row < len - 1)
                {
                    var key = $"{item.row},{item.column},{item.row + 1},{item.column},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((item.row + 1, Y: item.column, Direction.East, newSteps, item.Cost + map[item.row + 1, item.column]),
                            item.Cost + map[item.row + 1, item.column]);
                    }
                }

                if (directions[i] == Direction.South && item.column < height - 1)
                {
                    var key = $"{item.row},{item.column},{item.row},{item.column + 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((X: item.row, item.column + 1, Direction.South, newSteps, item.Cost + map[item.row, item.column + 1]),
                            item.Cost + map[item.row, item.column + 1]);
                    }
                }

                if (directions[i] == Direction.North && item.column > 0)
                {
                    var key = $"{item.row},{item.column},{item.row},{item.column - 1},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((X: item.row, item.column - 1, Direction.North, newSteps, item.Cost + map[item.row, item.column - 1]),
                            item.Cost + map[item.row, item.column - 1]);
                    }
                }

                if (directions[i] == Direction.West && item.row > 0)
                {
                    var key = $"{item.row},{item.column},{item.row - 1},{item.column},{newSteps}";

                    if (visited.Add(key))
                    {
                        visited.Add(key);

                        queue.Enqueue((item.row - 1, Y: item.column, Direction.West, newSteps, item.Cost + map[item.row - 1, item.column]),
                            item.Cost + map[item.row - 1, item.column]);
                    }
                }
            }
        }

        return 0;
    }
}

   
