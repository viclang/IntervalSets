using IntervalRecords.Types;
using System.Collections.Generic;
using System.Linq;
using Unbounded;

namespace IntervalRecords.Tests.TestData;
public sealed class Int32OverlapTestDataBuilder : OverlapBuilderBase<int>
{
    private readonly Interval<int> _reference;
    private readonly int _offset;

    private readonly List<OverlapTestData<int>> _testData;

    public Int32OverlapTestDataBuilder(Interval<int> reference, int offset)
    {
        _reference = reference;
        _offset = offset;
        _testData = new List<OverlapTestData<int>>();
    }

    private void AddBounded(Interval<int> first, IntervalOverlapping overlap)
    {
        _testData.Add(new OverlapTestData<int>(first, _reference, overlap));
    }

    private void AddLeftBounded(Interval<int> first, IntervalOverlapping overlap)
    {
        _testData.Add(new OverlapTestData<int>(
            first,
            OverlapFactory.LeftBoundedEqual(_reference),
            overlap));
    }

    private void AddRightBounded(Interval<int> first, IntervalOverlapping overlap)
    {
        _testData.Add(new OverlapTestData<int>(
            first,
            OverlapFactory.RightBoundedEqual(_reference),
            overlap));
    }

    private void AddUnBounded(Interval<int> first, IntervalOverlapping overlap)
    {
        _testData.Add(new OverlapTestData<int>(
            first,
            OverlapFactory.UnBoundedEqual(_reference),
            overlap));
    }

    public override Int32OverlapTestDataBuilder WithBefore()
    {
        AddBounded(
            OverlapFactory.Before(_reference, _offset),
            IntervalOverlapping.Before);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithMeets()
    {
        var overlap = _reference.IntervalType == IntervalType.Closed
                ? IntervalOverlapping.Meets
                : IntervalOverlapping.Before;

        AddBounded(
            OverlapFactory.Meets(_reference),
            overlap);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithOverlaps()
    {
        AddBounded(
            OverlapFactory.Overlaps(_reference, _offset),
            IntervalOverlapping.Overlaps);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithStarts()
    {
        AddBounded(
            OverlapFactory.Starts(_reference, _offset),
            IntervalOverlapping.Starts);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithRightboundedStarts()
    {
        AddRightBounded(
            OverlapFactory.RightboundedStarts(_reference, _offset),
            IntervalOverlapping.Starts);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithContainedBy()
    {
        AddBounded(
            OverlapFactory.ContainedBy(_reference, _offset),
            IntervalOverlapping.ContainedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithFinishes()
    {
        AddBounded(
            OverlapFactory.Finishes(_reference, _offset),
            IntervalOverlapping.Finishes);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithLeftboundedFinishes()
    {
        AddLeftBounded(
            OverlapFactory.LeftboundedFinishes(_reference, _offset),
            IntervalOverlapping.Finishes);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithEqual()
    {
        AddBounded(_reference, IntervalOverlapping.Equal);
        return this;
    }
    public override Int32OverlapTestDataBuilder WithLeftBoundedEqual()
    {
        AddLeftBounded(
            OverlapFactory.LeftBoundedEqual(_reference),
            IntervalOverlapping.Equal);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithRightBoundedEqual()
    {
        AddRightBounded(
            OverlapFactory.RightBoundedEqual(_reference),
            IntervalOverlapping.Equal);
        return this;
    }
    public override Int32OverlapTestDataBuilder WithUnBoundedEqual()
    {
        AddUnBounded(
            OverlapFactory.UnBoundedEqual(_reference),
            IntervalOverlapping.Equal);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithFinishedBy()
    {
        AddBounded(
            OverlapFactory.FinishedBy(_reference, _offset),
            IntervalOverlapping.FinishedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithLeftBoundedFinishedBy()
    {
        AddLeftBounded(
            OverlapFactory.LeftBoundedFinishedBy(_reference, _offset),
            IntervalOverlapping.FinishedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithContains()
    {
        AddBounded(
            OverlapFactory.Contains(_reference, _offset),
            IntervalOverlapping.Contains);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithStartedBy()
    {
        AddBounded(
            OverlapFactory.StartedBy(_reference, _offset),
            IntervalOverlapping.StartedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithRightboundedStartedBy()
    {
        AddRightBounded(
            OverlapFactory.RightboundedStartedBy(_reference, _offset),
            IntervalOverlapping.StartedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithOverlappedBy()
    {
        AddBounded(
            OverlapFactory.OverlappedBy(_reference, _offset),
            IntervalOverlapping.OverlappedBy);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithMetBy()
    {
        var overlap = _reference.IntervalType == IntervalType.Closed
            ? IntervalOverlapping.MetBy
            : IntervalOverlapping.After;

        AddBounded(
            OverlapFactory.MetBy(_reference),
            overlap);
        return this;
    }

    public override Int32OverlapTestDataBuilder WithAfter()
    {
        AddBounded(
            OverlapFactory.After(_reference, _offset),
            IntervalOverlapping.After);
        return this;
    }

    public static implicit operator List<OverlapTestData<int>>(Int32OverlapTestDataBuilder builder)
        => builder._testData;

    public static implicit operator List<object[]>(Int32OverlapTestDataBuilder builder)
        => builder._testData.Select(data => new object[] { data }).ToList();
}
