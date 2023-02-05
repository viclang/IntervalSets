using IntervalRecord.Extensions;
using System;

namespace IntervalRecord.Tests.DataSets
{
    public static class OffsetCreator
    {
        public static IOffsetCreator<T> Create<T>(Interval<T> reference, int offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (reference, offset) switch
            {
                (Interval<int> typedReference, int typedOffset) =>
                    (IOffsetCreator<T>)(object)new IntegerCreator(typedReference, typedOffset),
                (Interval<DateOnly> typedReference, int typedOffset) =>
                    (IOffsetCreator<T>)(object)new DateOnlyCreator(typedReference, typedOffset),
                (_, _) => throw new NotImplementedException()
            };

        public static IOffsetCreator<T> Create<T>(Interval<T> reference, TimeSpan offset)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (reference, offset) switch
        {
            (Interval<DateTime> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T>)(object)new DateTimeCreator(typedReference, typedOffset),
            (Interval<DateTimeOffset> typedReference, TimeSpan typedOffset) =>
                (IOffsetCreator<T>)(object)new DateTimeOffsetCreator(typedReference, typedOffset),
            (_, _) => throw new NotImplementedException()
        };
    }

    public readonly struct IntegerCreator : IOffsetCreator<int>
    {
        public Interval<int> Before { get; init; }
        public Interval<int> ContainedBy { get; init; }
        public Interval<int> After { get; init; }

        public IntegerCreator(Interval<int> reference, int offset)
        {
            var length = reference.Length().Finite!.Value;
            var beforeEnd = reference.Start - offset;
            var beforeStart = beforeEnd - length;
            var containsStart = reference.Start + offset;
            var containsEnd = reference.End - offset;
            var afterStart = reference.End + offset;
            var afterEnd = afterStart + length;

            Before = reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateOnlyCreator : IOffsetCreator<DateOnly>
    {
        public Interval<DateOnly> Before { get; init; }
        public Interval<DateOnly> ContainedBy { get; init; }
        public Interval<DateOnly> After { get; init; }

        public DateOnlyCreator(Interval<DateOnly> reference, int offset)
        {
            var length = reference.Length().Finite!.Value;
            var beforeEnd = reference.Start.Finite!.Value.AddDays(-offset);
            var beforeStart = beforeEnd.AddDays(-length);
            var containsStart = reference.Start.Finite.Value.AddDays(offset);
            var containsEnd = reference.End.Finite!.Value.AddDays(-offset);
            var afterStart = reference.End.Finite!.Value.AddDays(offset);
            var afterEnd = afterStart.AddDays(length);

            Before = reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateTimeCreator : IOffsetCreator<DateTime>
    {
        public Interval<DateTime> Before { get; init; }
        public Interval<DateTime> ContainedBy { get; init; }
        public Interval<DateTime> After { get; init; }

        public DateTimeCreator(Interval<DateTime> reference, TimeSpan offset)
        {

            var length = reference.Length().Finite!.Value;
            DateTime referenceStart = (DateTime)reference.Start!;
            DateTime referenceEnd = (DateTime)reference.End!;

            DateTime beforeEnd = referenceStart - offset;
            DateTime beforeStart = beforeEnd - length;
            DateTime containsStart = referenceStart + offset;
            DateTime containsEnd = referenceEnd - offset;
            DateTime afterStart = referenceEnd + offset;
            DateTime afterEnd = afterStart + length;

            Before = reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }

    public readonly struct DateTimeOffsetCreator : IOffsetCreator<DateTimeOffset>
    {
        public Interval<DateTimeOffset> Before { get; init; }
        public Interval<DateTimeOffset> ContainedBy { get; init; }
        public Interval<DateTimeOffset> After { get; init; }

        public DateTimeOffsetCreator(Interval<DateTimeOffset> reference, TimeSpan offset)
        {
            var length = reference.Length().Finite!.Value;
            DateTimeOffset referenceStart = (DateTimeOffset)reference.Start!;
            DateTimeOffset referenceEnd = (DateTimeOffset)reference.End!;

            var beforeEnd = referenceStart - offset;
            var beforeStart = beforeEnd - length;
            var containsStart = referenceStart + offset;
            var containsEnd = referenceEnd - offset;
            var afterStart = referenceEnd + offset;
            var afterEnd = afterStart + length;

            Before = reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = reference with { Start = containsStart, End = containsEnd };
            After = reference with { Start = afterStart, End = afterEnd };
        }
    }
}
