using AdventOfCode;

namespace AdventOfCode._2015;

public class Day24(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        long[] values0 = lines.Select(long.Parse).ToArray();
        var total = values0.Sum();
        var sum = total / 3;

        List<List<long>> group1=[]; 
        GetGroups(values0, sum, group1);

        long minQE = long.MaxValue;
        List<(long, long)> result = [];
        foreach (var g in group1)
        {
            var values1 = values0.Except(g).ToArray();
            List<List<long>> group2 = [];
            GetGroups(values1, sum, group2);
            foreach (var gg in group2)
            {
                var values2 = values0.Except(g).Except(gg).ToArray();
                if (values2.Sum() == sum )
                {
                    long qe = g.Aggregate((a, x) => a * x);
                    result.Add((g.Count,qe));
                }
            }
        }

        return result.OrderBy(count => count.Item1).ThenBy(sum => sum).First().Item2;
    }

   

    public long GetTotalPartB()
    {
        long[] values0 = lines.Select(long.Parse).ToArray();
        var total = values0.Sum();
        var sum = total / 4;

        List<List<long>> group1=[]; 
        GetGroups(values0, sum, group1);

        long minQE = long.MaxValue;
        List<(long, long)> result = [];
        foreach (var g in group1)
        {
            var values1 = values0.Except(g).ToArray();
            List<List<long>> group2 = [];
            GetGroups(values1, sum, group2);
            foreach (var gg in group2)
            {
                var values2 = values0.Except(g).Except(gg).ToArray();
                List<List<long>> group3 = [];
                GetGroups(values1, sum, group3);
                foreach (var ggg in group3)
                {
                    var values3 = values0.Except(g).Except(gg).Except(ggg).ToArray();
                    if (values3.Sum() == sum)
                    {

                        long qe = g.Aggregate((a, x) => a * x);
                        result.Add((g.Count, qe));
                    }
                }
            }
        }

        return result.OrderBy(count => count.Item1).ThenBy(sum => sum).First().Item2;    }
    
    private static void GetGroups(long[] values0, long sum, List<List<long>> group)
    {
        for (int i = 1; i < 7; i++)
        {
            var sets = values0.Combinations(i).ToList();
            foreach(var set in sets)
            {
                if(set.Sum()==sum) group.Add(set);
            }
            
        }
    }
}