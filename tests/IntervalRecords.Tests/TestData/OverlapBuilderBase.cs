using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecords.Tests.TestData;
public abstract class OverlapBuilderBase<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public abstract OverlapBuilderBase<T> WithBefore();

    public abstract OverlapBuilderBase<T> WithMeets();

    public abstract OverlapBuilderBase<T> WithOverlaps();

    public abstract OverlapBuilderBase<T> WithStarts();

    public abstract OverlapBuilderBase<T> WithRightboundedStarts();

    public abstract OverlapBuilderBase<T> WithContainedBy();

    public abstract OverlapBuilderBase<T> WithFinishes();

    public abstract OverlapBuilderBase<T> WithLeftboundedFinishes();

    public abstract OverlapBuilderBase<T> WithEqual();

    public abstract OverlapBuilderBase<T> WithLeftBoundedEqual();

    public abstract OverlapBuilderBase<T> WithRightBoundedEqual();

    public abstract OverlapBuilderBase<T> WithUnBoundedEqual();

    public abstract OverlapBuilderBase<T> WithFinishedBy();

    public abstract OverlapBuilderBase<T> WithLeftBoundedFinishedBy();

    public abstract OverlapBuilderBase<T> WithContains();

    public abstract OverlapBuilderBase<T> WithStartedBy();

    public abstract OverlapBuilderBase<T> WithRightboundedStartedBy();

    public abstract OverlapBuilderBase<T> WithOverlappedBy();

    public abstract OverlapBuilderBase<T> WithMetBy();

    public abstract OverlapBuilderBase<T> WithAfter();
}
