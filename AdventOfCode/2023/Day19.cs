using System.Text.RegularExpressions;
using Advent2023.Structures;
using Operation = (char arg, char op, long value, string res);
using Part = (long x, long m, long a, long s);
using Rule = (string name, (char arg, char op, long value, string res)[] operations, string otherwise);

namespace Advent2023;

public class Day19 : IDay
{
    private List<Rule> rules = [];
    private List<Part> partsList = [];

    public Day19(string lines)
    {
        var splitLines = Regex.Split(lines, "(?:\\r?\\n|\\r){2,}");
        string[] rulesStrings = splitLines[0].Split('\n');
        foreach (string rule in rulesStrings)
        {
            var match = Regex.Match(rule, @"^(?<name>\w+)\{(?<items>.*),(?<otherwise>\w+)\}$");
            string name = match.Groups[1].Value;
            string[] items = match.Groups[2].Value.Split(',');
            string otherwise = match.Groups[3].Value;

            Rule r = new Rule
            {
                name = name,
                otherwise = otherwise
            };

            List<Operation> ops = [];
            foreach (string item in items)
            {
                match = Regex.Match(item, @"^(?<arg>\w)(?<op>[><])(?<value>\d+)\:(?<res>\w+)$");
                char argument = match.Groups[1].Value[0];
                char operation = match.Groups[2].Value[0];
                long value = long.Parse(match.Groups[3].Value);
                string result = match.Groups[4].Value;
                ops.Add((argument, operation, value, result));

            }

            r.operations = ops.ToArray();
            rules.Add(r);
        }

        string[] partsStrings = splitLines[1].Split('\n');
        List<Part> partsList = new();
        foreach (string part in partsStrings)
        {
            string[] parts = part.Trim(new char[] { '{', '}' }).Split(',');
            long x = long.Parse(Regex.Match(parts[0], @"\d+").Value);
            long m = long.Parse(Regex.Match(parts[1], @"\d+").Value);
            long a = long.Parse(Regex.Match(parts[2], @"\d+").Value);
            long s = long.Parse(Regex.Match(parts[3], @"\d+").Value);
            partsList.Add((x, m, a, s));
        }
    }

    public long GetTotalPartA()
    {
        return partsList
            .Where(part => IsAccepted(part, "in"))
            .Sum(part => part.x + part.m + part.a + part.s);

        
    }
    public long GetTotalPartB()
    {
        var range = new RangePlus(1, 4000);
        return Count((x: range, m: range, a: range, s: range), "in");

        long Count((RangePlus x, RangePlus m, RangePlus a, RangePlus s) ranges, string ruleName)
        {
            var result = 0L;
            var rule = rules.Single(r => r.name == ruleName);
            foreach (var (arg, op, value, res) in rule.operations)
            {
                var r = GetPartValue(ranges, arg);
                var (matched, unmatched) = op switch
                {
                    '>' => (r.MakeGreaterThan(value), r.MakeLessThanOrEqualTo(value)),
                    '<' => (r.MakeLessThan(value), r.MakeGreaterThanOrEqualTo(value)),
                };
                if (!matched.IsEmpty)
                {
                    var next = SetPartValue(ranges, arg, matched);
                    result += res switch
                    {
                        "A" => next.x.Len * next.m.Len * next.a.Len * next.s.Len,
                        "R" => 0L,
                        _ => Count(next, res)
                    };
                }

                if (unmatched.IsEmpty)
                    return result;

                ranges = SetPartValue(ranges, arg, unmatched);
            }

            result += rule.otherwise switch
            {
                "A" => ranges.x.Len * ranges.m.Len * ranges.a.Len * ranges.s.Len,
                "R" => 0L,
                _ => Count(ranges, rule.otherwise)
            };
            return result;
        }

    }

    T GetPartValue<T>((T x, T m, T a, T s) part, char arg)
    {
        return arg switch
        {
            'x' => part.x,
            'm' => part.m,
            'a' => part.a,
            's' => part.s,
            _ => throw new Exception($"Bad part ref: {arg}")
        };
    }

    (T x, T m, T a, T s) SetPartValue<T>((T x, T m, T a, T s) part, char arg, T value)
    {
        return arg switch
        {
            'x' => part with { x = value },
            'm' => part with { m = value },
            'a' => part with { a = value },
            's' => part with { s = value },
            _ => throw new Exception($"Bad part ref: {arg}")
        };
    }
    
    bool IsAccepted((long x, long m, long a, long s) part, string ruleName)
    {
        var rule = rules.Single(r => r.name == ruleName);
        var next = rule.operations
                       .FirstOrDefault(
                           r => r.op == '<' && GetPartValue(part, r.arg) < r.value ||
                                r.op == '>' && GetPartValue(part, r.arg) > r.value
                       )
                       .res ??
                   rule.otherwise;
        return next switch
        {
            "A" => true,
            "R" => false,
            _ => IsAccepted(part, next)
        };
    }
}

