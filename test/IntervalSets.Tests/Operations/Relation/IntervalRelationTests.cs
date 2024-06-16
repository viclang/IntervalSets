﻿using FluentAssertions;
using IntervalSets.Operations;
using IntervalSets.Tests.TestData;
using IntervalSets.Types;

namespace IntervalSets.Tests.Operations
{
    public class IntervalRelationTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GetRelation_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = Interval<int>.Parse(left);
            var rightInterval = Interval<int>.Parse(right);

            var result = leftInterval.GetRelation(rightInterval);

            result.Should().Be(relation);
        }

    }
}