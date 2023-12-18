using System.ComponentModel.DataAnnotations.Schema;

namespace Advent2023;

public class Day18(string[] lines) : IDay
{
    
    Dictionary<char, (int, int)> deltas = new ()
    {
        { 'R', (1, 0) },
        { 'L', (-1, 0) },
        { 'U', (0, -1) },
        { 'D', (0, 1) }
    };

    Dictionary<char, char> hex2d = new()
    {
        { '0', 'R' },
        { '1', 'D' },
        { '2', 'L' },
        { '3', 'U' }
    };
    
    public long GetTotalPartA()
    {
        int count = 0;
        var polygon = GetPolygon(true, ref count);
        return ShoeLacePolygon(polygon) + count / 2 + 1;
    }

    private (int, int)[] GetPolygon(bool partA, ref int count)
    {
        var  polygon = new[] { (0, 0) };
        foreach (var line in lines)
        {
            var parts = line.Split();
            (int X, int Y) direction = partA?deltas[parts[0][0]]:deltas[hex2d[parts[2][^2]]];
            (int X, int Y) lastPoint = polygon[polygon.Length - 1];
            var steps = partA
                ? int.Parse(parts[1])
                : int.Parse(parts[2][2..^2], System.Globalization.NumberStyles.HexNumber);
            count += steps;
            polygon = polygon.Append((lastPoint.X + direction.X * steps, lastPoint.Y + direction.Y * steps)).ToArray();
        }

        return polygon;
    }

    public long GetTotalPartB()
    {
        int count = 0;
        var polygon = GetPolygon(false, ref count);
        return ShoeLacePolygon(polygon) + count / 2 + 1;
    }
    
    private static long ShoeLacePolygon((int X, int Y)[] points)
    {
        long res = 0;
        for (int i = 0; i < points.Length; i++)
        {
            int nextIndex = (i + 1) % points.Length;
            res += (long)(points[i].X) * points[nextIndex].Y - (long)(points[i].Y) * points[nextIndex].X;
        }
        return res / 2;
    }

}
