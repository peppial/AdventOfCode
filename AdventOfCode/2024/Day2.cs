using System.ComponentModel;
using AdventOfCode.Extensions;

namespace AdventOfCode._2024;

public class Day2(string[] lines) : IDay
{
    public long GetTotalPartA()
    {
        return lines.Select(x => x.GetNumbers()).Count(r => r.IsSafe());
    }

    public long GetTotalPartB()
    {
        return lines.Select(x => x.GetNumbers()).Count(r => r.IsTolerateSafe());
    }
}

static class Extensions
{
    public static bool HasNoGaps(this IEnumerable<int> list)
    {
        return list
            .Zip(list.Skip(1))
            .All(p => int.Abs(p.First - p.Second) is >= 1 and <= 3);
    }

    public static bool IsInOrder(this IEnumerable<int> list)
        => list.Order().SequenceEqual(list) || list.OrderDescending().SequenceEqual(list);

    public static bool IsSafe(this IEnumerable<int> list)
        => list.IsInOrder() && list.HasNoGaps();

    public static bool IsTolerateSafe(this IEnumerable<int> list)
    {
        return Enumerable
            .Range(0, list.Count())
            .Any(i => list.Where((_, index) => index != i).IsSafe());
    }
}