using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace IntervalRecord.Tests
{
    public class CreatorTests
    {
        private const int _start = 1;
        private const int _end = 2;
        public static readonly Interval<int> Empty = new(0, 0, false, false);
        public static readonly Interval<int> All = new(null, null, false, false);
        public static readonly Interval<int> Singleton = new(_end, _end, true, true);
        public static readonly Interval<int> Open = new(_start, _end, false, false);
        public static readonly Interval<int> Closed = new(_start, _end, true, true);
        public static readonly Interval<int> OpenClosed = new(_start, _end, false, true);
        public static readonly Interval<int> ClosedOpen = new(_start, _end, true, false);
        public static readonly Interval<int> GreaterThan = new(_start, null, false, false);
        public static readonly Interval<int> AtLeast = new(_start, null, true, false);
        public static readonly Interval<int> LessThan = new(null, _end, false, false);
        public static readonly Interval<int> AtMost = new(null, _end, false, true);


        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { Interval.Empty<int>(), Empty },
            { Interval.All<int>(), All },
            { Interval.Singleton(_end), Singleton },
            { Interval.Closed(_start, _end), Closed },
            { Interval.ClosedOpen(_start, _end), ClosedOpen },
            { Interval.OpenClosed(_start, _end), OpenClosed },
            { Interval.Open(_start, _end), Open },
            { Interval.GreaterThan(_start), GreaterThan },
            { Interval.AtLeast(_start), AtLeast },
            { Interval.LessThan(_end), LessThan },
            { Interval.AtMost(_end), AtMost }
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
