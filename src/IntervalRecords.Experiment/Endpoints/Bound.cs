namespace IntervalRecords.Experiment.Endpoints;
public interface IBound
{
    static abstract Bound Bound { get; }
}

public interface IBounded : IBound { }

public struct Closed : IBounded
{
    public static Bound Bound => Bound.Closed;
}

public struct Open : IBounded
{
    public static Bound Bound => Bound.Open;
}

public struct Unbounded : IBound
{
    public static Bound Bound => Bound.Unbounded;
}

public enum Bound
{
    Open,
    Closed,
    Unbounded
}
