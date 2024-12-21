namespace AdventOfCode._2024;

public class Day21(string[] lines) : IDay
{
    HashSet<(int, int)> allowedNumPos = [..numericKeys.Values];
    HashSet<(int, int)> allowedDirPos = [..directionalKeys.Values];
    readonly Dictionary<string, Dictionary<string, long>> cache = new();

    static Dictionary<string, (int, int)> numericKeys = new()
    {
        ["7"] = (0, 0), ["8"] = (1, 0), ["9"] = (2, 0),
        ["4"] = (0, 1), ["5"] = (1, 1), ["6"] = (2, 1),
        ["1"] = (0, 2), ["2"] = (1, 2), ["3"] = (2, 2),
        ["0"] = (1, 3), ["A"] = (2, 3)
    };

    static Dictionary<string, (int, int)> directionalKeys = new()
    {
        ["^"] = (1, 0), ["A"] = (2, 0),
        ["<"] = (0, 1), ["v"] = (1, 1), [">"] = (2, 1)
    };

    public long GetTotalPartA()
    {
        return FindTotalComplexity(lines, 2);
    }

    public long GetTotalPartB()
    {
        return FindTotalComplexity(lines, 25);
    }

    List<string> GetAllKeystrokes((int, int) src, (int, int) target, bool directional)
    {
        if (src == target) return ["A"];
        if (!directional && !allowedNumPos.Contains(src)) return [];
        if (directional && !allowedDirPos.Contains(src)) return [];

        var (x1, y1) = src;
        var (x2, y2) = target;
        List<string> res = [];

        if (x1 < x2)
            res.AddRange(GetAllKeystrokes((x1 + 1, y1), target, directional).Select(s => ">" + s));
        else if (x1 > x2)
            res.AddRange(GetAllKeystrokes((x1 - 1, y1), target, directional).Select(s => "<" + s));

        if (y1 < y2)
            res.AddRange(GetAllKeystrokes((x1, y1 + 1), target, directional).Select(s => "v" + s));
        else if (y1 > y2)
            res.AddRange(GetAllKeystrokes((x1, y1 - 1), target, directional).Select(s => "^" + s));

        return res;
    }

    long FindShortestToClick(string a, string b, int depth = 2)
    {
        var cacheKey = $"{a}_{b}_{depth}";
        if (cache.ContainsKey(cacheKey) && cache[cacheKey].ContainsKey(a))
            return cache[cacheKey][a];

        var opts = GetAllKeystrokes(directionalKeys[a], directionalKeys[b], true);
        if (depth == 1)
        {
            var result = opts.Min(x => x.Length);
            cache[cacheKey] = new Dictionary<string, long> { [a] = result };
            return result;
        }

        List<long> tmps = [];
        foreach (var o in opts)
        {
            var tmp = new List<long>
            {
                FindShortestToClick("A", o[0].ToString(), depth - 1)
            };

            for (int i = 1; i < o.Length; i++)
            {
                tmp.Add(FindShortestToClick(o[i - 1].ToString(), o[i].ToString(), depth - 1));
            }

            tmps.Add(tmp.Sum());
        }

        var minResult = tmps.Min();
        cache[cacheKey] = new() { [a] = minResult };
        return minResult;
    }

    long FindShortest(string code, int levels)
    {
        var pos = numericKeys["A"];
        long shortest = 0;

        foreach (var key in code)
        {
            var possibleKeySequences = GetAllKeystrokes(pos, numericKeys[key.ToString()], false);
            var tmps = new List<long>();

            foreach (var sequence in possibleKeySequences)
            {
                var tmp = new List<long>
                {
                    FindShortestToClick("A", sequence[0].ToString(), levels)
                };

                for (int i = 1; i < sequence.Length; i++)
                {
                    tmp.Add(FindShortestToClick(
                        sequence[i - 1].ToString(),
                        sequence[i].ToString(),
                        levels));
                }

                tmps.Add(tmp.Sum());
            }

            pos = numericKeys[key.ToString()];
            if (tmps.Count > 0) shortest += tmps.Min();
        }

        return shortest;
    }

    long FindTotalComplexity(string[] codes, int levels)
    {
        return codes.Sum(code => FindShortest(code, levels) * int.Parse(code[..^1]));
    }
}