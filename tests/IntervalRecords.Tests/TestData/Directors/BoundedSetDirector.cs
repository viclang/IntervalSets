using System;

namespace IntervalRecords.Tests.TestData;
public static class BoundedSetDirector<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static void WithBoundedSet(OverlapBuilderBase<T> builder)
    {
        builder.WithBefore();
        builder.WithMeets();
        builder.WithOverlaps();
        builder.WithStarts();
        builder.WithContainedBy();
        builder.WithFinishes();
        builder.WithEqual();
        builder.WithFinishedBy();
        builder.WithContains();
        builder.WithStartedBy();
        builder.WithOverlappedBy();
        builder.WithMetBy();
        builder.WithAfter();
    }
}
