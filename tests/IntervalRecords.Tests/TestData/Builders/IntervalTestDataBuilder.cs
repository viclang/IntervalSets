using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecords.Tests.TestData;
public sealed class IntervalTestDataBuilder<T, TOffset> : IIntervalTestDataBuilder
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
    where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, IComparable
{
    private readonly Interval<T> _reference;
    private readonly TOffset _offset;

    private readonly List<IntervalRelationTestData<T>> _testData;

    public IntervalTestDataBuilder(Interval<T> reference, TOffset offset)
    {
        _reference = reference;
        _offset = offset;
        _testData = new List<IntervalRelationTestData<T>>();
    }

    private void AddBounded(Interval<T> first, IntervalRelation overlap)
    {
        _testData.Add(new IntervalRelationTestData<T>(first, _reference, overlap));
    }

    private void AddLeftBounded(Interval<T> first, IntervalRelation overlap)
    {
        _testData.Add(new IntervalRelationTestData<T>(
            first,
            IntervalRelationFactory<T, TOffset>.LeftBoundedEqual(_reference),
            overlap));
    }

    private void AddRightBounded(Interval<T> first, IntervalRelation overlap)
    {
        _testData.Add(new IntervalRelationTestData<T>(
            first,
            IntervalRelationFactory<T, TOffset>.RightBoundedEqual(_reference),
            overlap));
    }

    private void AddUnBounded(Interval<T> first, IntervalRelation overlap)
    {
        _testData.Add(new IntervalRelationTestData<T>(
            first,
            IntervalRelationFactory<T, TOffset>.UnBoundedEqual(_reference),
            overlap));
    }

    public IIntervalTestDataBuilder WithBefore()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.Before(_reference, _offset),
            IntervalRelation.Before);
        return this;
    }

    public IIntervalTestDataBuilder WithMeets()
    {
        var overlap = _reference.IntervalType == IntervalType.Closed
                ? IntervalRelation.Meets
                : IntervalRelation.Before;

        AddBounded(
            IntervalRelationFactory<T, TOffset>.Meets(_reference),
            overlap);
        return this;
    }

    public IIntervalTestDataBuilder WithOverlaps()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.Overlaps(_reference, _offset),
            IntervalRelation.Overlaps);
        return this;
    }

    public IIntervalTestDataBuilder WithStarts()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.Starts(_reference, _offset),
            IntervalRelation.Starts);
        return this;
    }

    public IIntervalTestDataBuilder WithRightboundedStarts()
    {
        AddRightBounded(
            IntervalRelationFactory<T, TOffset>.RightboundedStarts(_reference, _offset),
            IntervalRelation.Starts);
        return this;
    }

    public IIntervalTestDataBuilder WithContainedBy()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.ContainedBy(_reference, _offset),
            IntervalRelation.ContainedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithFinishes()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.Finishes(_reference, _offset),
            IntervalRelation.Finishes);
        return this;
    }

    public IIntervalTestDataBuilder WithLeftboundedFinishes()
    {
        AddLeftBounded(
            IntervalRelationFactory<T, TOffset>.LeftboundedFinishes(_reference, _offset),
            IntervalRelation.Finishes);
        return this;
    }

    public IIntervalTestDataBuilder WithEqual()
    {
        AddBounded(_reference, IntervalRelation.Equal);
        return this;
    }
    public IIntervalTestDataBuilder WithLeftBoundedEqual()
    {
        AddLeftBounded(
            IntervalRelationFactory<T, TOffset>.LeftBoundedEqual(_reference),
            IntervalRelation.Equal);
        return this;
    }

    public IIntervalTestDataBuilder WithRightBoundedEqual()
    {
        AddRightBounded(
            IntervalRelationFactory<T, TOffset>.RightBoundedEqual(_reference),
            IntervalRelation.Equal);
        return this;
    }
    public IIntervalTestDataBuilder WithUnBoundedEqual()
    {
        AddUnBounded(
            IntervalRelationFactory<T, TOffset>.UnBoundedEqual(_reference),
            IntervalRelation.Equal);
        return this;
    }

    public IIntervalTestDataBuilder WithFinishedBy()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.FinishedBy(_reference, _offset),
            IntervalRelation.FinishedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithLeftBoundedFinishedBy()
    {
        AddLeftBounded(
            IntervalRelationFactory<T, TOffset>.LeftBoundedFinishedBy(_reference, _offset),
            IntervalRelation.FinishedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithContains()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.Contains(_reference, _offset),
            IntervalRelation.Contains);
        return this;
    }

    public IIntervalTestDataBuilder WithStartedBy()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.StartedBy(_reference, _offset),
            IntervalRelation.StartedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithRightboundedStartedBy()
    {
        AddRightBounded(
            IntervalRelationFactory<T, TOffset>.RightboundedStartedBy(_reference, _offset),
            IntervalRelation.StartedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithOverlappedBy()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.OverlappedBy(_reference, _offset),
            IntervalRelation.OverlappedBy);
        return this;
    }

    public IIntervalTestDataBuilder WithMetBy()
    {
        var overlap = _reference.IntervalType == IntervalType.Closed
            ? IntervalRelation.MetBy
            : IntervalRelation.After;

        AddBounded(
            IntervalRelationFactory<T, TOffset>.MetBy(_reference),
            overlap);
        return this;
    }

    public IIntervalTestDataBuilder WithAfter()
    {
        AddBounded(
            IntervalRelationFactory<T, TOffset>.After(_reference, _offset),
            IntervalRelation.After);
        return this;
    }

    public static implicit operator List<IntervalRelationTestData<T>>(IntervalTestDataBuilder<T, TOffset> builder)
        => builder._testData;

    public static implicit operator List<object[]>(IntervalTestDataBuilder<T, TOffset> builder)
        => builder._testData.Select(data => new object[] { data }).ToList();
}
