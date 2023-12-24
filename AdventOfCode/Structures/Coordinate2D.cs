namespace Advent2023.Structures;


public record Coordinate2D(long X, long Y)
{
    public static readonly Coordinate2D origin = new(0, 0);
    public static readonly Coordinate2D unit_x = new(1, 0);
    public static readonly Coordinate2D unit_y = new(0, 1);
    
    public Coordinate2D((long x, long y) coord):this(0,0)
    {
        this.X = coord.x;
        this.Y = coord.y;
    }

    public Coordinate2D(string StringPair):this(0,0)
    {
        var t = StringPair.Split(',');
        X = long.Parse(t[0]);
        Y = long.Parse(t[1]);
    }

    public Coordinate2D RotateCW(int degrees, Coordinate2D center)
    {
        Coordinate2D offset = center - this;
        return center + offset.RotateCW(degrees);
    }

    public Coordinate2D RotateCW(int degrees)
    {
        return ((degrees / 90) % 4) switch
        {
            0 => this,
            1 => RotateCW(),
            2 => -this,
            3 => RotateCCW(),
            _ => this,
        };
    }

    private Coordinate2D RotateCW()
    {
        return new Coordinate2D(Y, -X);
    }

    public Coordinate2D RotateCCW(int degrees, Coordinate2D center)
    {
        Coordinate2D offset = center - this;
        return center + offset.RotateCCW(degrees);
    }

    public Coordinate2D RotateCCW(int degrees)
    {
        return ((degrees / 90) % 4) switch
        {
            0 => this,
            1 => RotateCCW(),
            2 => -this,
            3 => RotateCW(),
            _ => this,
        };
    }

    private Coordinate2D RotateCCW()
    {
        return new Coordinate2D(-Y, X);
    }

    public static Coordinate2D operator +(Coordinate2D a) => a;
    public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b) => new(a.X + b.X, a.Y + b.Y);
    public static Coordinate2D operator -(Coordinate2D a) => new(-a.X, -a.Y);
    public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b) => a + (-b);
    public static Coordinate2D operator *(int scale, Coordinate2D a) => new(scale * a.X, scale * a.Y);
 
    public static implicit operator Coordinate2D((long x, long y) a) => new(a.x, a.y);

    public static implicit operator (long x, long y)(Coordinate2D a) => (a.X, a.Y);

    public long ManDistance(Coordinate2D other)
    {
        long x = Math.Abs(this.X - other.X);
        long y = Math.Abs(this.Y - other.Y);
        return x + y;
    }

    public override int GetHashCode()
    {
        return (23 * X + Y).GetHashCode();
    }

    public override string ToString()
    {
        return string.Concat("(", X, ", ", Y, ")");
    }

    public void Deconstruct(out long xVal, out long yVal)
    {
        xVal = X;
        yVal = Y;
    }

}
