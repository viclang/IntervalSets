namespace IntervalRecords.Experiment.Endpoints;
public interface IBound
{
    static abstract Bound Value { get; }
}

public interface IBounded : IBound { }

public struct Closed : IBounded
{
    public static Bound Value => Bound.Closed;
}

public struct Open : IBounded
{
    public static Bound Value => Bound.Open;
}

public struct Unbounded : IBound
{
    public static Bound Value => Bound.Unbounded;
}

public enum Bound
{
    Open,
    Closed,
    Unbounded
}
