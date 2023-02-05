using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public struct Infinity<T> : IComparable<Infinity<T>>
        where T : struct, IComparable<T>
    {
        public bool Polarity { get; } = true;
        public T? Finite { get; } = null;

        public Infinity(T? value)
        {
            Finite = value;
        }

        public Infinity(T? value, bool polarity)
        {
            Finite = value;
            Polarity = polarity;
        }

        public bool IsFinite()
        {
            return Finite is not null;
        }

        public override bool Equals(object? other)
        {
            if (Finite is null) return other == null;
            if (other == null) return false;
            return Finite.Value.Equals(other);
        }

        public override int GetHashCode()
        {
            return Finite is not null ? Finite.Value.GetHashCode() : Polarity ? 1 : -1;
        }

        public override string ToString()
        {
#pragma warning disable CS8603 // Possible null reference return.
            return Finite is not null ? Finite.ToString() : (Polarity ? "+∞" : "-∞");
#pragma warning restore CS8603 // Possible null reference return.
        }

        public int CompareTo(Infinity<T> other)
        {
            if (Finite is null && other.Finite is null)
            {
                return Polarity.CompareTo(other.Polarity);
            }
            if (Finite is null && other.Finite is not null)
            {
                return Polarity ? 1 : -1;
            }
            if (Finite is not null && other.Finite is null)
            {
                return other.Polarity ? -1 : 1;
            }
            return Finite!.Value.CompareTo(other.Finite!.Value);
        }

        public static bool operator ==(Infinity<T> a, Infinity<T> b)
        {
            return a.Finite.Equals(b.Finite) && a.Polarity == b.Polarity;
        }
        public static bool operator !=(Infinity<T> a, Infinity<T> b)
        {
            return !a.Finite.Equals(b.Finite) && a.Polarity != b.Polarity;
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
            return new Infinity<T>(value.Finite, false);
        }

        public static Infinity<T> operator +(Infinity<T> value)
        {
            return new Infinity<T>(value.Finite, true);
        }
    }

    public static class Infinity
    {
        public static Infinity<T> Inf<T>() where T : struct, IComparable<T>
        {
            return new();
        }

        public static Infinity<T> OrInfinite<T>(this T? value, bool polarity) where T : struct, IComparable<T>
        {
            return new(value, polarity);
        }

        public static Infinity<T> OrInfinite<T>(this T? value) where T : struct, IComparable<T>
        {
            return new (value);
        }

        public static int Compare<T>(Infinity<T> a, Infinity<T> b) where T : struct, IComparable<T>
        {
            if (a.Finite.HasValue)
            {
                if (b.Finite.HasValue) return Comparer<T>.Default.Compare(a.Finite.Value, b.Finite.Value);
                return 1;
            }
            if (b.Finite.HasValue) return -1;
            return 0;
        }

        public static bool Equals<T>(Infinity<T> a, Infinity<T> b) where T : struct, IComparable<T>
        {
            if (a.Finite.HasValue)
            {
                if (b.Finite.HasValue) return a.Equals(b);
                return false;
            }
            if (b.Finite.HasValue) return false;
            return true;
        }

        public static Type? GetUnderlyingType(Type infinityType)
        {
            if ((object)infinityType == null)
            {
                throw new ArgumentNullException("nullableType");
            }
            Type? result = null;
            if (infinityType.IsGenericType && !infinityType.IsGenericTypeDefinition)
            {
                // instantiated generic type only
                Type genericType = infinityType.GetGenericTypeDefinition();
                if (Object.ReferenceEquals(genericType, typeof(Infinity<>)))
                {
                    result = infinityType.GetGenericArguments()[0];
                }
            }
            return result;
        }
    }
}
