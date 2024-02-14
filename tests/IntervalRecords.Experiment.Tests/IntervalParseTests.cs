﻿using IntervalRecords.Experiment.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Tests;
public class IntervalParseTests
{

    [Fact]
    public void Parsed_interval_equals_original_string()
    {
        var intervalToParse = "[1, 2]";

        var parsedInterval = Interval<int>.Parse(intervalToParse);

        parsedInterval.ToString()
            .Should()
            .Be(intervalToParse);
    }
}
