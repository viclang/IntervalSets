using FluentAssertions.Execution;
using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData;
using IntervalRecords.Tests.TestData.ClassData;
using System.Diagnostics;

namespace IntervalRecords.Tests.ExtensionsTests.IntervalCombiner
{
    public class ExceptTests
    {
        [Theory]
        [InlineData("( 1, 2 )")]
        [InlineData("[ 3, 4 )")]
        [InlineData("( 5, 6 ]")]
        [InlineData("[ 7, 8 ]")]
        public void Empty_if_intervals_are_equal(string interval)
        {
            var left = IntervalParser.Parse<int>(interval);
            var right = left with { };

            var actual = left.Except(right);

            actual.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(Int32NonOverlappingClassData))]
        public void List_of_inputs_if_not_overlapping(IntervalRelationTestData<int> testData)
        {
            var actual = testData.Left.Except(testData.Right);

            actual.Should().BeEquivalentTo(new[] { testData.Left, testData.Right });
        }

        [Theory]
        [InlineData("( 1, 4 ]", "( 2, 4 ]", "( 1, 2 ]")]
        [InlineData("( 2, 4 ]", "( 1, 4 ]", "( 1, 2 ]")]
        [InlineData("[ 1, 4 )", "[ 2, 4 )", "[ 1, 2 )")]
        [InlineData("[ 2, 4 )", "[ 1, 4 )", "[ 1, 2 )")]
        public void Start_difference_if_start_not_equal(string leftInterval, string rightInterval, string expectedInterval)
        {
            var left = IntervalParser.Parse<int>(leftInterval);
            var right = IntervalParser.Parse<int>(rightInterval);
            var expected = IntervalParser.Parse<int>(expectedInterval);

            var actual = left.Except(right);

            using (new AssertionScope())
            {
                actual.Count().Should().Be(1);
                actual.Should().AllBeEquivalentTo(expected);
            }
        }

        public void End_difference_if_end_not_equal()
        {

        }

        public void Start_end_difference_if_start_end_not_equal()
        {

        }
    }
}
