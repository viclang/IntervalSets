using IntervalSet.Operations;
using System.Collections;

namespace IntervalSet.Tests.TestData;

/// <summary>
/// Two overlapping intervals for
/// <see cref="OpenInterval{int}"/>,
/// <see cref="ClosedOpenInterval{int}"/>,
/// <see cref="OpenClosedInterval{T}"/> and
/// <see cref="BareInterval{T}"/>
/// with the expected <see cref="IntervalRelation"/>.
/// </summary>
public class Int32OverlappingClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        /// <see cref="OpenInterval{int}"/>
        yield return new object[] { "(4, 6)", "(5, 9)", IntervalRelation.Overlaps };
        yield return new object[] { "(5, 8)", "(5, 9)", IntervalRelation.Starts };
        yield return new object[] { "(6, 8)", "(5, 9)", IntervalRelation.ContainedBy };
        yield return new object[] { "(6, 9)", "(5, 9)", IntervalRelation.Finishes };
        yield return new object[] { "(5, 9)", "(5, 9)", IntervalRelation.Equals };
        yield return new object[] { "(4, 9)", "(5, 9)", IntervalRelation.FinishedBy };
        yield return new object[] { "(4, 10)", "(5, 9)", IntervalRelation.Contains };
        yield return new object[] { "(5, 10)", "(5, 9)", IntervalRelation.StartedBy };
        yield return new object[] { "(8, 10)", "(5, 9)", IntervalRelation.OverlappedBy };
        yield return new object[] { "(6, Infinity)", "(5, Infinity)", IntervalRelation.Finishes };
        yield return new object[] { "(5, Infinity)", "(5, Infinity)", IntervalRelation.Equals };
        yield return new object[] { "(4, Infinity)", "(5, Infinity)", IntervalRelation.FinishedBy };
        yield return new object[] { "(-Infinity, 8)", "(-Infinity, 9)", IntervalRelation.Starts };
        yield return new object[] { "(-Infinity, 9)", "(-Infinity, 9)", IntervalRelation.Equals };
        yield return new object[] { "(-Infinity, 10)", "(-Infinity, 9)", IntervalRelation.StartedBy };
        yield return new object[] { "(-Infinity, Infinity)", "(-Infinity, Infinity)", IntervalRelation.Equals };
        /// <see cref="ClosedOpenInterval{int}"/>
        yield return new object[] { "[4, 6)", "[5, 9)", IntervalRelation.Overlaps };
        yield return new object[] { "[5, 8)", "[5, 9)", IntervalRelation.Starts };
        yield return new object[] { "[6, 8)", "[5, 9)", IntervalRelation.ContainedBy };
        yield return new object[] { "[6, 9)", "[5, 9)", IntervalRelation.Finishes };
        yield return new object[] { "[5, 9)", "[5, 9)", IntervalRelation.Equals };
        yield return new object[] { "[4, 9)", "[5, 9)", IntervalRelation.FinishedBy };
        yield return new object[] { "[4, 10)", "[5, 9)", IntervalRelation.Contains };
        yield return new object[] { "[5, 10)", "[5, 9)", IntervalRelation.StartedBy };
        yield return new object[] { "[8, 10)", "[5, 9)", IntervalRelation.OverlappedBy };
        yield return new object[] { "[6, Infinity)", "[5, Infinity)", IntervalRelation.Finishes };
        yield return new object[] { "[5, Infinity)", "[5, Infinity)", IntervalRelation.Equals };
        yield return new object[] { "[4, Infinity)", "[5, Infinity)", IntervalRelation.FinishedBy };
        /// <see cref="OpenClosedInterval{int}"/>
        yield return new object[] { "(4, 6]", "(5, 9]", IntervalRelation.Overlaps };
        yield return new object[] { "(5, 8]", "(5, 9]", IntervalRelation.Starts };
        yield return new object[] { "(6, 8]", "(5, 9]", IntervalRelation.ContainedBy };
        yield return new object[] { "(6, 9]", "(5, 9]", IntervalRelation.Finishes };
        yield return new object[] { "(5, 9]", "(5, 9]", IntervalRelation.Equals };
        yield return new object[] { "(4, 9]", "(5, 9]", IntervalRelation.FinishedBy };
        yield return new object[] { "(4, 10]", "(5, 9]", IntervalRelation.Contains };
        yield return new object[] { "(5, 10]", "(5, 9]", IntervalRelation.StartedBy };
        yield return new object[] { "(8, 10]", "(5, 9]", IntervalRelation.OverlappedBy };
        yield return new object[] { "(-Infinity, 8]", "(-Infinity, 9]", IntervalRelation.Starts };
        yield return new object[] { "(-Infinity, 9]", "(-Infinity, 9]", IntervalRelation.Equals };
        yield return new object[] { "(-Infinity, 10]", "(-Infinity, 9]", IntervalRelation.StartedBy };
        /// <see cref="BareInterval{int}"/>
        yield return new object[] { "[1, 5]", "[5, 9]", IntervalRelation.Meets };
        yield return new object[] { "[9, 13]", "[5, 9]", IntervalRelation.MetBy };
        yield return new object[] { "[4, 6]", "[5, 9]", IntervalRelation.Overlaps };
        yield return new object[] { "[5, 8]", "[5, 9]", IntervalRelation.Starts };
        yield return new object[] { "[6, 8]", "[5, 9]", IntervalRelation.ContainedBy };
        yield return new object[] { "[6, 9]", "[5, 9]", IntervalRelation.Finishes };
        yield return new object[] { "[5, 9]", "[5, 9]", IntervalRelation.Equals };
        yield return new object[] { "[4, 9]", "[5, 9]", IntervalRelation.FinishedBy };
        yield return new object[] { "[4, 10]", "[5, 9]", IntervalRelation.Contains };
        yield return new object[] { "[5, 10]", "[5, 9]", IntervalRelation.StartedBy };
        yield return new object[] { "[8, 10]", "[5, 9]", IntervalRelation.OverlappedBy };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
