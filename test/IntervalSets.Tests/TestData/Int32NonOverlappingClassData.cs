using IntervalSets.Operations;
using IntervalSets.Types;
using System.Collections;

namespace IntervalSets.Tests.TestData;
public class Int32NonOverlappingClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        /// <see cref="Interval{int, Open, Open}"/>
        yield return new object[] { "(0, 0)", "(0, 0)", IntervalRelation.BothEmpty };
        yield return new object[] { "(0, 0)", "(5, 9)", IntervalRelation.FirstEmpty };
        yield return new object[] { "(2, 4)", "(0, 0)", IntervalRelation.SecondEmpty };
        yield return new object[] { "(1, 5)", "(5, 9)", IntervalRelation.Before };
        yield return new object[] { "(9, 13)", "(5, 9)", IntervalRelation.After };
        yield return new object[] { "(2, 4)", "(5, 9)", IntervalRelation.Before };
        yield return new object[] { "(10, 14)", "(5, 9)", IntervalRelation.After };

        /// <see cref="Interval{int, Closed, Open}"/>
        yield return new object[] { "[0, 0)", "[0, 0)", IntervalRelation.BothEmpty };
        yield return new object[] { "[0, 0)", "[5, 9)", IntervalRelation.FirstEmpty };
        yield return new object[] { "[2, 4)", "[0, 0)", IntervalRelation.SecondEmpty };
        yield return new object[] { "[1, 5)", "[5, 9)", IntervalRelation.Before };
        yield return new object[] { "[9, 13)", "[5, 9)", IntervalRelation.After };
        yield return new object[] { "[2, 4)", "[5, 9)", IntervalRelation.Before };
        yield return new object[] { "[10, 14)", "[5, 9)", IntervalRelation.After };
        /// <see cref="Interval{int, Open, Closed}"/>
        yield return new object[] { "(0, 0]", "(0, 0]", IntervalRelation.BothEmpty };
        yield return new object[] { "(0, 0]", "(5, 9]", IntervalRelation.FirstEmpty };
        yield return new object[] { "(2, 4]", "(0, 0]", IntervalRelation.SecondEmpty };
        yield return new object[] { "(1, 5]", "(5, 9]", IntervalRelation.Before };
        yield return new object[] { "(9, 13]", "(5, 9]", IntervalRelation.After };
        yield return new object[] { "(2, 4]", "(5, 9]", IntervalRelation.Before };
        yield return new object[] { "(10, 14]", "(5, 9]", IntervalRelation.After };

        /// <see cref="Interval{int, Closed, Closed}"/>
        yield return new object[] { "[2, 4]", "[5, 9]", IntervalRelation.Before };
        yield return new object[] { "[10, 14]", "[5, 9]", IntervalRelation.After };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
