namespace Advent2023;

public static class PointExtensions
{
    public static IEnumerable<(GridPoint,GridPoint)> Combinations(this List<GridPoint> items)
    {   
        return from d1 in items
            from d2 in items
            where (d1.row!=d2.row ||d1.column!=d2.column)
            select (d1,d2);
    }
}