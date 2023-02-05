﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace IntervalRecord
{
    public static partial class Interval
    {
        private static readonly Regex _intervalRegex =
            new Regex(@"(?:\[|\()(?:[^[\](),]*,[^,()[\]]*)(?:\)|\])");
        private static readonly string[] infinity = { "-inf", "+inf", "inf", "-∞", "+∞", "∞", "null" };
        private const string intervalNotFound = "Interval not found in string. Please provide an interval string in correct format";

        public static Interval<T> Parse<T>(string value, Func<string, T> boundaryParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var match = _intervalRegex.Match(value);
            if (!match.Success)
            {
                throw new ArgumentException(intervalNotFound);
            }
            return ParseInterval(match.Value, boundaryParser);
        }

        public static bool TryParse<T>(string value, Func<string, T> boundaryParser, out Interval<T>? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            try
            {
                result = Parse(value, boundaryParser);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static IEnumerable<Interval<T>> ParseAll<T>(string value, Func<string, T> boundaryParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var matches = _intervalRegex.Matches(value);
            foreach(Match match in matches)
            {
                yield return ParseInterval(match.Value, boundaryParser);
            }
        }

        private static Interval<T> ParseInterval<T>(string value, Func<string, T> boundaryParser)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var parts = Regex
                .Replace(value, @"\s", string.Empty)
                .Split(',');

            var startString = parts[0].Substring(1);
            var endString = parts[1].Substring(0, parts[1].Length - 1);

            var start = ParseBoundary(startString, boundaryParser);
            var end = ParseBoundary(endString, boundaryParser);

            return new Interval<T>(
                start,
                end,
                start is null ? false : value.StartsWith('['),
                end is null ? false : value.EndsWith(']'));
        }

        private static T? ParseBoundary<T>(string value, Func<string, T> boundaryParser)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            if (string.IsNullOrEmpty(value) || infinity.Contains(value))
            {
                return null;
            }
            return boundaryParser(value);
        }
    }
}
