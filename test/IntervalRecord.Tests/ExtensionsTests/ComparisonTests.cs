using IntervalRecord.Tests.DataSets;
using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class ComparisonTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static readonly IOverlappingDataSet<int> _dataSet = new IntOverlappingDataSet(start, end, BoundaryType.Closed, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> GetOverlappingState(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(false);
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> GetOverlappingState_IncludeHalfOpen(BoundaryType boundaryType)
            => _dataSet.CopyWith(boundaryType).GetOverlappingState(true);

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Open)]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, false);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.Open)]
        public void GetOverlappingStateIncludeHalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, true);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Open)]
        public void IndividualMethods_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            a.IsBefore(b)
                .Should().Be(state == OverlappingState.Before);
            a.Meets(b)
                .Should().Be(state == OverlappingState.Meets);
            a.Starts(b)
                .Should().Be(state == OverlappingState.Starts);
            a.OverlapsState(b)
                .Should().Be(state == OverlappingState.Overlaps);
            a.ContainedBy(b)
                .Should().Be(state == OverlappingState.ContainedBy);
            a.Finishes(b)
                .Should().Be(state == OverlappingState.Finishes);
            a.Equals(b)
                .Should().Be(state == OverlappingState.Equal);
            a.FinishedBy(b)
                .Should().Be(state == OverlappingState.FinishedBy);
            a.Contains(b)
                .Should().Be(state == OverlappingState.Contains);
            a.OverlappedByState(b)
                .Should().Be(state == OverlappingState.OverlappedBy);
            a.StartedBy(b)
                .Should().Be(state == OverlappingState.StartedBy);
            a.MetBy(b)
                .Should().Be(state == OverlappingState.MetBy);
            a.IsAfter(b)
                .Should().Be(state == OverlappingState.After);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.Closed)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.ClosedOpen)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.OpenClosed)]
        [MemberData(nameof(GetOverlappingState_IncludeHalfOpen), BoundaryType.Open)]
        public void IndividualMethods_IncludeHalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            a.IsBefore(b, true)
                .Should().Be(state == OverlappingState.Before);
            a.Meets(b, true)
                .Should().Be(state == OverlappingState.Meets);
            a.MetBy(b, true)
                .Should().Be(state == OverlappingState.MetBy);
            a.IsAfter(b, true)
                .Should().Be(state == OverlappingState.After);
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        public void CompareTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            // Act
            var actual = a.CompareTo(b);

            // Assert
            if ((int)state < 6)
            {
                actual.Should().Be(-1);
            }
            else if ((int)state == 6)
            {
                actual.Should().Be(0);
            }
            else if ((int)state > 6)
            {
                actual.Should().Be(1);
            }
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        public void GreaterThan_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            // Act
            var actual = a > b;

            // Assert
            if ((int)state <= 6)
            {
                actual.Should().BeFalse();
            }
            else if ((int)state > 6)
            {
                actual.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        public void LessThan_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            // Act
            var actual = a < b;

            // Assert
            if ((int)state < 6)
            {
                actual.Should().BeTrue();
            }
            else if ((int)state >= 6)
            {
                actual.Should().BeFalse();
            }
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        public void GreaterOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            // Act
            var actual = a >= b;

            // Assert
            if ((int)state < 6)
            {
                actual.Should().BeFalse();
            }
            else if ((int)state >= 6)
            {
                actual.Should().BeTrue();
            }
        }

        [Theory]
        [MemberData(nameof(GetOverlappingState), BoundaryType.Closed)]
        public void LessOrEqualTo_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            // Act
            var actual = a <= b;

            // Assert
            if ((int)state <= 6)
            {
                actual.Should().BeTrue();
            }
            else if ((int)state > 6)
            {
                actual.Should().BeFalse();
            }
        }
    }
}
