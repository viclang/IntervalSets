namespace IntervalRecord
{
    public static class IntegerInterval
    {
        
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
