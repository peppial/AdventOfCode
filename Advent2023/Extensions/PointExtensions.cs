namespace Advent2023;

public static class PointExtensions
{
    public static IEnumerable<(GridPoint,GridPoint)> Combinations(this List<GridPoint> items)
    {   
        return from d1 in items
            from d2 in items
            where (d1.X!=d2.X ||d1.Y!=d2.Y)
            select (d1,d2);
    }
}