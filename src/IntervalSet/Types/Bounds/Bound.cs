namespace IntervalSet.Types;

public enum Bound : byte
{
    Open = 0,
    Closed = 1
}

public interface IBound
{
    static abstract Bound Bound { get; }
}

public struct Open : IBound
{
    public static Bound Bound => Bound.Open;
}

public struct Closed : IBound
{
    public static Bound Bound => Bound.Closed;
}
