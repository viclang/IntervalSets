using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Tests;
public class IntervalParseTests
{

    [Fact]
    public void Given_When_Then()
    {
        // Arrange
        var intervalString = "[1, 2]";

        // Act
        var actual = Interval<int>.Parse(intervalString);

        // Assert
        actual.ToString().Should()
            .Be(intervalString);
    }

}
