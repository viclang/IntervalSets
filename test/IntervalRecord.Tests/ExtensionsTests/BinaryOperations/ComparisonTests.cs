using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.ExtensionsTests
{
    public class ComparisonTests : BaseIntervalPairSetTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, false })]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, false);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, true })]
        public void GetOverlappingStateIncludeHalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, true);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, false })]
        public void IndividualMethods_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState state)
        {
            a.IsBefore(b)
                .Should().Be(state == OverlappingState.Before);
            a.Meets(b)
                .Should().Be(state == OverlappingState.Meets);
            a.Equals(b)
                .Should().Be(state == OverlappingState.Equal);
            a.MetBy(b)
                .Should().Be(state == OverlappingState.MetBy);
            a.IsAfter(b)
                .Should().Be(state == OverlappingState.After);
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.ClosedOpen, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.OpenClosed, offset, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Open, offset, true })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { start, end, BoundaryType.Closed, offset, false })]
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
