using IntervalRecords.Types;
using Unbounded;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Creates an open interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open interval with the specified start and end</returns>
        public static OpenInterval<T> Open<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(start, end);

        /// <summary>
        /// Creates a closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new closed interval with the specified start and end</returns>
        public static ClosedInterval<T> Closed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedInterval<T>(start, end);

        /// <summary>
        /// Creates an open-closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open-closed interval with the specified start and end</returns>
        public static OpenClosedInterval<T> OpenClosed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenClosedInterval<T>(start, end);

        /// <summary>
        /// Creates an interval with a closed start and open end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The value to use as the start of the interval</param>
        /// <param name="end">The value to use as the end of the interval</param>
        public static ClosedOpenInterval<T> ClosedOpen<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedOpenInterval<T>(start, end);
    }
}
