using AdventOfCode.Extensions;

namespace AdventOfCode._2025;

public class Day11 : IDay
{
    private Dictionary<string, List<string>> dict;
    public Day11(string[] lines)
    {
        dict = [];
        foreach (var line in lines)
        {
            var split = line.Split(':');
            string[] neighbors = split[1].Split(' ');
            foreach (string n in neighbors)
            {
                dict.TryAdd(n, []);
                dict[n].Add(split[0]);
            }
        }

    }
    public long GetTotalPartA() => CalculateA();

    public long GetTotalPartB() => CalculateB();

    private Dictionary<(string, int), long> memo = [];

    private long CalculateB()
    {
        memo.Clear();
        return CountPaths("out", 0);
    }

    private long CountPaths(string current, int mask)
    {
        if (current == "dac") mask |= 1;
        if (current == "fft") mask |= 2;

        if (current == "svr")
        {
            return mask == 3 ? 1 : 0;
        }

        if (memo.TryGetValue((current, mask), out long count))
        {
            return count;
        }

        long total = 0;
        if (dict.ContainsKey(current))
        {
            foreach (var neighbor in dict[current])
            {
                total += CountPaths(neighbor, mask);
            }
        }

        memo[(current, mask)] = total;
        return total;
    }
    private long CalculateA()
    {

        Queue<(string, string)> queue = [];
        queue.Enqueue(("out", ""));

        HashSet<string> paths = [];
        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                var (node, path) = queue.Dequeue();

                if (path.Length > 50) continue;
                if (node == "you")
                {
                    Console.WriteLine(path);
                    paths.Add(path);

                }
                else
                    foreach (string neighbor in dict[node])
                    {
                        if (dict.ContainsKey(neighbor))
                        {
                            queue.Enqueue((neighbor, path + ":" + neighbor));
                        }
                    }
            }

        }
        return paths.Count;
    }

}
