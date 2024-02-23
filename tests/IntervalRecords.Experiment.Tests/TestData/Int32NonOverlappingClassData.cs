using System.Collections;

namespace IntervalRecords.Experiment.Tests.TestData;
public class Int32NonOverlappingClassData : IEnumerable<object[]>
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
        /// <see cref="BareInterval{int}"/>
        yield return new object[] { "[2, 4]", "[5, 9]", IntervalRelation.Before };
        yield return new object[] { "[10, 14]", "[5, 9]", IntervalRelation.After };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
