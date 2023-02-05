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
        public static readonly Interval<int> Empty = new Interval<int>(0, 0, false, false);
        public static readonly Interval<int> All = new Interval<int>(null, null, false, false);
        public static readonly Interval<int> Singleton = new Interval<int>(_end, _end, true, true);
        public static readonly Interval<int> Open = new Interval<int>(_start, _end, false, false);
        public static readonly Interval<int> Closed = new Interval<int>(_start, _end, true, true);
        public static readonly Interval<int> OpenClosed = new Interval<int>(_start, _end, false, true);
        public static readonly Interval<int> ClosedOpen = new Interval<int>(_start, _end, true, false);
        public static readonly Interval<int> GreaterThan = new Interval<int>(_start, null, false, false);
        public static readonly Interval<int> AtLeast = new Interval<int>(_start, null, true, false);
        public static readonly Interval<int> LessThan = new Interval<int>(null, _end, false, false);
        public static readonly Interval<int> AtMost = new Interval<int>(null, _end, false, true);


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
            { Interval.AtMost(_end), AtMost },
            { Interval.Parse($"(,)", x => int.Parse(x)), All },
            { Interval.Parse($"[,]", x => int.Parse(x)), All },
            { Interval.Parse($"(,]", x => int.Parse(x)), All },
            { Interval.Parse($"[,)", x => int.Parse(x)), All },
            { Interval.Parse($"(-∞,+∞)", x => int.Parse(x), "∞"), All },
            { Interval.Parse($"(∞,∞)", x => int.Parse(x), "∞"), All },
            { Interval.Parse($"(null,null)", x => int.Parse(x)), All },
            { Interval.Parse($"({_start},{_end})", x => int.Parse(x)), Open },
            { Interval.Parse($"(       {_start}       ,      {_end}          )", x => int.Parse(x)), Open },
            { Interval.Parse($"[{_start},{_end}]", x => int.Parse(x)), Closed },
            { Interval.Parse($"({_start},{_end}]", x => int.Parse(x)), OpenClosed },
            { Interval.Parse($"[{_start},{_end})", x => int.Parse(x)), ClosedOpen },
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

        [Theory]
        [InlineData("()")]
        [InlineData("[]")]
        [InlineData("(]")]
        [InlineData("[)")]
        [InlineData(",")]
        [InlineData(",,")]
        [InlineData("1,2")]
        [InlineData("],[")]
        [InlineData("],]")]
        [InlineData("[,[")]
        [InlineData("),(")]
        [InlineData("),)")]
        [InlineData("(,(")]
        [InlineData("{,}")]
        [InlineData("<,>")]
        public void FromStringIncorrectFormat_ShouldThrowArgumentException(string interval)
        {
            // Arrange
            var act = () => Interval.Parse(interval, x => int.Parse(x));

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Interval not found in string. Please provide an interval string in correct format");
        }
    }
}
