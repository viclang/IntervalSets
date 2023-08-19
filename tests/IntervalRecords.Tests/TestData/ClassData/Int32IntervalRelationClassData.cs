using IntervalRecords.Tests.TestData.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using IntervalRecords.Extensions;

namespace IntervalRecords.Tests.TestData.ClassData;
public class Int32IntervalRelationClassData : IEnumerable<object[]>
{
    private const int _offset = 1;
    private readonly static Interval<int> _reference = new ClosedInterval<int>(5, 9);

    public IEnumerator<object[]> GetEnumerator()
    {
        var testData = new List<object[]>();
        foreach (var intervalType in (IntervalType[])Enum.GetValues(typeof(IntervalType)))
        {
            var builder = new IntervalRelationTestDataBuilder<int, int>(_reference.Canonicalize(intervalType, 0), _offset);

            IntervalTestDataDirector.WithUnBoundedIntervals(builder);
            IntervalTestDataDirector.WithBoundedIntervals(builder);

            testData.AddRange((List<object[]>)builder);
        }
        return testData.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
