namespace Advent2023;

public record GridPoint(int row, int column): IComparable
{
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        GridPoint otherPoint = obj as GridPoint;
        if (this.row == otherPoint.row && this.column == otherPoint.column)
            return 0;
        else
            return -1;
    }
}