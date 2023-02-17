namespace IntervalRecords.Tests
{
    public class IsEmptyTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(int.MaxValue)]
        [InlineData(443)]
        [InlineData(80)]
        public void EqualValueWithAnOpenBound_ShouldBeEmpty(int value)
        {
            // Arrange
            var open = new Interval<int>(value, value, false, false);
            var openClosed = new Interval<int>(value, value, false, true);
            var closedOpen = new Interval<int>(value, value, true, false);
            var closed = new Interval<int>(value, value, true, true);

            // Act
            var resultTrue = new bool[]
            {
                open.IsEmpty(),
                openClosed.IsEmpty(),
                closedOpen.IsEmpty(),
            };

            var resultFalse = closed.IsEmpty();

            // Assert
            resultTrue.Should().AllBeEquivalentTo(true);
            resultFalse.Should().BeFalse();
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void EndBeforeStart_ShouldBeEmpty(int start, int end)
        {
            // Arrange
            var open = new Interval<int>(start, end, false, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var closed = new Interval<int>(start, end, true, true);

            // Act
            var resultTrue = new bool[]
            {
                open.IsEmpty(),
                openClosed.IsEmpty(),
                closedOpen.IsEmpty(),
                closed.IsEmpty()
            };

            // Assert
            resultTrue.Should().AllBeEquivalentTo(true);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void EndAfterStart_ShouldNotBeEmpty(int start, int end)
        {
            // Arrange
            var open = new Interval<int>(start, end, false, false);
            var openClosed = new Interval<int>(start, end, false, true);
            var closedOpen = new Interval<int>(start, end, true, false);
            var closed = new Interval<int>(start, end, true, true);

            // Act
            var result = new bool[]
            {
                open.IsEmpty(),
                openClosed.IsEmpty(),
                closedOpen.IsEmpty(),
                closed.IsEmpty()
            };

            // Assert
            result.Should().AllBeEquivalentTo(false);
        }
    }
}
