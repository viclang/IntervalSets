using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityComparable
{
    public partial struct Infinity<T> : IEquatable<Infinity<T>>, IComparable<Infinity<T>>, IComparable
        where T : struct, IComparable<T>, IComparable
    {
        private readonly bool positive;
        public T? Finite { get; } = null;
        public bool IsInfinite => !Finite.HasValue;

        public Infinity()
        {
            positive = true;
        }

        public Infinity(bool positive)
        {
            this.positive = positive;
        }

        public Infinity(T? value, bool positive)
        {
            Finite = value;
            this.positive = positive;
        }

        public bool Equals(Infinity<T> other)
        {
            return this == other;
        }

        public override bool Equals(object? other)
        {
            if (!IsInfinite)
            {
                return Finite.Equals(other);
            }
            if (other is Infinity<T>)
            {
                return this == (Infinity<T>)other;
            }
            if (IsInfinite && other is double)
            {
                return positive && ((double)other) == double.PositiveInfinity
                    || !positive && ((double)other) == double.NegativeInfinity;
            }
            if (IsInfinite && other is float)
            {
                return positive && ((float)other) == float.PositiveInfinity
                    || !positive && ((float)other) == float.NegativeInfinity;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return IsInfinite ? positive ? 1 : -1
                : Finite.GetHashCode();
        }


        public int CompareTo(Infinity<T> other)
        {
            if (IsInfinite && other.IsInfinite)
            {
                return positive.CompareTo(other.positive);
            }
            if (IsInfinite && !other.IsInfinite)
            {
                return positive ? 1 : -1;
            }
            if (!IsInfinite && other.IsInfinite)
            {
                return other.positive ? -1 : 1;
            }
            return Finite!.Value.CompareTo(other.Finite!.Value);
        }

        public int CompareTo(object? other)
        {
            if (IsInfinite && other is null)
            {
                return 0;
            }
            if (other is Infinity<T>)
            {
                return CompareTo((Infinity<T>)other);
            }
            if (IsInfinite && other is double && double.IsInfinity((double)other))
            {
                return (double)other == double.PositiveInfinity
                    ? (positive ? 0 : -1)
                    : (positive ? 1 : 0);
            }
            if (IsInfinite && other is float && float.IsInfinity((float)other))
            {
                return (float)other == double.PositiveInfinity
                    ? (positive ? 0 : -1)
                    : (positive ? 1 : 0);
            }
            if (IsInfinite && other is not null)
            {
                return positive ? 1 : -1;
            }
            return Finite!.Value.CompareTo(other);
        }

        public override string ToString()
        {
            if (IsInfinite)
            {
                return positive ? "+∞" : "-∞";
            }
            return string.Empty + Finite.ToString();
        }

        public static implicit operator Infinity<T>(T? value)
        {
            return new Infinity<T>(value, true);
        }

        public static implicit operator T?(Infinity<T> value)
        {
            return value.Finite;
        }

        public static Infinity<T> operator -(Infinity<T> value)
        {
            return new Infinity<T>(false);
        }

        public static Infinity<T> operator +(Infinity<T> value)
        {
            return new Infinity<T>(true);
        }

        public static bool operator ==(Infinity<T> left, Infinity<T> right)
        {
            return left.IsInfinite && right.IsInfinite && left.positive == right.positive
                || !left.IsInfinite && !right.IsInfinite && left.Finite.Equals(right.Finite);
        }

        public static bool operator !=(Infinity<T> left, Infinity<T> right)
        {
            return !left.Finite.Equals(right.Finite) && left.positive != right.positive;
        }

        public static bool operator >(Infinity<T> left, Infinity<T> right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(Infinity<T> left, Infinity<T> right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(Infinity<T> left, Infinity<T> right)
        {
            return left == right || left > right;
        }

        public static bool operator <=(Infinity<T> left, Infinity<T> right)
        {
            return left == right || left < right;
        }
    }

    public static class Infinity
    {
        public static Infinity<T> Inf<T>(T? value = null, bool positive = true)
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>(value, positive);
        }

        public static Infinity<T> Inf<T>(bool positive)
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>(positive);
        }

        public static Infinity<T> Inf<T>()
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>();
        }

        public static Infinity<T> ToInfinity<T>(this T? value, bool positive)
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>(value, positive);
        }

        public static Infinity<T> ToPositiveInfinity<T>(this T? value)
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>(value, true);
        }

        public static Infinity<T> ToNegativeInfinity<T>(this T? value)
            where T : struct, IComparable<T>, IComparable
        {
            return new Infinity<T>(value, false);
        }
    }
}
