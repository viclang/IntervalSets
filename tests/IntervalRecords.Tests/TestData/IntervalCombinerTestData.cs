using System;
using System.Collections.Generic;

namespace IntervalRecords.Tests.TestData;
public record IntervalCombinerTestData<T>(Interval<T> Left, Interval<T> Right, Interval<T>[] ExpectedResult)
    where T : struct, IEquatable<T>, IComparable<T>, IComparable;
