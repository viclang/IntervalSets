using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData;
using IntervalRecords.Tests.TestData.ClassData;
using System.Diagnostics;

namespace IntervalRecords.Tests
{
    public class OverlapsTests
    {
        [Theory]
        [ClassData(typeof(Int32OverlappingClassData))]
        public void GivenTwoOverlappingIntervals_WhenComparing_ReturnsTrue(string left, string right, IntervalRelation _)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);

            var actual = leftInterval.Overlaps(rightInterval);

            actual.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(Int32DisjointClassData))]
        public void GivenTwoDisjointIntervals_WhenComparing_ReturnsFalse(string left, string right, IntervalRelation _)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);

            var actual = leftInterval.Overlaps(rightInterval);

            actual.Should().BeFalse();
        }
    }
}
