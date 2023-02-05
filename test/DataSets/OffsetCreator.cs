using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.DataSets
{
    public static class OffsetCreator
    {
        public static IOffsetCreator<T, TOffset> Create<T, TOffset>(Interval<T> reference, TOffset offset)
            where T : struct, IEquatable<T>, IComparable<T>
            => (reference, offset) switch
        {
            (Interval<int> typedReference, int typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new IntegerCreator(typedReference, typedOffset),
            (Interval<DateOnly> typedReference, int typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateOnlyCreator(typedReference, typedOffset),
            (Interval<DateTime> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateTimeCreator(typedReference, typedOffset),
            (Interval<DateTimeOffset> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T, TOffset>)(object)new DateTimeOffsetCreator(typedReference, typedOffset),
            (_, _) => throw new NotImplementedException()
        };
    }

    public readonly struct IntegerCreator : IOffsetCreator<int, int>
    {
        public Interval<int> Before { get; init; }
        public Interval<int> Contains { get; init; }
        public Interval<int> After { get; init; }

        public IntegerCreator(Interval<int> reference, int offset)
        {
            var beforeEnd = reference.Start - offset;
            var beforeStart = beforeEnd - reference.Length();
            var containsStart = reference.Start + offset;
            var containsEnd = reference.End - offset;
            var afterStart = reference.End + offset;
            var afterEnd = afterStart + reference.Length();

            Before = reference with { Start = beforeStart, End = beforeEnd };
            Contains = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateOnlyCreator : IOffsetCreator<DateOnly, int>
    {
        public Interval<DateOnly> Before { get; init; }
        public Interval<DateOnly> Contains { get; init; }
        public Interval<DateOnly> After { get; init; }

        public DateOnlyCreator(Interval<DateOnly> reference, int offset)
        {
            var length = reference.Length()!.Value;
            var beforeEnd = reference.Start!.Value.AddDays(-offset);
            var beforeStart = beforeEnd.AddDays(-length);
            var containsStart = reference.Start!.Value.AddDays(offset);
            var containsEnd = reference.End!.Value.AddDays(-offset);
            var afterStart = reference.End!.Value.AddDays(offset);
            var afterEnd = afterStart.AddDays(length);

            Before = reference with { Start = beforeStart, End = beforeEnd };
            Contains = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateTimeCreator : IOffsetCreator<DateTime, TimeSpan>
    {
        public Interval<DateTime> Before { get; init; }
        public Interval<DateTime> Contains { get; init; }
        public Interval<DateTime> After { get; init; }

        public DateTimeCreator(Interval<DateTime> reference, TimeSpan offset)
        {
            var beforeEnd = reference.Start - offset;
            var beforeStart = beforeEnd - reference.Length();
            var containsStart = reference.Start + offset;
            var containsEnd = reference.End - offset;
            var afterStart = reference.End + offset;
            var afterEnd = afterStart + reference.Length();

            Before = reference with { Start = beforeStart, End = beforeEnd };
            Contains = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateTimeOffsetCreator : IOffsetCreator<DateTimeOffset, TimeSpan>
    {
        public Interval<DateTimeOffset> Before { get; init; }
        public Interval<DateTimeOffset> Contains { get; init; }
        public Interval<DateTimeOffset> After { get; init; }

        public DateTimeOffsetCreator(Interval<DateTimeOffset> reference, TimeSpan offset)
        {
            var beforeEnd = reference.Start - offset;
            var beforeStart = beforeEnd - reference.Length();
            var containsStart = reference.Start + offset;
            var containsEnd = reference.End - offset;
            var afterStart = reference.End + offset;
            var afterEnd = afterStart + reference.Length();

            Before = reference with { Start = beforeStart, End = beforeEnd };
            Contains = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }
}
