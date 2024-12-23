using System.Reflection;

namespace AdventOfCode._2024;

public class Day23 : IDay
{
    private readonly Dictionary<string, HashSet<string>> computers = [];
    
    public Day23(string[] lines)
    {
        foreach (var line in lines)
        {
            var id1 = line[0..2];
            var id2 = line[3..];
            computers.TryAdd(id1, []);
            computers.TryAdd(id2, []);
            computers[id1].Add(id2);
            computers[id2].Add(id1);
        }
    }
    public long GetTotalPartA()
    {
        HashSet<string> tripples = [];
        foreach (var computer1 in computers.Keys)
        {
            if (computer1[0] =='t')
            {
                foreach (var computer2 in computers[computer1])
                {
                    foreach (var computer3 in computers[computer2])
                    {
                        if (computers[computer3].Contains(computer1))
                        {
                            List<string> tripple = [computer1, computer2, computer3];
                            tripple.Sort();
                            tripples.Add(string.Join(',', tripple));
                        }
                    }
                }
            }
        }
        return tripples.Count;
    }
    public long GetTotalPartB()
    {
        int maxLanSize = computers.Values.Max(x => x.Count);
        Dictionary<string, HashSet<string>> tests = [];
        foreach (var computer in computers)
        {
            HashSet<string> test = new(computer.Value);
            test.Add(computer.Key);
            var l = test.ToList();
            l.Sort();
            tests.TryAdd(string.Join(",",l), test);
        }
        while (tests.First().Value.Count>1)
        {
            foreach (var test in tests)
            {
                if (CheckLanIsValid(test.Value))
                {
                    {
                        Console.WriteLine($"{test.Key}");
                        return 0;
                    }
                }
            }
            Dictionary<string, HashSet<string>> newTests = [];
            foreach (var test in tests)
            {
                foreach (var id in test.Value)
                {
                    HashSet<string> newTestVal = new(test.Value);
                    newTestVal.Remove(id);
                    var l = newTestVal.ToList();
                    l.Sort();
                    newTests.TryAdd(string.Join(",", l), newTestVal);
                }
            }
            tests = newTests;
        }
        return 0;
    }
    private bool CheckLanIsValid(HashSet<string> lan)
    {
        foreach (var computer in lan)
        {
            foreach (var otherComputer in lan)
            {
                if ((!computers[computer].Contains(otherComputer)) && (computer!=otherComputer))
                    return false;
            }
        }
        return true;
    }
    
}