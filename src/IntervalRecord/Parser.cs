using InfinityComparable;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace IntervalRecord
{
    public static partial class Interval
    {
        private static readonly Regex intervalRegex = new Regex(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])");
        private const string intervalNotFound = "Interval not found in string. Please provide an interval string in correct format";

        [Pure]
        public static Interval<T> Parse<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => Parse(value, s => Infinity.Parse<T>(s));

        [Pure]
        public static Interval<T> Parse<T>(string value, Func<string, Infinity<T>> infinityParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var match = intervalRegex.Match(value);
            if (!match.Success)
            {
                throw new ArgumentException(intervalNotFound);
            }
            return ParseInterval(match.Value, infinityParser);
        }

        [Pure]
        public static bool TryParse<T>(string value, out Interval<T>? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => TryParse(value, s => Infinity.Parse<T>(s), out result);

        [Pure]
        public static bool TryParse<T>(string value, Func<string, Infinity<T>> infinityParser, out Interval<T>? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            try
            {
                result = Parse(value, infinityParser);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        [Pure]
        public static IEnumerable<Interval<T>> ParseAll<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ParseAll(value, s => Infinity.Parse<T>(s));

        [Pure]
        public static IEnumerable<Interval<T>> ParseAll<T>(string value, Func<string, Infinity<T>> infinityParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var matches = intervalRegex.Matches(value);
            foreach(Match match in matches)
            {
                yield return ParseInterval(match.Value, infinityParser);
            }
        }

        [Pure]
        private static Interval<T> ParseInterval<T>(string value, Func<string, Infinity<T>> infinityParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var parts = value.Split(',');
            var startString = parts[0].Trim();
            var endString = parts[1].Trim();

            var start = infinityParser(startString[1..]);
            var end = infinityParser(endString[..(endString.Length - 1)]);

            return new Interval<T>(
                start,
                end,
                start.IsInfinite ? false : value.StartsWith('['),
                end.IsInfinite ? false : value.EndsWith(']'));
        }
    }
}
