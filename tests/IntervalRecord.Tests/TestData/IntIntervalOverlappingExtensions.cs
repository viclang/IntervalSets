using InfinityComparable;

namespace IntervalRecord.Tests.TestData
{
    public static class IntIntervalOverlappingExtensions
    {
        public static Interval<int> GetBefore(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Add(offset).Substract(value.Length()), End = value.Start.Substract(offset) };
        }

        public static Interval<int> GetMeets(this Interval<int> value)
        {
            return value with { Start = value.Start.Substract(value.Length()), End = value.Start };
        }

        public static Interval<int> GetOverlaps(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Substract(offset), End = value.Start.Add(offset) };
        }

        public static Interval<int> GetStarts(this Interval<int> value, int offset)
        {
            return value with { End = value.End.Substract(offset) };
        }

        public static Interval<int> GetContainedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Add(offset), End = value.End.Substract(offset) };
        }

        public static Interval<int> GetFinishes(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Add(offset) };
        }

        public static Interval<int> GetFinishedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Substract(offset) };
        }

        public static Interval<int> GetContains(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start.Substract(offset), End = value.End.Add(offset) };
        }

        public static Interval<int> GetStartedBy(this Interval<int> value, int offset)
        {
            return value with { End = value.End.Add(offset) };
        }

        public static Interval<int> GetOverlappedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.End.Substract(offset), End = value.End.Add(offset) };
        }

        public static Interval<int> GetMetBy(this Interval<int> value)
        {
            return value with { Start = value.End, End = value.End.Add(value.Length()) };
        }

        public static Interval<int> GetAfter(this Interval<int> value, int offset)
        {
            return value with { Start = value.End.Add(offset), End = value.End.Add(offset).Add(value.Length()) };
        }
    }
}
