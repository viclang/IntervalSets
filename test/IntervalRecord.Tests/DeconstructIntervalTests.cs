using IntervalRecord.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests
{
    public class DeconstructIntervalTests : BaseIntervalSetTests
    {
        [Theory]
        [MemberData(nameof(IncrementalLengthSet), 3, 2)]
        public void DeconstructAsTuple_ShouldBeExpected(Interval<int> interval)
        {
            // Act
            var (actualStart, actualEnd, actualStartInclusive, actualEndInclusive)  = interval;

            // Assert
            actualStart.Should().Be(interval.Start);
            actualEnd.Should().Be(interval.End);
            actualStartInclusive.Should().Be(interval.StartInclusive);
            actualEndInclusive.Should().Be(interval.EndInclusive);
        }
    }
}
