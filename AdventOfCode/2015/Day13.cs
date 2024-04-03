using System.Security.Cryptography;
using AdventOfCode.Extensions;

namespace AdventOfCode._2015;

public class Day13(string[] lines):IDay
{
    HashSet<string> names = new();
    List<List<string>> all = new();

    public long GetTotalPartA()
    {
        return GetScore(false);
    }
    public long GetTotalPartB()
    {
        return GetScore(true);
    }
    private int GetScore(bool addMe)
    {
        List<(string, string, int)> weights = new();
        foreach (string line in lines)
        {
            int[] numbers = line.GetNumbers();
            string[] parts = line.Substring(0,line.Length-1).Split();
            string left = parts[0];
            string right = parts[^1];
            Console.WriteLine(left+right+parts[2]+numbers[0].ToString());
            names.Add(left);
            names.Add(right);
            int net = parts[2] == "gain" ? numbers[0] : -numbers[0];
            weights.Add((left,right,net));
            weights.Add((right,left,net));
        }

        if (addMe)
        {
            foreach (string name in names)
            {
                weights.Add(("me",name,0));
                weights.Add((name,"me",0));
            }

            names.Add("me");
        }

        Iterate(names,new List<string>());
        int max = 0;
        foreach (var list in all)
        {
            int current = 0;
            for(int i=0;i<list.Count;i++)
            {
                int secondIndex = (i+1) == list.Count ? 0 : i + 1;
                current += weights.Where(x => x.Item1 == list[i] && x.Item2 == list[secondIndex]).Sum(x=>x.Item3);

            }

            if (current > max) max = current;
        }

        return max;
    }

    private void Iterate(HashSet<string> set, List<string> list)
    {
        if(set.Count==0) all.Add(list);
        foreach (string name in set)
        {
            var hs = new HashSet<string>(set);
            hs.Remove(name);
            var l = new List<string>(list);
            l.Add(name);
            Iterate(hs,l);
            
        }
    }
   
}