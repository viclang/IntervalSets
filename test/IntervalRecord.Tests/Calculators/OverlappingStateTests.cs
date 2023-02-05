using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests.Calculators
{
    public class ComparableTests : OverlappingStateTestsBase
    {
        private const int startingPoint = 0;
        private const int length = 4;
        private const int offset = 1;

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, false })]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, false);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, true })]
        public void GetOverlappingStateIncludeHalfOpen_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b, true);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, false })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.ClosedOpen, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.OpenClosed, true })]
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Open, true })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
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
        [MemberData(nameof(IntervalPairsWithOverlappingState), new object[] { startingPoint, length, offset, BoundaryType.Closed, false })]
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
