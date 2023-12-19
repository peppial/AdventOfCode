using static System.Math;

namespace Advent2023.Structures;

public record RangePlus(long Start, long Len)
{
    public long End => Start + Len;
    public bool IsEmpty => Len <= 0;
    public bool Touches(RangePlus other) => End == other.Start || Start == other.End;
    public RangePlus Shift(long delta) => this with { Start = Start + delta };
    public bool Intersects(RangePlus other) => !IntersectWith(other).IsEmpty;
    public RangePlus IntersectWith(RangePlus other) => FromStartEnd(Max(Start, other.Start), Min(End, other.End));
    public static RangePlus FromStartEnd(long start, long end) => new(start, end - start);
    public RangePlus MakeGreaterThan(long value) => FromStartEnd(Max(Start, value + 1), End);
    public RangePlus MakeLessThan(long value) => FromStartEnd(Start, Min(End, value));
    public RangePlus MakeGreaterThanOrEqualTo(long value) => FromStartEnd(Max(Start, value), End);
    public RangePlus MakeLessThanOrEqualTo(long value) => FromStartEnd(Start, Min(End, value + 1));
    
    public IEnumerable<RangePlus> ExceptWith(IEnumerable<RangePlus> others)
    {
        var s = Start;
        foreach (var u in others.OrderBy(x => x.Start))
        {
            var part = FromStartEnd(s, u.Start);
            if (!part.IsEmpty)
                yield return part;
            s = u.End;
        }

        var lastPart = FromStartEnd(s, End);
        if (!lastPart.IsEmpty)
            yield return lastPart;
    }
}