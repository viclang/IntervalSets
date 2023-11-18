using IntervalRecords.Tests.TestData;
using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.ClassData;
using System.Diagnostics;

namespace IntervalRecords.Tests
{
    public class IntervalRelationTests
    {
        [Theory]
        [ClassData(typeof(Int32IntervalRelationClassData))]
        public void GetOverlappingState_ShouldBeExpected(string left, string right, IntervalRelation relation)
        {
            var leftInterval = IntervalParser.Parse<int>(left);
            var rightInterval = IntervalParser.Parse<int>(right);

            var result = leftInterval.GetRelation(rightInterval);

            result.Should().Be(relation);
        }

    }
}
