namespace Advent2023;
using Advent2023.Structures;

public class Day25(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        
        List<(string first, string second)> edges = [];
        foreach (string line in lines)
        {
            var parts = line.SplitDefault(":");
            var first = parts[0];
            foreach (string second in parts[1].SplitDefault(" " ))
            {
                edges.Add((first,second));
            }
        }
        
        var vertices = edges
            .SelectMany(t => new[] { t.first, t.second })
            .Distinct()
            .ToHashSet();

        var outgoing = edges
            .SelectMany(e => new[] { (e.first, e.second), (e.second, e.first) })
            .ToDictionary(t => t, _ => 1);

        var verticesCount = vertices.Count;

        var bestCost = int.MaxValue;
        var bestCut = new HashSet<string>();
        var removed = new HashSet<string>();
        var source = vertices.ToDictionary(v => v, v => new HashSet<string> { v });
        for (var ph = 0; ph < verticesCount - 1; ph++)
        {
            var a = new HashSet<string>();
            var w = vertices.ToDictionary(v => v, _ => 0);
            string previous = null!;
            for (var i = 0; i < verticesCount - ph; i++)
            {
                string selected = null!;
                foreach (var v in vertices)
                    if (!removed.Contains(v) && !a.Contains(v) && (selected == null || w[v] > w[selected]))
                        selected = v;
                if (i == verticesCount - ph - 1)
                {
                    if (w[selected] < bestCost)
                    {
                        bestCost = w[selected];
                        bestCut = source[selected].ToArray().ToHashSet();
                    }

                    source[previous].UnionWith(source[selected]);
                    foreach (var v in vertices)
                    {
                        if (!outgoing.ContainsKey((v, previous)))
                            outgoing[(v, previous)] = 0;
                        outgoing[(v, previous)] += outgoing.GetValueOrDefault((selected, v), 0);
                        outgoing[(previous, v)] = outgoing[(v, previous)];
                    }

                    removed.Add(selected);
                }
                else
                {
                    a.Add(selected);
                    foreach (var v in vertices)
                        w[v] += outgoing.GetValueOrDefault((selected, v));
                    previous = selected;
                }
            }
        }

        return bestCut.Count * (vertices.Count - bestCut.Count);
    }

    public long GetTotalPartB()
    {
        throw new NotImplementedException();
    }
    
}