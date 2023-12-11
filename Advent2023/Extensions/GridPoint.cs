namespace Advent2023;

public record GridPoint(int X, int Y): IComparable
{
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        GridPoint otherPoint = obj as GridPoint;
        if (this.X == otherPoint.X && this.Y == otherPoint.Y)
            return 0;
        else
            return -1;
    }
}