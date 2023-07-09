using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntervalRecords.Tests.TestData;

public class OverlapInt32ClassData : IEnumerable<object[]>
{
    private const int _offset = 1;
    private readonly static Interval<int> _reference = new ClosedInterval<int>(5, 9);

    public IEnumerator<object[]> GetEnumerator()
    {
        var testData = new List<OverlapTestData<int>>();
        foreach (var intervalType in (IntervalType[])Enum.GetValues(typeof(IntervalType)))
        {
            testData.AddRange(
                new OverlapInt32TestDataBuilder(_reference.Canonicalize(intervalType, 0), _offset)
                .WithBoundedSet()
                .WithUnboundedSet()
                .Build());
        }
        return testData.Select(data => new object[] { data })
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
