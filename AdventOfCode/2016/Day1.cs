using AdventOfCode.Extensions;

namespace AdventOfCode._2016;

public class Day1(string[] lines):IDay
{
    enum Direction
    {
        North,
        East,
        South,
        West
    }
    
    HashSet<(int, int)> positions = [];
    public long GetTotalPartA()
    {
        var (X,Y) = Iterate(true);
        return Math.Abs(X)+ Math.Abs(Y);
    }

    private (int,int) Iterate(bool partA)
    {
        string[] instructions = lines[0].SplitDefault(", ");
        int[,] grid = new int[1000, 1000];
        int X = 0;
        int Y = 0;
        
        Direction direction = Direction.North;
        
        
        foreach (string s in instructions)
        {
            bool left = s[0] == 'L';
            if (!left)
            {
                direction += 1;
                if ((int)direction == 4) direction = Direction.North;
            }
            else
            {
                direction -= 1;
                if ((int)direction == -1) direction = Direction.West;
            }

            int blocks = int.Parse(s.Substring(1));
            if (direction == Direction.North)
            {
                int prevY = Y;
                Y -= blocks;
                var pos= AddPositionsY(prevY, Y, X);
                if (pos.Item1 is not null) return (pos.Item1.Value,pos.Item2.Value);
            }
            else if (direction == Direction.South)
            {
                int prevY = Y;
                Y += blocks;
                var pos= AddPositionsY(prevY, Y, X);
                if (pos.Item1 is not null) return (pos.Item1.Value,pos.Item2.Value);
            }
            else if (direction == Direction.East)
            {
                int prevX = X;
                X += blocks;
                var pos= AddPositionsX(Y, prevX, X);
                if (pos.Item1 is not null) return (pos.Item1.Value,pos.Item2.Value);
            }
            else if (direction == Direction.West)
            {
                int prevX = X;
                X -= blocks;
                var pos= AddPositionsX(Y, prevX, X);
                if (pos.Item1 is not null) return (pos.Item1.Value,pos.Item2.Value);
            }
            
        }

        return (X,Y);
    }

    private (int?,int?) AddPositionsY(int prevY, int Y, int X)
    {
        if(prevY<Y)
        for (int i = prevY+1; i <= Y; i++)
        {
            if (positions.Contains((i, X))) return (i, X);
            positions.Add((i, X));
        }
        else
            for (int i = prevY-1; i >= Y; i--)
            {
                if (positions.Contains((i, X))) return (i, X);
                positions.Add((i, X));
            }
        return (null, null);
    }
    private (int?,int?) AddPositionsX(int Y,int prevX, int X)
    {
        if(prevX<X)
        for (int i = prevX+1; i <= X; i++)
        {
            if (positions.Contains((Y, i))) return (Y, i);
            positions.Add((Y, i));
        }
        else
            for (int i = prevX-1; i >= X; i--)
            {
                if (positions.Contains((Y, i))) return (Y, i);
                positions.Add((Y, i));
            }
        return (null, null);
    }
    public long GetTotalPartB()
    {
        var (X,Y) = Iterate(false);   
        return Math.Abs(X)+ Math.Abs(Y);
    }
}