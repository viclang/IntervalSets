using IntervalRecord.Tests.TestData;

namespace IntervalRecord.Tests
{
    public class DeconstructIntervalTests : OverlappingStateTestsBase
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
