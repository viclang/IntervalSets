//namespace IntervalRecords.Experiment.Helpers;
//public static class EndpointComparer
//{
//    /// <summary>
//    /// Compares the start of two intervals.
//    /// </summary>
//    /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
//    public static int CompareStart<T>(this Interval<T> left, Interval<T> right)
//        where T : struct, IComparable<T>, ISpanParsable<T>
//    {
//        return (left.Start.HasValue, right.Start.HasValue) switch
//        {
//            (false, false) => 0,
//            (false, true) => -1,
//            (true, false) => 1,
//            (true, true) =>
//                left.Start!.Value.CompareTo(right.Start!.Value) switch
//                {
//                    0 => left.StartInclusive.CompareTo(right.StartInclusive),
//                    var comparison => comparison
//                }
//        };
//    }

//    /// <summary>
//    /// Compares the end of two intervals.
//    /// </summary>
//    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
//    public static int CompareEnd<T>(this Interval<T> left, Interval<T> right)
//        where T : struct, IComparable<T>, ISpanParsable<T>
//    {
//        return (left.End.HasValue, right.End.HasValue) switch
//        {
//            (false, false) => 0,
//            (false, true) => 1,
//            (true, false) => -1,
//            (true, true) =>
//                left.End!.Value.CompareTo(right.End!.Value) switch
//                {
//                    0 => left.EndInclusive.CompareTo(right.EndInclusive),
//                    var comparison => comparison
//                }
//        };
//    }

//    /// <summary>
//    /// Compares the start of the first interval with the end of the second interval.
//    /// </summary>
//    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
//    public static int CompareStartToEnd<T>(this Interval<T> left, Interval<T> right)
//        where T : struct, IComparable<T>, ISpanParsable<T>
//    {
//        if (left.Start is null || right.End is null)
//        {
//            return -1;
//        }

//        int comparison = left.Start!.Value.CompareTo(right.End!.Value);
//        if (comparison == 0 && (!left.StartInclusive || !right.EndInclusive))
//        {
//            return 1;
//        }
//        return comparison;
//    }

//    /// <summary>
//    /// Compares the end of the first interval with the start of the second interval.
//    /// </summary>
//    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
//    public static int CompareEndToStart<T>(this Interval<T> left, Interval<T> right)
//        where T : struct, IComparable<T>, ISpanParsable<T>
//    {
//        if (left.End is null || right.Start is null)
//        {
//            return 1;
//        }

//        int comparison = left.End!.Value.CompareTo(right.Start!.Value);
//        if (comparison == 0 && (!left.EndInclusive || !right.StartInclusive))
//        {
//            return -1;
//        }
//        return comparison;
//    }
//}
