namespace Advent2023.Structures;

using AdventOfCode.Extensions;
public record Coordinate3D(int X, int Y, int Z)
{
    public Coordinate3D(string line):this(0,0,0)
    {
        var n = line.GetNumbers().ToArray();
        X = n[0];
        Y = n[1];
        Z = n[2];
    }

    public static implicit operator Coordinate3D((int x, int y, int z) a) => new(a.x, a.y, a.z);

    public static implicit operator (int x, int y, int z)(Coordinate3D a) => (a.X, a.Y, a.Z);
    public static Coordinate3D operator +(Coordinate3D a) => a;
    public static Coordinate3D operator +(Coordinate3D a, Coordinate3D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Coordinate3D operator -(Coordinate3D a) => new(-a.X, -a.Y, -a.Z);
    public static Coordinate3D operator -(Coordinate3D a, Coordinate3D b) => a + (-b);

    public int ManhattanDistance(Coordinate3D other) =>
        (int)(Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z));

    public int ManhattanMagnitude() => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

    public override int GetHashCode()
    {
        return (137 * X + 149 * Y + 163 * Z);
    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }

    public static Coordinate3D[] GetNeighbours()
    {
        return neighbors3D;
    }

    private static readonly Coordinate3D[] neighbors3D =
    {
        (-1, -1, -1), (-1, -1, 0), (-1, -1, 1), (-1, 0, -1), (-1, 0, 0), (-1, 0, 1), (-1, 1, -1), (-1, 1, 0),
        (-1, 1, 1),
        (0, -1, -1), (0, -1, 0), (0, -1, 1), (0, 0, -1), (0, 0, 1), (0, 1, -1), (0, 1, 0), (0, 1, 1),
        (1, -1, -1), (1, -1, 0), (1, -1, 1), (1, 0, -1), (1, 0, 0), (1, 0, 1), (1, 1, -1), (1, 1, 0), (1, 1, 1)
    };

    public List<Coordinate3D> Rotations => new()
    {
        (X, Y, Z),
        (X, Z, -Y),
        (X, -Y, -Z),
        (X, -Z, Y),
        (Y, X, -Z),
        (Y, Z, X),
        (Y, -X, Z),
        (Y, -Z, -X),
        (Z, X, Y),
        (Z, Y, -X),
        (Z, -X, -Y),
        (Z, -Y, X),
        (-X, Y, -Z),
        (-X, Z, Y),
        (-X, -Y, Z),
        (-X, -Z, -Y),
        (-Y, X, Z),
        (-Y, Z, -X),
        (-Y, -X, -Z),
        (-Y, -Z, X),
        (-Z, X, -Y),
        (-Z, Y, X),
        (-Z, -X, Y),
        (-Z, -Y, -X)
    };
}
    