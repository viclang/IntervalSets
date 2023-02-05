using IntervalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntervalRecord
{
    internal static class IntervalStringReader
    {
        internal static Interval<T> FromString<T>(string interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            Validate(interval);

            var parts = Regex.Replace(interval, @"\s", string.Empty)
                .Split(',');

            var startString = parts[0].Substring(1);
            var endString = parts[1].Substring(0, parts[1].Length-1);

            var start = TryParse<T>(startString);
            var end = TryParse<T>(endString);

            return new Interval<T>(
                start,
                end,
                start is null ? false : interval.StartsWith('['),
                end is null ? false : interval.EndsWith(']'));
        }

        private static void Validate(string interval)
        {
            if (string.IsNullOrEmpty(interval))
            {
                throw new ArgumentNullException(nameof(interval));
            }

            if ((!interval.StartsWith('(') || !interval.StartsWith('['))
                && (!interval.EndsWith(')') || !interval.EndsWith(']'))
                && Regex.Matches(interval, ",").Count != 1)
            {
                throw new ArgumentException("The interval string should be in correct format.");
            }
        }

        private static T? TryParse<T>(string value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if(string.IsNullOrEmpty(value)
                || value == "-∞"
                || value == "+∞"
                || value == "∞"
                || value == "null")
            {
                return null;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
