
namespace IntervalRecord.Extensions
{
    public static class IntervalValidator
    {
        private const string mustBeGreaterOrEqualMessage = "The End parameter must be greater or equal to the Start parameter";
        private const string mustBeGreaterThanMessage = "The End parameter must be greater or equal to the Start parameter";
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
            return value.IsClosed() && value.End.CompareTo(value.Start) != -1
                || !value.IsClosed() && value.End.CompareTo(value.Start) == 1;
        }

        public static bool IsValid<T>(this Interval<T> value, out string? message)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsClosed() && value.End.CompareTo(value.Start) == -1)
            {
                message = mustBeGreaterOrEqualMessage;
                return false;
            }

            if (!value.IsClosed() && value.End.CompareTo(value.Start) <= 0)
            {
                message = mustBeGreaterThanMessage;
                return false;
            }
            message = null;
            return true;
        }
    }
}
