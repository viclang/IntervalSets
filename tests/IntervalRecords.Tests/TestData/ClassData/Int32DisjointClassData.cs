using IntervalRecords.Extensions;
using IntervalRecords.Tests.TestData.Builders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IntervalRecords.Tests.TestData.ClassData;
public class Int32DisjointClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        /// <see cref="OpenInterval{int}"/>
        yield return new object[] { "(1, 5)", "(5, 9)", IntervalRelation.Before };
        yield return new object[] { "(9, 13)", "(5, 9)", IntervalRelation.After };
        yield return new object[] { "(2, 4)", "(5, 9)", IntervalRelation.Before };
        yield return new object[] { "(10, 14)", "(5, 9)", IntervalRelation.After };
        /// <see cref="ClosedOpenInterval{int}"/>
        yield return new object[] { "[1, 5)", "[5, 9)", IntervalRelation.Before };
        yield return new object[] { "[9, 13)", "[5, 9)", IntervalRelation.After };
        yield return new object[] { "[2, 4)", "[5, 9)", IntervalRelation.Before };
        yield return new object[] { "[10, 14)", "[5, 9)", IntervalRelation.After };
        /// <see cref="OpenClosedInterval{int}"/>
        yield return new object[] { "(1, 5]", "(5, 9]", IntervalRelation.Before };
        yield return new object[] { "(9, 13]", "(5, 9]", IntervalRelation.After };
        yield return new object[] { "(2, 4]", "(5, 9]", IntervalRelation.Before };
        yield return new object[] { "(10, 14]", "(5, 9]", IntervalRelation.After };
        /// <see cref="ClosedInterval{int}"/>
        yield return new object[] { "[2, 4]", "[5, 9]", IntervalRelation.Before };
        yield return new object[] { "[10, 14]", "[5, 9]", IntervalRelation.After };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
