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
            var open = Interval.Open<int>(value, value);
            var openClosed = Interval.OpenClosed(value, value);
            var closedOpen = Interval.ClosedOpen<int>(value, value);
            var closed = Interval.Closed(value, value);

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
            var open = Interval.Open(start, end);
            var openClosed = Interval.OpenClosed(start, end);
            var closedOpen = Interval.ClosedOpen(start, end);
            var closed = Interval.Closed(start, end);

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
            var open = Interval.Open<int>(start, end);
            var openClosed = Interval.OpenClosed(start, end);
            var closedOpen = Interval.ClosedOpen<int>(start, end);
            var closed = Interval.Closed(start, end);

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
