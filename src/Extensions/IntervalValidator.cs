
namespace IntervalRecord.Extensions
{
    internal static class IntervalValidator
    {
        public static Interval<T> ValidateAndThrow<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.IsValid(out var message))
            {
                throw new ArgumentOutOfRangeException("End", message);
            }
            return value;
        }

        public static bool IsValid<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.IsValid(out _);
        }

        public static bool IsValid<T>(this Interval<T> value, out string? message)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsClosed() && value.End.CompareTo(value.Start) == -1)
            {
                message = "The End parameter must be greater or equal to the Start parameter";
                return false;
            }

            if (!value.IsClosed() && value.End.CompareTo(value.Start) <= 0)
            {
                message = "The End parameter must be greater than the Start parameter";
                return false;
            }
            message = null;
            return true;
        }
    }
}
