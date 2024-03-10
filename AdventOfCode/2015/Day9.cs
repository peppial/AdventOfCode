using System.Reflection.Metadata;
using Advent2023;

namespace AdventOfCode._2015;

public class Day9(string[] lines):IDay
{
    public record Route(string Start, string End, int Km);

    private List<string> all;
    List<Route> routes = [];
    private int grandtotal = int.MaxValue;

    public long GetTotalPartA()
    {
        return ComputeRoute(true);
    }

    private long ComputeRoute(bool partA)
    {
        foreach (string line in lines)
        {
            string[] parts = line.Replace(" to ", "-").Split(new char[] { '-', '=' },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            routes.Add(new Route(parts[0], parts[1], int.Parse(parts[2])));
        }

        all = routes.Select(x => x.Start).Union(routes.Select(x => x.End)).Distinct().ToList();
        var used = new HashSet<string>();
        for (int i = 0; i < all.Count; i++)
        {
            var next = all[i];
            all.RemoveAt(i);
            used.Add(next);
            GetRoute(all, used, next, 0, partA);
            used.Remove(next);
            all.Insert(i,next);
        }

        return grandtotal;
    }

    private void GetRoute(List<string> all, HashSet<string> used,string start, int total, bool partA)
    {
        if (all.Count == 0)
        {
            if (partA)
            {
                if (total < grandtotal) grandtotal = total;
            }
            else
            {
                if (total > grandtotal) grandtotal = total;
            }

            foreach (var u in used)
            {
                Console.Write(u);
            }

            Console.WriteLine();
            return;
        }

        var route = routes.Where(x => x.Start == start && !used.Contains(x.End)) ;
        foreach (var r in route)
        {
            all.Remove(r.End);
            used.Add(r.End);
            GetRoute(all, used, r.End, total + r.Km, partA);
            all.Add(r.End);
            used.Remove(r.End);
        }
        
        route = routes.Where(x => x.End == start && !used.Contains(x.Start));
        foreach (var r in route)
        {
            all.Remove(r.Start);
            used.Add(r.Start);
            GetRoute(all, used, r.Start, total + r.Km, partA);
            all.Add(r.Start);
            used.Remove(r.Start);
        }
    }

    public long GetTotalPartB()
    {
        grandtotal = 0;
        return ComputeRoute(false);
    }
}