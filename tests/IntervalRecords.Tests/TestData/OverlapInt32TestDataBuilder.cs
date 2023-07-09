using Unbounded;

namespace IntervalRecords.Tests.TestData;
public sealed class OverlapInt32TestDataBuilder : OverlapTestDataBuilderBase<int, int>
{
    public OverlapInt32TestDataBuilder(Interval<int> reference, int offset) : base(reference, offset)
    {
    }

    protected override OverlapInt32TestDataBuilder WithBefore()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Add(Offset).Substract(Reference.Length()),
                End = Reference.Start.Substract(Offset)
            },
            IntervalOverlapping.Before);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithMeets()
    {
        var overlap = Reference.IntervalType == IntervalType.Closed
                ? IntervalOverlapping.Meets
                : IntervalOverlapping.Before;

        AddBounded(
            Reference with
            {
                Start = Reference.Start.Substract(Reference.Length()),
                End = Reference.Start
            },
            overlap);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithOverlaps()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Substract(Offset),
                End = Reference.Start.Add(Offset)
            },
            IntervalOverlapping.Overlaps);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithStarts()
    {
        AddBounded(
            Reference with
            {
                End = Reference.End.Substract(Offset)
            },
            IntervalOverlapping.Starts);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithUnboundedStarts()
    {
        AddRightBounded(
            Reference with
            {
                Start = Unbounded<int>.NegativeInfinity,
                End = Reference.End.Substract(Offset)
            },
            IntervalOverlapping.Starts);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithContainedBy()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Add(Offset),
                End = Reference.End.Substract(Offset)
            },
            IntervalOverlapping.ContainedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithFinishes()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Add(Offset)
            },
            IntervalOverlapping.Finishes);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithUnboundedFinishes()
    {
        AddLeftBounded(
            Reference with
            {
                Start = Reference.Start.Add(Offset),
                End = Unbounded<int>.PositiveInfinity
            },
            IntervalOverlapping.Finishes);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithEqual()
    {
        AddBounded(Reference, IntervalOverlapping.Equal);
        return this;
    }
    protected override OverlapInt32TestDataBuilder WithLeftBoundedEqual()
    {
        AddLeftBounded(
            Reference with
            {
                End = Unbounded<int>.PositiveInfinity
            },
            IntervalOverlapping.Equal);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithRightBoundedEqual()
    {
        AddRightBounded(
            Reference with
            {
                Start = Unbounded<int>.NegativeInfinity
            },
            IntervalOverlapping.Equal);
        return this;
    }
    protected override OverlapInt32TestDataBuilder WithUnBoundedEqual()
    {
        AddUnBounded(
            Reference with
            {
                Start = Unbounded<int>.NegativeInfinity,
                End = Unbounded<int>.PositiveInfinity
            },
            IntervalOverlapping.Equal);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithFinishedBy()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Substract(Offset)
            },
            IntervalOverlapping.FinishedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithUnBoundedFinishedBy()
    {
        AddLeftBounded(
            Reference with
            {
                Start = Reference.Start.Substract(Offset),
                End = Unbounded<int>.PositiveInfinity
            },
            IntervalOverlapping.FinishedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithContains()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.Start.Substract(Offset),
                End = Reference.End.Add(Offset)
            },
            IntervalOverlapping.Contains);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithStartedBy()
    {
        AddBounded(
            Reference with
            {
                End = Reference.End.Add(Offset)
            },
            IntervalOverlapping.StartedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithUnboundedStartedBy()
    {
        AddRightBounded(
            Reference with
            {
                Start = Unbounded<int>.NegativeInfinity,
                End = Reference.End.Add(Offset)
            },
            IntervalOverlapping.StartedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithOverlappedBy()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.End.Substract(Offset),
                End = Reference.End.Add(Offset)
            },
            IntervalOverlapping.OverlappedBy);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithMetBy()
    {
        var overlap = Reference.IntervalType == IntervalType.Closed
            ? IntervalOverlapping.MetBy
            : IntervalOverlapping.After;

        AddBounded(
            Reference with
            {
                Start = Reference.End,
                End = Reference.End.Add(Reference.Length())
            },
            overlap);
        return this;
    }

    protected override OverlapInt32TestDataBuilder WithAfter()
    {
        AddBounded(
            Reference with
            {
                Start = Reference.End.Add(Offset),
                End = Reference.End.Add(Offset).Add(Reference.Length())
            },
            IntervalOverlapping.After);
        return this;
    }
}
