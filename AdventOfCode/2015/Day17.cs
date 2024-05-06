using System.Linq;
using AdventOfCode;

namespace AdventOfCode._2015;

public class Day17(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        return Calculate(false);
    }
    

    public long GetTotalPartB()
    {
        return Calculate(true);
    }

    private long Calculate(bool partB)
    {
        var jars = InitJars();

        int sum = 150;
        int total = 0;

        Dictionary<int, int> dict = [];
        for (int i = 0; i < jars.Count; i++)
        {
            var combinations = jars.ToArray().Combinations(i);
            foreach (var combination in combinations)
            {
               
                if (combination.Sum() == sum)
                {
                    total++;
                    dict.TryAdd(i, 0);
                    dict[i]++;
                }
            }
        }

        return partB?dict.MinBy(x=>x.Key).Value:total;
    }
    private List<int> InitJars()
    {
        List<int> jars = [];

        foreach (var line in lines)
        {
            jars.Add(int.Parse(line));
        }

        return jars;
    }
}