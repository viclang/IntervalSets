using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests
{
    public class IntervalParserTests
    {
        [Fact]
        public void ParseAll_ShouldBeEmpty()
        {
            // Arrange
            var stringToParse = "[3537asd([1,2,3])))), ])[(4,,,5],asdasdlkjl[[6,,,7)][,,][][.]()[](][)]";

            // Act
            var result = Interval.ParseAll<int>(stringToParse);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ParseAll_ShouldHaveCount()
        {
            // Arrange
            var stringToParse = "3537asd([1,2])))), ])[(4,5],asdasdlkjl[[6,7)][,][][.](6,)[,7](8,][9,)";

            // Act
            var result = Interval.ParseAll<int>(stringToParse);

            // Assert
            result.Should().HaveCount(8);
        }
    }
}
