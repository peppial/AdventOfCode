namespace Advent2023;

public record TransposeGridPoint(int row, int column, int transposeRow=0, int transposeColumn=0): IComparable
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