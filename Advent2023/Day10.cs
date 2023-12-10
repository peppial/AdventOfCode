namespace Advent2023;

public class Day10(string[] lines): IDay
{
    
    public long GetTotalPartA()
    {
        (var row, var column) = GetSPosition();

        Dictionary<char, int[]> tiledict = GetDictionary();

        static int[] Sub(int[] src, int[] dir)
        {
            int[] result = new int[src.Length];

            for (int i = 0; i < src.Length; i++)
            {
                result[i] = src[i] - dir[(i + 2) % 4];
            }
            return result;
        }

        int steps = 1;
        int[] direction = { 0, 0, 1, 0 };
        row += 1;
        char tile = lines[row][column];

        while (tile != 'S')
        {
            direction = Sub(tiledict[tile], direction);
            row += direction[2] - direction[0];
            column += direction[3] - direction[1];
            tile = lines[row][column];
            steps += 1;
        }

        return steps / 2;
    }

    public long GetTotalPartB()
    {
        Dictionary<char, int[]> tiledict = GetDictionary();
        HashSet<(int, int)> loop = new HashSet<(int, int)>();
        HashSet<(int, int)> cw = new HashSet<(int, int)>();
        HashSet<(int, int)> ccw = new HashSet<(int, int)>();

        (var row, var column) = GetSPosition();

        (int[], int) Sub(int[] src, int[] dir)
        {
            int[] newDirection = new int[src.Length];
            Array.Copy(src, newDirection, src.Length);
            for (int i = 0; i < src.Length; i++)
            {
                newDirection[i] -= dir[(i + 2) % 4];
            }
            int rotation = Array.IndexOf(newDirection, 1) - Array.IndexOf(dir, 1);
            if (rotation != 0)
            {
                rotation = (4 - rotation) % 4 - 2;
            }
            return (newDirection, rotation);
        }

        int[] directions = [0, 0, 1, 0 ];
        row += 1;
        char tile = lines[row][column];
        int totalRotations = 0;

        while (tile != 'S')
        {
            loop.Add((row, column));
            int j = Array.IndexOf(directions, 1);
            (directions, int rotation) = Sub(tiledict[tile], directions);
            totalRotations += rotation;
            (int, int)[] neighbours = new (int, int)[]
            {
                (row, column + 1), (row - 1, column), (row, column - 1), (row + 1, column),
                (row + 1, column + 1), (row + 1, column - 1), (row - 1, column + 1), (row - 1, column - 1)
            };
            int i = Array.IndexOf(directions, 1);
            cw.Add(neighbours[i]);
            ccw.Add(neighbours[(i + 2) % 4]);
            cw.Add(neighbours[j]);
            ccw.Add(neighbours[(j + 2) % 4]);
            row += directions[2] - directions[0];
            column += directions[3] - directions[1];
            tile = lines[row][column];
        }

        HashSet<(int, int)> adjacent = totalRotations < 0 ? cw : ccw;
        HashSet<(int, int)> inside = new HashSet<(int, int)>();

        bool Valid(int r, int c)
        {
            return r >= 0 && r < lines.Length && c >= 0 && c < lines[0].Length;
        }

        while (adjacent.Count > 0)
        {
            (int, int) coord = adjacent.First();
            adjacent.Remove(coord);
            int rowItem = coord.Item1;
            int colItem = coord.Item2;
            if (!Valid(rowItem, colItem) || inside.Contains(coord) || loop.Contains(coord))
            {
                continue;
            }
            inside.Add(coord);
            (int, int)[] neighbours = new (int, int)[]
            {
                (rowItem, colItem + 1), (rowItem + 1, colItem), (rowItem, colItem - 1), (rowItem - 1, colItem)
            };
            foreach ((int, int) n in neighbours)
            {
                if (Valid(n.Item1, n.Item2) && !loop.Contains(n) && !adjacent.Contains(n) && !inside.Contains(n))
                {
                    adjacent.Add(n);
                }
            }
        }

        return inside.Count;
    }
    
    private Dictionary<char, int[]> GetDictionary() => new (){
        { '|', new int[] { 1, 0, 1, 0 } },
        { '-', new int[] { 0, 1, 0, 1 } },
        { 'L', new int[] { 1, 0, 0, 1 } },
        { 'J', new int[] { 1, 1, 0, 0 } },
        { '7', new int[] { 0, 1, 1, 0 } },
        { 'F', new int[] { 0, 0, 1, 1 } },
        { '.', new int[] { 0, 0, 0, 0 } },
        { 'S', new int[] { 1, 1, 1, 1 } }
    };
    private (int,int) GetSPosition()
    {
        var item = lines.Select((line, index) => new { line , index }).
            First(x => x.line.IndexOf('S') >= 0);
        int row = item.index;
        int column = item.line.IndexOf('S');
        return (row,column);
    }
}


