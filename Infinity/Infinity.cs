using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity
{
    public struct Infinity<T> : IComparable<Infinity<T>>
        where T : struct, IComparable<T>
    {
        private bool polarity = true;
        public bool IsInfinite { get; } = true;
        public T Value { get; }


        public Infinity(T value)
        {
            Value = value;
            IsInfinite = false;
        }

        public override bool Equals(object? other)
        {
            if (IsInfinite) return false;
            if (other == null) return false;
            return Value.Equals(other);
        }

        public override int GetHashCode()
        {
            return IsInfinite ? polarity ? 1 : -1
                : Value.GetHashCode();
        }

        public override string ToString()
        {
            if (IsInfinite)
            {
                return polarity ? "+∞" : "-∞";
            }
#pragma warning disable CS8603 // Possible null reference return.
            return Value.ToString();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public int CompareTo(Infinity<T> other)
        {
            if (IsInfinite && other.IsInfinite)
            {
                return polarity.CompareTo(other.polarity);
            }
            if (IsInfinite && !other.IsInfinite)
            {
                return polarity ? 1 : -1;
            }
            if (!IsInfinite && other.IsInfinite)
            {
                return other.polarity ? -1 : 1;
            }
            return Value.CompareTo(other.Value);
        }

        public static bool operator ==(Infinity<T> a, Infinity<T> b)
        {
            return !a.IsInfinite && !b.IsInfinite && a.Value.Equals(b.Value)
                || a.polarity == b.polarity;
        }
        public static bool operator !=(Infinity<T> a, Infinity<T> b)
        {
            return !a.Value.Equals(b.Value) && a.polarity != b.polarity;
        }

        public static bool operator >(Infinity<T> a, Infinity<T> b)
        {
            return a.CompareTo(b) == 1;
        }

        public static bool operator <(Infinity<T> a, Infinity<T> b)
        {
            return a.CompareTo(b) == -1;
        }

        public static bool operator >=(Infinity<T> a, Infinity<T> b)
        {
            return a == b || a > b;
        }

        public static bool operator <=(Infinity<T> a, Infinity<T> b)
        {
            return a == b || a < b;
        }
        public static Infinity<T> operator -(Infinity<T> value)
        {
            value.polarity = false;
            return value;
        }

        public static Infinity<T> operator +(Infinity<T> value)
        {
            value.polarity = true;
            return value;
        }

        public static implicit operator Infinity<T>(T? value)
        {
            return value.HasValue ? new Infinity<T>(value.Value) : new Infinity<T>();
        }
        public static implicit operator T?(Infinity<T> value)
        {
            return (value.IsInfinite ? null : value);
        }
    }

    public static class Infinity
    {
        public static Infinity<T> ToInfinity<T>(this T? value) where T : struct, IComparable<T>
        {
            return value;
        }

        public static Infinity<T> Inf<T>() where T : struct, IComparable<T>
        {
            return new Infinity<T>();
        }

        public static Infinity<T> ToNegativeInfinity<T>(this T? value) where T : struct, IComparable<T>
        {
            return -new Infinity<T>(value.GetValueOrDefault());
        }

        public static Infinity<T> ToPositiveInfinity<T>(this T? value) where T : struct, IComparable<T>
        {
            return new Infinity<T>(value.GetValueOrDefault());
        }
    }
}
