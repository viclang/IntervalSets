namespace IntervalSet.Types;

public enum Bound : byte
{
    Open = 0,
    Closed = 1,
    Unbounded = 2
}

public interface IBound
{
    static abstract Bound Bound { get; }
}

public interface IBounded : IBound
{
}

public struct Open : IBounded
{
    public static Bound Bound => Bound.Open;
}

public struct Closed : IBounded
{
    public static Bound Bound => Bound.Closed;
}

public struct Unbounded : IBound
{
    public static Bound Bound => Bound.Unbounded;
}
