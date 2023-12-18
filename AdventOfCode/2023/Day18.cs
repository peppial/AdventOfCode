using System.ComponentModel.DataAnnotations.Schema;

namespace Advent2023;

public class Day18(string[] lines) : IDay
{
    
    Dictionary<char, (int, int)> deltas = new Dictionary<char, (int, int)>
    {
        { 'R', (1, 0) },
        { 'L', (-1, 0) },
        { 'U', (0, -1) },
        { 'D', (0, 1) }
    };

    Dictionary<char, char> hex2d = new Dictionary<char, char>
    {
        { '0', 'R' },
        { '1', 'D' },
        { '2', 'L' },
        { '3', 'U' }
    };
    
    public long GetTotalPartA()
    {
        var polygon = new[] { (0, 0) };

        int count = 0;

        foreach (var line in lines)
        {
            var parts = line.Split();
            (int column,int row) direction = deltas[parts[0][0]];
            (int column, int row) lastPoint = polygon[polygon.Length - 1];
            var steps = int.Parse(parts[1]);
            count += steps;
            Console.WriteLine((lastPoint.column + direction.column * steps, lastPoint.row + direction.row * steps).ToString());

            polygon = polygon.Append((lastPoint.column + direction.column * steps, lastPoint.row + direction.row * steps)).ToArray();
        }
        
        return ShoeLacePolygon(polygon) + count / 2 + 1;
    }

    public long GetTotalPartB()
    {
        var polygon = new[] { (0, 0) };

        int count = 0;

        foreach (var line in lines)
        {
            var parts = line.Split();
            (int column, int row) direction = deltas[hex2d[parts[2][^2]]];
            (int column, int row) lastPoint = polygon[polygon.Length - 1];
            var steps = int.Parse(parts[2][2..^2], System.Globalization.NumberStyles.HexNumber);
            count += steps;
            polygon = polygon.Append((lastPoint.column + direction.column * steps, lastPoint.row + direction.row * steps)).ToArray();
        }

        return ShoeLacePolygon(polygon) + (count + 1) / 2 + 1;
    }
    
    private static long ShoeLacePolygon((int row, int column)[] points)
    {
        long res = 0;
        for (int i = 0; i < points.Length; i++)
        {
            int nextIndex = (i + 1) % points.Length;
            res += (long)(points[i].row) * points[nextIndex].column - (long)(points[i].column) * points[nextIndex].row;
        }
        return res / 2;
    }

}
