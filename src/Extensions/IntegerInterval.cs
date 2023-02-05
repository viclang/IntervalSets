
namespace IntervalRecord.Extensions
{
    public static class IntegerInterval
    {
        public static int? Length(this Interval<int> value)
        {
            if (value.IsUnBounded())
            {
                return null;
            }

            if (value.IsEmpty())
            {
                return 0;
            }

            var start = value.Start.Finite ?? int.MinValue;
            var end = value.End.Finite ?? int.MaxValue;
            return end - start;
        }

        public static double? Radius(this Interval<int> value)
        {
            if (value.IsUnBounded() || value.IsEmpty())
            {
                return null;
            }

            return value.Length() / 2;
        }

        public static double? Centre(this Interval<int> value)
        {
            if (value.IsUnBounded() || value.IsEmpty())
            {
                return null;
            }

            var start = value.Start.Finite ?? int.MinValue;
            var end = value.End.Finite ?? int.MaxValue;
            return (start + end) / 2;
        }

        public static Interval<int> Add(Interval<int> a, Interval<int> b)
        {
            if (b.IsEmpty())
            {
                return a;
            }

            return a with { Start = a.Start + b.Start, End = a.End + b.End };
        }

        public static Interval<int> Add(Interval<int> a, int b)
        {
            return a with { Start = a.Start + b, End = a.End + b };
        }

        public static Interval<int> Substract(Interval<int> a, Interval<int> b)
        {
            if (b.IsEmpty())
            {
                return a;
            }

            return a with { Start = a.Start - b.Start, End = a.End - b.End };
        }

        public static Interval<int> Substract(Interval<int> a, int b)
        {
            return a with { Start = a.Start - b, End = a.End - b };
        }
    }
}
