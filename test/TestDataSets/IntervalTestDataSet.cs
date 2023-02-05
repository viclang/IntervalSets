using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalRecord.Tests.TestDataSets
{
    public abstract class IntervalTestDataSet<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public IEnumerable<Interval<T>> DataSet { get; }
        public Interval<T> Reference { get; init; }
        public Interval<T> Before { get; protected set; }
        public Interval<T> Meets { get; protected set; }
        public Interval<T> Overlaps { get; protected set; }
        public Interval<T> Starts { get; protected set; }
        public Interval<T> ContainedBy { get; protected set; }
        public Interval<T> Finishes { get; protected set; }
        public Interval<T> Equal { get; protected set; }
        public Interval<T> FinishedBy { get; protected set; }
        public Interval<T> Contains { get; protected set; }
        public Interval<T> StartedBy { get; protected set; }
        public Interval<T> OverlappedBy { get; protected set; }
        public Interval<T> MetBy { get; protected set; }
        public Interval<T> After { get; protected set; }

        public IntervalTestDataSet(Interval<T> reference)
        {
            Reference = reference;
        }

        protected void Generate()
        {
            Meets = Reference with { Start = Before.Start, End = Reference.Start };
            Overlaps = Reference with { Start = Contains.End, End = After.End };
            Starts = Reference with { End = After.Start };
            ContainedBy = Reference with { Start = Before.Start, End = After.End };
            Finishes = Reference with { Start = Contains.Start };
            Equal = Reference with { };
            FinishedBy = Reference with { Start = Before.Start };
            StartedBy = Reference with { End = Contains.End };
            OverlappedBy = Reference with { Start = Before.Start, End = Contains.Start };
            MetBy = Reference with { Start = Reference.End, End = After.End };
        }

        public TheoryData<Interval<T>, Interval<T>, bool> OverlapsWith => new TheoryData<Interval<T>, Interval<T>, bool>
        {
            { Reference, Before, false },
            { Reference, Meets, Reference.IntervalType == IntervalType.Closed },
            { Reference, Overlaps, true },
            { Reference, Starts, true },
            { Reference, ContainedBy, true },
            { Reference, Finishes, true },
            { Reference, Equal, true },
            { Reference, FinishedBy, true },
            { Reference, Contains, true },
            { Reference, StartedBy, true },
            { Reference, OverlappedBy, true },
            { Reference, MetBy, Reference.IntervalType == IntervalType.Closed },
            { Reference, After, false }
        };
    }
}
