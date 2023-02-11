using InfinityComparable;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Linq;

namespace IntervalRecord
{
    public static partial class Interval
    {
        private static readonly Regex intervalRegex = new(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])");
        private const string intervalNotFound = "Interval not found in string. Please provide an interval string in correct format";

        /// <summary>
        /// Parses a string representation of an interval and returns a new interval object.
        /// </summary>
        /// <typeparam name="T">The type of values represented in the interval.</typeparam>
        /// <param name="value">The string representation of the interval to parse.</param>
        /// <returns>A new interval object representing the interval described by the input string.</returns>
        [Pure]
        public static Interval<T> Parse<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var match = intervalRegex.Match(value);
            if (!match.Success)
            {
                throw new ArgumentException(intervalNotFound);
            }
            return ParseInterval<T>(match.Value);
        }

        /// <summary>
        /// Attempts to parse a string representation of an interval and returns a boolean indicating whether or not the parse was successful.
        /// </summary>
        /// <typeparam name="T">The type of values represented in the interval.</typeparam>
        /// <param name="value">The string representation of the interval to parse.</param>
        /// <param name="result">The resulting interval object if the parse was successful, or null if the parse was not successful.</param>
        /// <returns>True if the parse was successful, False otherwise.</returns>
        [Pure]
        public static bool TryParse<T>(string value, out Interval<T>? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            try
            {
                result = Parse<T>(value);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Parses all intervals within a string and returns an enumerable collection of interval objects.
        /// </summary>
        /// <typeparam name="T">The type of values represented in the interval.</typeparam>
        /// <param name="value">The string representation of the intervals to parse.</param>
        /// <returns>An enumerable collection of interval objects representing the intervals described by the input string.</returns>
        [Pure]
        public static IEnumerable<Interval<T>> ParseAll<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var matches = intervalRegex.Matches(value);
            return matches.Select(match => ParseInterval<T>(match.Value));
        }

        private static Interval<T> ParseInterval<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var parts = value.Split(',');
            var startString = parts[0].Trim();
            var endString = parts[1].Trim();

            var start = Infinity.Parse<T>(startString[1..]);
            var end = Infinity.Parse<T>(endString[..(endString.Length - 1)]);

            return new Interval<T>(
                start,
                end,
                !start.IsInfinity && value.StartsWith('['),
                !end.IsInfinity && value.EndsWith(']'));
        }
    }
}
