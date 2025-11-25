using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day1(string[] lines):IDay
{
    public long GetTotalPartA()
    {
        List<int> list1 = [];
        List<int> list2 = [];
        
        foreach (string line in lines)
        {
            int[] parts = line.GetNumbers();
            list1.Add(parts[0]);
            list2.Add(parts[1]);
        }
        list1.Sort();
        list2.Sort();
        
        long count = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            count+=Math.Abs(list1[i] - list2[i]);
        }
        return count;
    }

    public long GetTotalPartB()
    {
        List<int> list1 = [];
        Dictionary<int,int> dict = [];
        
        foreach (string line in lines)
        {
            int[] parts = line.GetNumbers();
            list1.Add(parts[0]);
            if(dict.ContainsKey(parts[1])) dict[parts[1]]++;
            else dict[parts[1]] = 1;
        }

        long total = 0;
        
        foreach (int i in list1)
        {
            int count = 0;
            dict.TryGetValue(i, out count);
            total+=i*count;
        }
        return total;
    }
}