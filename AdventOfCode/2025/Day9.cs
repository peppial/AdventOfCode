using Advent2023;
using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day9(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(lines
            .Select(x => x.GetNumbersLong())
            .ToList(), []);

    public long GetTotalPartB() => CalculateB();
    private long CalculateB()
    {
        var points = lines
            .Select(x => x.GetNumbersLong())
            .ToList();

        var rowBounds = new Dictionary<long, (long minX, long maxX)>();

        var pointsByY = points.GroupBy(p => p[1]).ToDictionary(g => g.Key, g => g.Select(p => p[0]).OrderBy(x => x).ToList());
        foreach (var kvp in pointsByY)
        {
            long y = kvp.Key;
            var xValues = kvp.Value;
            long segMinX = xValues.First();
            long segMaxX = xValues.Last();
            if (rowBounds.TryGetValue(y, out var existing))
                rowBounds[y] = (Math.Min(existing.minX, segMinX), Math.Max(existing.maxX, segMaxX));
            else
                rowBounds[y] = (segMinX, segMaxX);
        }

        var pointsByX = points.GroupBy(p => p[0]).ToDictionary(g => g.Key, g => g.Select(p => p[1]).OrderBy(y => y).ToList());
        var verticalSegments = new List<(long x, long minY, long maxY)>();
        foreach (var kvp in pointsByX)
        {
            long x = kvp.Key;
            var yValues = kvp.Value;
            for (int i = 0; i < yValues.Count - 1; i++)
            {
                verticalSegments.Add((x, yValues[i], yValues[i + 1]));
            }
        }

        long globalMinY = points.Min(p => p[1]);
        long globalMaxY = points.Max(p => p[1]);
        verticalSegments = verticalSegments.OrderBy(s => s.minY).ToList();

        var allYs = points.Select(p => p[1]).Distinct().OrderBy(y => y).ToList();

        for (int i = 0; i < allYs.Count - 1; i++)
        {
            long y1 = allYs[i];
            long y2 = allYs[i + 1];

            var xsAtY1 = verticalSegments.Where(s => s.minY <= y1 && s.maxY >= y1).Select(s => s.x).ToList();
            if (xsAtY1.Count >= 2)
            {
                long minX = xsAtY1.Min();
                long maxX = xsAtY1.Max();

                for (long y = y1; y <= y2; y++)
                {
                    if (rowBounds.TryGetValue(y, out var existing))
                        rowBounds[y] = (Math.Min(existing.minX, minX), Math.Max(existing.maxX, maxX));
                    else
                        rowBounds[y] = (minX, maxX);
                }
            }
        }

        return Calculate(points, rowBounds);
    }
    private long Calculate(List<long[]> points, Dictionary<long, (long minX, long maxX)> rowBounds)
    {
        bool partA = rowBounds.Count == 0;
        long max = 0;

        var sortedPairs = new List<(long[] p1, long[] p2, long area, long minX, long maxX, long minY, long maxY)>();
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var p1 = points[i];
                var p2 = points[j];
                var dx = Math.Abs(p2[0] - p1[0]) + 1;
                var dy = Math.Abs(p2[1] - p1[1]) + 1;
                var area = dx * dy;
                if (partA)
                {
                    if (area > max) max = area;
                    continue;
                }
                if (area < max) continue;
                sortedPairs.Add((p1, p2, area,
                    Math.Min(p1[0], p2[0]), Math.Max(p1[0], p2[0]),
                    Math.Min(p1[1], p2[1]), Math.Max(p1[1], p2[1])));
            }
        }
        if (partA) return max;

        sortedPairs = sortedPairs.OrderByDescending(p => p.area).ToList();

        foreach (var (p1, p2, area, minX, maxX, minY, maxY) in sortedPairs)
        {
            if (area <= max) break;

            bool allRowsValid = true;
            for (long y = minY; y <= maxY && allRowsValid; y++)
            {

                if (!rowBounds.TryGetValue(y, out var bounds) ||
                    bounds.minX > minX || bounds.maxX < maxX)
                {
                    allRowsValid = false;
                }
            }

            if (allRowsValid)
            {
                max = area;
            }
        }
        return max;
    }
}
