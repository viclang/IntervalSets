using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions
{
    public record struct Interval<T>
        where T : struct, IComparable<T>, IComparable
    {
        public T? Start { get; init; }
        public T? End { get; init; }
        public (bool Start, bool End) Inclusive { get; init; }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
        {
            Start = start;
            End = end;
            Inclusive = (startInclusive, endInclusive);

            Validate();

            if (Start is not null && End is not null && !Inclusive.Start && !Inclusive.End)
            {
                throw new NotSupportedException("Open interval is not supported!");
            }
        }

        public Interval(T? start, T? end, (bool start, bool end) inclusive)
        {
            Start = start;
            End = end;
            Inclusive = inclusive;

            Validate();

            if (Start is not null && End is not null && !Inclusive.Start && !Inclusive.End)
            {
                throw new NotSupportedException("Open interval is not supported");
            }
        }

        private void Validate()
        {
            if (Start is not null && End is not null
                    && Inclusive.Start && Inclusive.End
                    && End.Value.CompareTo(Start.Value) == -1)
            {
                throw new ArgumentOutOfRangeException(nameof(End), "The end parameter must be greater or equal to the start parameter");
            }

            if (Start is not null && End is not null
                    && (!Inclusive.Start || !Inclusive.End)
                    && End.Value.CompareTo(Start.Value) <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(End), "The end parameter must be greater than the start parameter");
            }
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

            if (!a.Inclusive.Start || !b.Inclusive.End)
            {
                return a.Start.Value.CompareTo(b.End.Value) >= 0;
            }

            return a.Start.Value.CompareTo(b.End.Value) == 1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            if(a.End is null || b.Start is null)
            {
                return false;
            }

            if(!a.Inclusive.End || !b.Inclusive.Start)
            {
                return a.End.Value.CompareTo(b.Start.Value) <= 0;
            }

            return a.End.Value.CompareTo(b.Start.Value) == -1;
        }

        public override string? ToString()
        {
            var start = Start is null
                ? "(-∞"
                : Inclusive.Start ? "[" : "("
                    + Start.Value;

            var end = End is null
                ? "+∞)"
                : Inclusive.End ? "]" : ")"
                    + End.Value;

            return string.Join(", ", start, end);
        }
    }
}
