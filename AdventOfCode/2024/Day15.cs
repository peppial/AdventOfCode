namespace AdventOfCode._2024;

public class Day15 : IDay
{
    enum Symbol
    {
        None = 0,
        Wall = 1,
        Box = 2,
        Robot = 3,
        LeftBox = 4,
        RightBox = 5,
    }

    Symbol[,] map1;
    Symbol[,] map2;
    
    List<(int x, int y)> instructions;
    (int x, int y) robotInitPos;

    public Day15(string[] lines)
    {
        int rows = 0;
        
        while (lines[rows] != "") rows++;
        
        map1 = new Symbol[lines[0].Length, rows];
        map2 = new Symbol[lines[0].Length * 2, rows];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                switch (lines[y][x])
                {
                    case '#':
                        map1[x, y] = Symbol.Wall;
                        map2[x * 2, y] = Symbol.Wall;
                        map2[x * 2 + 1, y] = Symbol.Wall;
                        break;
                    case '.':
                        map1[x, y] = Symbol.None;
                        map2[x * 2, y] = Symbol.None;
                        map2[x * 2 + 1, y] = Symbol.None;
                        break;
                    case 'O':
                        map1[x, y] = Symbol.Box;
                        map2[x * 2, y] = Symbol.LeftBox;
                        map2[x * 2 + 1, y] = Symbol.RightBox;
                        break;
                    case '@':
                        map1[x, y] = Symbol.Robot;
                        map2[x * 2, y] = Symbol.Robot;
                        map2[x * 2 + 1, y] = Symbol.None;
                        robotInitPos = (x, y);
                        break;
                }
            }
        }
        instructions = [];
        for (int y = rows; y < lines.Length; y++)
        {
            foreach (char c in lines[y])
            {
                switch (c)
                {
                    case '^':
                        instructions.Add((0, -1));
                        break;
                    case 'v':
                        instructions.Add((0, 1));
                        break;
                    case '<':
                        instructions.Add((-1, 0));
                        break;
                    case '>':
                        instructions.Add((1, 0));
                        break;
                }
            }
        }

    }
    public long GetTotalPartA()
    {
        return SumOfBoxes(map1, robotInitPos.x, robotInitPos.y);
    }

    public long GetTotalPartB()
    {
        return SumOfBoxes(map2, robotInitPos.x*2, robotInitPos.y);
    }
  
    private long SumOfBoxes(Symbol[,] map, int robotX, int robotY)
    {
        long res = 0;
        foreach (var (x, y) in instructions)
        {
            if (Move(ref map, robotX, robotY, x, y))
            {
                robotX += x;
                robotY += y;
            }
        }

        for (int x = 0; x < map.GetLength(0); x++)
        for (int y = 0; y < map.GetLength(1); y++)
            if (map[x, y] == Symbol.Box || map[x, y] == Symbol.LeftBox)
                res += x + 100 * y;
        
        return res;
    }

    private bool Move(ref Symbol[,] map, int x, int y, int dx, int dy)
    {
        Symbol symbol = map[x, y];
        if (symbol == Symbol.Wall) return false;
        if (symbol == Symbol.None) return true;
        if (symbol is Symbol.Robot or Symbol.Box)
        {
            if (Move(ref map, x + dx, y + dy, dx, dy))
            {
                map[x + dx, y + dy] = symbol;
                map[x, y] = Symbol.None;
                return true;
            }

            return false;
        }

        int xLeftBox = x;
        int yLeftBox = y;
        int xRightBox = x;
        int yRightBox = y;
        
        if (symbol == Symbol.RightBox) xLeftBox--;
        if (symbol == Symbol.LeftBox) xRightBox++;
        
        if (dx == 0) 
        {
            var map1 = (Symbol[,]) map.Clone();
            if (Move(ref map, xLeftBox + dx, yLeftBox + dy, dx, dy) &&
                Move(ref map, xRightBox + dx, yRightBox + dy, dx, dy))
            {
                map[xLeftBox, yLeftBox + dy] = Symbol.LeftBox;
                map[xRightBox, yRightBox + dy] = Symbol.RightBox;
                map[xLeftBox, yLeftBox] = Symbol.None;
                map[xRightBox, yRightBox] = Symbol.None;
                return true;
            }

            map = map1;
        }
        else if (dx > 0) 
        {
            if (Move(ref map, xRightBox + dx, yRightBox + dy, dx, dy))
            {
                map[xLeftBox + dx, yLeftBox + dy] = Symbol.LeftBox;
                map[xRightBox + dx, yRightBox + dy] = Symbol.RightBox;
                map[x, y] = Symbol.None;
                return true;
            }
        }
        else
        {
            if (Move(ref map, xLeftBox + dx, yLeftBox + dy, dx, dy))
            {
                map[xLeftBox + dx, yLeftBox + dy] = Symbol.LeftBox;
                map[xRightBox + dx, yRightBox + dy] = Symbol.RightBox;
                map[x, y] = Symbol.None;
                return true;
            }
        }

        return false;
    }
}

