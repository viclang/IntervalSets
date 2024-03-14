namespace IntervalRecords.Experiment.Endpoints;
public interface ILeftEndpoint<T> : IEndpoint<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
}

public interface IRightEndpoint<T> : IEndpoint<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
}

public interface IEndpoint<T>
    : IComparable<ILeftEndpoint<T>>,
      IComparable<IRightEndpoint<T>>,
      IComparable<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    T Value { get; init; }

    Bound Bound { get; }

    bool IsClosed { get; }

    bool IsOpen { get; }

    bool IsUnbounded { get; }
}
