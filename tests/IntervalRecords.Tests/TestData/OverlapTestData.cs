using System;
using Unbounded;

namespace IntervalRecords.Tests.TestData;
public record OverlapTestData<T>(Interval<T> First, Interval<T> Second, IntervalOverlapping Overlap)
    where T : struct, IEquatable<T>, IComparable<T>, IComparable;
