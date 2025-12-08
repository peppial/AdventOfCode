using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day8(string[] lines) : IDay
{
    public long GetTotalPartA() => Calculate(false);

    public long GetTotalPartB() => Calculate(true);

    private long Calculate(bool partB)
    {
        var points = lines
            .Select(x => x.GetNumbers())
            .ToList();

        var distances = new List<(long Dist, int I, int J)>();
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                long dx = points[i][0] - points[j][0];
                long dy = points[i][1] - points[j][1];
                long dz = points[i][2] - points[j][2];
                var dist = dx * dx + dy * dy + dz * dz;
                distances.Add((dist, i, j));
            }
        }

        distances.Sort((a, b) => a.Dist.CompareTo(b.Dist));

        var parent = Enumerable.Range(0, points.Count).ToArray();
        var rank = new int[points.Count];

        int Find(int x)
        {
            if (parent[x] != x)
                parent[x] = Find(parent[x]);
            return parent[x];
        }

        bool Union(int x, int y)
        {
            var px = Find(x);
            var py = Find(y);
            if (px == py) return false;
            if (rank[px] < rank[py]) parent[px] = py;
            else if (rank[px] > rank[py]) parent[py] = px;
            else { parent[py] = px; rank[px]++; }
            return true;
        }

        if (partB)
        {
            int numCircuits = points.Count;
            int lastI = 0, lastJ = 0;

            foreach (var (_, i, j) in distances)
            {
                if (Union(i, j))
                {
                    numCircuits--;
                    lastI = i;
                    lastJ = j;
                    if (numCircuits == 1) break;
                }
            }

            return (long)points[lastI][0] * points[lastJ][0];
        }
        else
        {
            int connections = lines.Length;
            for (int i = 0; i < connections && i < distances.Count; i++)
            {
                Union(distances[i].I, distances[i].J);
            }

            var circuitSizes = new Dictionary<int, int>();
            for (int i = 0; i < points.Count; i++)
            {
                var root = Find(i);
                circuitSizes[root] = circuitSizes.GetValueOrDefault(root, 0) + 1;
            }

            var top3 = circuitSizes.Values.OrderByDescending(x => x).Take(3).ToList();
            return top3.Aggregate(1L, (acc, x) => acc * x);
        }
    }
}
