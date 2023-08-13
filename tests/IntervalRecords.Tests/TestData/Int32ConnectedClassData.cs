using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Tests.TestData;
public class Int32ConnectedClassData : IEnumerable<object[]>
{
    private const int _offset = 1;
    private readonly static Interval<int> _reference = new ClosedInterval<int>(5, 9);

    public IEnumerator<object[]> GetEnumerator()
    {
        var testData = new List<object[]>();
        foreach (var intervalType in (IntervalType[])Enum.GetValues(typeof(IntervalType)))
        {
            var builder = new Int32OverlapTestDataBuilder(_reference.Canonicalize(intervalType, 0), _offset);
            if (intervalType != IntervalType.Open)
            {
                builder.WithMeets()
                    .WithMetBy();
            }
            List<object[]> overlap = builder.WithOverlaps()
                .WithStarts()
                .WithContainedBy()
                .WithFinishes()
                .WithEqual()
                .WithFinishedBy()
                .WithContains()
                .WithStartedBy()
                .WithOverlappedBy();

            testData.AddRange(overlap);
            
        }
        return testData.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
