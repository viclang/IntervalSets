using FluentAssertions;
using IntervalSets.Types;

namespace IntervalSets.Tests.Types;
public class IntervalTypeTests
{
    [Theory]
    [InlineData(Bound.Open, Bound.Open, IntervalType.Open)]
    [InlineData(Bound.Open, Bound.Closed, IntervalType.OpenClosed)]
    [InlineData(Bound.Open, Bound.Unbounded, IntervalType.OpenUnbounded)]
    [InlineData(Bound.Closed, Bound.Open, IntervalType.ClosedOpen)]
    [InlineData(Bound.Closed, Bound.Closed, IntervalType.Closed)]
    [InlineData(Bound.Closed, Bound.Unbounded, IntervalType.ClosedUnbounded)]
    [InlineData(Bound.Unbounded, Bound.Open, IntervalType.UnboundedOpen)]
    [InlineData(Bound.Unbounded, Bound.Closed, IntervalType.UnboundedClosed)]
    [InlineData(Bound.Unbounded, Bound.Unbounded, IntervalType.Unbounded)]
    public void Encode_IntervalType(Bound leftBound, Bound rightBound, IntervalType expectedIntervalType)
    {
        var intervalType = IntervalTypeFactory.Create(leftBound, rightBound);

        intervalType.Should().Be(expectedIntervalType);
    }


    [Theory]
    [InlineData(IntervalType.Open, Bound.Open)]
    [InlineData(IntervalType.OpenClosed, Bound.Open)]
    [InlineData(IntervalType.OpenUnbounded, Bound.Open)]
    [InlineData(IntervalType.ClosedOpen, Bound.Closed)]
    [InlineData(IntervalType.Closed, Bound.Closed)]
    [InlineData(IntervalType.ClosedUnbounded, Bound.Closed)]
    [InlineData(IntervalType.UnboundedOpen, Bound.Unbounded)]
    [InlineData(IntervalType.UnboundedClosed, Bound.Unbounded)]
    [InlineData(IntervalType.Unbounded, Bound.Unbounded)]
    public void Decode_StartBound(IntervalType intervalType, Bound expectedStartBound)
    {
        var startBound = intervalType.StartBound();

        startBound.Should().Be(expectedStartBound);
    }

    [Theory]
    [InlineData(IntervalType.Open, Bound.Open)]
    [InlineData(IntervalType.OpenClosed, Bound.Closed)]
    [InlineData(IntervalType.OpenUnbounded, Bound.Unbounded)]
    [InlineData(IntervalType.ClosedOpen, Bound.Open)]
    [InlineData(IntervalType.Closed, Bound.Closed)]
    [InlineData(IntervalType.ClosedUnbounded, Bound.Unbounded)]
    [InlineData(IntervalType.UnboundedOpen, Bound.Open)]
    [InlineData(IntervalType.UnboundedClosed, Bound.Closed)]
    [InlineData(IntervalType.Unbounded, Bound.Unbounded)]
    public void Decode_EndBound(IntervalType intervalType, Bound expectedStartBound)
    {
        var startBound = intervalType.EndBound();

        startBound.Should().Be(expectedStartBound);
    }

}
