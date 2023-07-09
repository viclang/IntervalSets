using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Tests.TestData;
public abstract class OverlapTestDataBuilderBase<TType, TOffset>
        where TType : struct, IEquatable<TType>, IComparable<TType>, IComparable
{
    protected Interval<TType> Reference { get; init; }
    protected TOffset Offset { get; init; }

    protected List<OverlapTestData<TType>> TestData { get; init; }

    protected OverlapTestDataBuilderBase(Interval<TType> reference, TOffset offset)
    {
        Reference = reference;
        Offset = offset;
        TestData = new List<OverlapTestData<TType>>();
    }

    protected void AddBounded(Interval<TType> first, IntervalOverlapping overlap)
    {
        TestData.Add(new OverlapTestData<TType>(first, Reference, overlap));
    }

    protected void AddLeftBounded(Interval<TType> first, IntervalOverlapping overlap)
    {
        TestData.Add(new OverlapTestData<TType>(first, Reference with
        {
            End = Unbounded.Unbounded<TType>.PositiveInfinity
        },
        overlap));
    }

    protected void AddRightBounded(Interval<TType> first, IntervalOverlapping overlap)
    {
        TestData.Add(new OverlapTestData<TType>(first, Reference with
        {
            Start = Unbounded.Unbounded<TType>.NegativeInfinity
        },
        overlap));
    }

    protected void AddUnBounded(Interval<TType> first, IntervalOverlapping overlap)
    {
        TestData.Add(new OverlapTestData<TType>(first, Reference with
        {
            Start = Unbounded.Unbounded<TType>.NegativeInfinity,
            End = Unbounded.Unbounded<TType>.PositiveInfinity
        },
        overlap));
    }

    public OverlapTestDataBuilderBase<TType, TOffset> WithBoundedSet()
    {
        return WithBefore()
            .WithMeets()
            .WithOverlaps()
            .WithStarts()
            .WithContainedBy()
            .WithFinishes()
            .WithEqual()
            .WithFinishedBy()
            .WithContains()
            .WithStartedBy()
            .WithOverlappedBy()
            .WithMetBy()
            .WithAfter();
    }

    public OverlapTestDataBuilderBase<TType, TOffset> WithUnboundedSet()
    {
        return WithLeftBoundedEqual()
            .WithRightBoundedEqual()
            .WithUnBoundedEqual()
            .WithUnBoundedFinishedBy()
            .WithUnboundedFinishes()
            .WithUnboundedStarts()
            .WithUnboundedStartedBy();
    }

    public IEnumerable<object[]> BuildTheoryData() => TestData.Select(data => new object[] { data });

    public IReadOnlyList<OverlapTestData<TType>> Build() => TestData.AsReadOnly();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithBefore();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithMeets();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithOverlaps();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithStarts();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithUnboundedStarts();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithContainedBy();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithFinishes();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithUnboundedFinishes();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithEqual();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithLeftBoundedEqual();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithRightBoundedEqual();
    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithUnBoundedEqual();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithFinishedBy();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithUnBoundedFinishedBy();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithContains();
    
    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithStartedBy();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithUnboundedStartedBy();

    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithOverlappedBy();
    
    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithMetBy();
    
    protected abstract OverlapTestDataBuilderBase<TType, TOffset> WithAfter();
}
