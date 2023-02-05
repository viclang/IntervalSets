using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace IntervalRecord.Tests
{
    public class CreatorTests
    {
        private const int start = 1;
        private const int end = 2;
        public static readonly Interval<int> Empty = new(0, 0, false, false);
        public static readonly Interval<int> All = new(null, null, false, false);
        public static readonly Interval<int> Singleton = new(end, end, true, true);
        public static readonly Interval<int> Open = new(start, end, false, false);
        public static readonly Interval<int> Closed = new(start, end, true, true);
        public static readonly Interval<int> OpenClosed = new(start, end, false, true);
        public static readonly Interval<int> ClosedOpen = new(start, end, true, false);
        public static readonly Interval<int> GreaterThan = new(start, null, false, false);
        public static readonly Interval<int> AtLeast = new(start, null, true, false);
        public static readonly Interval<int> LessThan = new(null, end, false, false);
        public static readonly Interval<int> AtMost = new(null, end, false, true);


        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { Interval.Empty<int>(), Empty },
            { Interval.All<int>(), All },
            { Interval.Singleton(end), Singleton },
            { Interval.Closed(start, end), Closed },
            { Interval.ClosedOpen(start, end), ClosedOpen },
            { Interval.OpenClosed(start, end), OpenClosed },
            { Interval.Open(start, end), Open },
            { Interval.GreaterThan(start), GreaterThan },
            { Interval.AtLeast(start), AtLeast },
            { Interval.LessThan(end), LessThan },
            { Interval.AtMost(end), AtMost }
        };

        [Theory]
        [MemberData(nameof(AllBuildersWithExpectedResults))]
        public void BuilderShouldBeEqualToExpectedResult(Interval<int> result, Interval<int> expectedResult)
        {
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void FromStringEmpty_ShouldThrowArgumentNullException()
        {
            // Arrange
            var act = () => Interval.Parse(string.Empty, x => int.Parse(x));

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"Interval not found in string. Please provide an interval string in correct format");
        }
    }
}
