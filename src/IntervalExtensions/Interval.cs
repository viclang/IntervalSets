using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions
{
    public readonly record struct Interval<T>
        where T : struct, IComparable<T>, IComparable
    {
        public readonly record struct Endpoint
        {
            public T EndpointValue { get; init; }
            public bool Included { get; init; }

            public Endpoint(T value, bool included)
            {
                EndpointValue = value;
                Included = included;
            }
        }

        public Endpoint? Start { get; init; }
        public Endpoint? End { get; init; }

        public Interval(Endpoint? start, Endpoint? end)
        {
            Start = start;
            End = end;
        }

        public Interval(T? start, T? end, (bool start, bool end) included)
        {
            Start = start is not null
                ? new Endpoint(start.Value, included.start)
                : null;

            End = end is not null
                ? new Endpoint(end.Value, included.end)
                : null;
        }

        public bool OverlapsWith(Interval<T> other)
        {
            return !(this < other) && !(this > other);
        }

        public static bool operator >(Interval<T> a, Interval<T> b)
        {
            if (a.Start is null || b.End is null)
            {
                return false;
            }

            if (a.Start.Value.Included && !b.End.Value.Included
                || !a.Start.Value.Included && b.End.Value.Included)
            {
                return a.Start.Value.EndpointValue.CompareTo(b.End.Value.EndpointValue) >= 0;
            }

            if (!a.Start.Value.Included && !b.End.Value.Included)
            {
                return a.Start.Value.EndpointValue.CompareTo(b.End.Value.EndpointValue) == 0;
            }

            return a.Start.Value.EndpointValue.CompareTo(b.End.Value.EndpointValue) == 1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            if(a.End is null || b.Start is null)
            {
                return false;
            }

            if(a.End.Value.Included && !b.Start.Value.Included
                || !a.End.Value.Included && b.Start.Value.Included)
            {
                return a.End.Value.EndpointValue.CompareTo(b.Start.Value.EndpointValue) <= 0;
            }

            if (!a.End.Value.Included && !b.Start.Value.Included)
            {
                return a.End.Value.EndpointValue.CompareTo(b.Start.Value.EndpointValue) == 0;
            }

            return a.End.Value.EndpointValue.CompareTo(b.Start.Value.EndpointValue) == -1;
        }

        public override string? ToString()
        {
            var start = Start is null
                ? "(-∞"
                : Start.Value.Included ? "[" : "("
                    + Start.Value.EndpointValue;

            var end = End is null
                ? "+∞)"
                : End.Value.Included ? "]" : ")"
                    + End.Value.EndpointValue;

            return string.Join(", ", start, end);
        }
    }
}
