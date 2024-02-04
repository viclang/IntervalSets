using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecords.Experiment.Tests;
public class IntervalEmptyTests
{

    [Fact]
    public void Given_When_Then()
    {
        var empty = Interval<int>.Unbounded;
        var result = empty.Start.Point + empty.Start.Point;
    }

}
