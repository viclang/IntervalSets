using FluentAssertions;
using IntervalRecord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalCreatorTests
    {
        private const int _start = 1;
        private const int _end = 2;



        public static TheoryData<Interval<int>, Interval<int>> AllBuildersWithExpectedResults = new TheoryData<Interval<int>, Interval<int>>
        {
            { Interval.Empty<int>(), new Interval<int>(0, 0, false, false) },
            { Interval.All<int>(), new Interval<int>(null, null, false, false) },
            { Interval.Singleton(_end), new Interval<int>(_end, _end, true, true) },
            { Interval.Open(_start, _end), new Interval<int>(_start, _end, false, false) },
            { Interval.Closed(_start, _end), new Interval<int>(_start, _end, true, true) },
            { Interval.OpenClosed(_start, _end), new Interval<int>(_start, _end, false, true) },
            { Interval.ClosedOpen(_start, _end), new Interval<int>(_start, _end, true, false) },
            { Interval.GreaterThan(_start), new Interval<int>(_start, null, false, false) },
            { Interval.AtLeast(_start), new Interval<int>(_start, null, true, true) },
            { Interval.LessThan(_end), new Interval<int>(null, _end, false, false) },
            { Interval.AtMost(_end), new Interval<int>(null, _end, false, true) },
            { Interval.FromString<int>($"(,)"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"[,]"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"(,]"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"[,)"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"(-∞,+∞)"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"(∞,∞)"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"(null,null)"), new Interval<int>(null, null, false, false) },
            { Interval.FromString<int>($"({_start},{_end})"), new Interval<int>(_start, _end, false, false) },
            { Interval.FromString<int>($"( {_start}  ,   {_end}    )"), new Interval<int>(_start, _end, false, false) },
            { Interval.FromString<int>($"[{_start},{_end}]"), new Interval<int>(_start, _end, true, true) },
            { Interval.FromString<int>($"({_start},{_end}]"), new Interval<int>(_start, _end, false, true) },
            { Interval.FromString<int>($"[{_start},{_end})"), new Interval<int>(_start, _end, true, false) },
        };

        [Theory]
        [MemberData(nameof(AllBuildersWithExpectedResults))]
        public void BuilderShouldBeEqualToExpectedResult(Interval<int> result, Interval<int> expectedResult)
        {
            result.Should().Be(expectedResult);
        }
    }
}
