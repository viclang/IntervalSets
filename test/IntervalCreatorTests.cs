using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalCreatorTests
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
            { Interval.Open(_start, _end), Open },
            { Interval.Closed(_start, _end), Closed },
            { Interval.OpenClosed(_start, _end), OpenClosed },
            { Interval.ClosedOpen(_start, _end), ClosedOpen },
            { Interval.GreaterThan(_start), GreaterThan },
            { Interval.AtLeast(_start), AtLeast },
            { Interval.LessThan(_end), LessThan },
            { Interval.AtMost(_end), AtMost },
            { Interval.Parse<int>($"(,)"), All },
            { Interval.Parse<int>($"[,]"), All },
            { Interval.Parse<int>($"(,]"), All },
            { Interval.Parse<int>($"[,)"), All },
            { Interval.Parse<int>($"(-∞,+∞)"), All },
            { Interval.Parse<int>($"(∞,∞)"), All },
            { Interval.Parse<int>($"(null,null)"), All },
            { Interval.Parse<int>($"({_start},{_end})"), Open },
            { Interval.Parse<int>($"(       {_start}       ,      {_end}          )"), Open },
            { Interval.Parse<int>($"[{_start},{_end}]"), Closed },
            { Interval.Parse<int>($"({_start},{_end}]"), OpenClosed },
            { Interval.Parse<int>($"[{_start},{_end})"), ClosedOpen },
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
            var act = () => Interval.Parse<int>(string.Empty);

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
            var act = () => Interval.Parse<int>(interval);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage($"Interval not found in string {interval}. Please provide an interval string in correct format");
        }

    }
}
