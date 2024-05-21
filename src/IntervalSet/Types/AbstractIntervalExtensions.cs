//namespace IntervalSet.Types;
//public static class AbstractIntervalExtensions
//{
//    public static TResult Match<T, TResult>(
//        this IAbstractInterval<T> abstractInterval,
//        Func<IBoundedInterval<T>, TResult> bounded,
//        Func<IAbstractInterval<T>, TResult> leftBounded,
//        Func<IRightBoundedInterval<T>, TResult> rightBounded,
//        Func<IComplementInterval<T>, TResult> complement)
//        where T : notnull, IComparable<T>, ISpanParsable<T>
//        => abstractInterval switch
//        {
//            IBoundedInterval<T> boundedInterval => bounded(boundedInterval),
//            IAbstractInterval<T> leftBoundedInterval => leftBounded(leftBoundedInterval),
//            IRightBoundedInterval<T> rightBoundedInterval => rightBounded(rightBoundedInterval),
//            IComplementInterval<T> complementInterval => complement(complementInterval),
//            _ => throw new InvalidOperationException()
//        };
//}
