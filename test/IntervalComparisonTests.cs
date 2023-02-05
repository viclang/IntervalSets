using FluentAssertions;
using IntervalRecord.Enums;
using IntervalRecord.Extensions;
using IntervalRecord.Tests.DataSets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalComparisonTests
    {
        private const int start = 6;
        private const int end = 10;
        private const int offset = 1;
        private static readonly IntervalDataSet<int> _openDataSet = new IntervalDataSet<int>(start, end, BoundaryType.Open, offset);

        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> OpenGetOverlappingState => _openDataSet.GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> ClosedGetOverlappingState => _openDataSet.CopyWith(BoundaryType.Closed).GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> OpenClosedGetOverlappingState => _openDataSet.CopyWith(BoundaryType.OpenClosed).GetOverlappingState;
        public static TheoryData<Interval<int>, Interval<int>, OverlappingState> ClosedOpenGetOverlappingState => _openDataSet.CopyWith(BoundaryType.ClosedOpen).GetOverlappingState;

        [Theory]
        [MemberData(nameof(OpenGetOverlappingState))]
        [MemberData(nameof(ClosedGetOverlappingState))]
        [MemberData(nameof(OpenClosedGetOverlappingState))]
        [MemberData(nameof(ClosedOpenGetOverlappingState))]
        public void GetOverlappingState_ShouldBeExpected(Interval<int> a, Interval<int> b, OverlappingState expectedResult)
        {
            var result = a.GetOverlappingState(b);
            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(ClosedGetOverlappingState))]
        public void AllNullStates_ShouldBeInDictionary(Interval<int> a, Interval<int> b, OverlappingState origin)
        {
            var results = new HashSet<OverlappingState>
            {
                a.GetOverlappingState(b with { Start = null } ),
                a.GetOverlappingState(b with { End = null } ),
                a.GetOverlappingState(b with { Start = null, End = null } ),
                ( a with { Start = null }).GetOverlappingState(b ),
                ( a with { Start = null }).GetOverlappingState( b with { Start = null } ),
                ( a with { Start = null }).GetOverlappingState( b with { End = null } ),
                ( a with { Start = null }).GetOverlappingState( b with { Start = null, End = null } ),
                ( a with { End = null }).GetOverlappingState( b ),
                ( a with { End = null }).GetOverlappingState( b with { Start = null } ),
                ( a with { End = null }).GetOverlappingState( b with { End = null } ),
                ( a with { End = null }).GetOverlappingState( b with { Start = null, End = null } ),
                ( a with { Start = null, End = null }).GetOverlappingState( b ),
                ( a with { Start = null, End = null }).GetOverlappingState( b with { Start = null } ),
                ( a with { Start = null, End = null }).GetOverlappingState( b with { End = null } )
            };
            var count = results.Should().HaveCountLessThanOrEqualTo(9);
        }

    }
}
