using InfinityComparable;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestHelper
{
    public static class GenericExtensions
    {
        public static T? ToBoundary<T>(this int? boundary)
            where T : struct
        {
            var type = typeof(T);
            return Type.GetTypeCode(type) switch
            {
                TypeCode.Int32 => (T?)(object?)boundary,
                TypeCode.Double => (T?)(object?)(double?)boundary,
                TypeCode.DateTime => (T?)(object?)(boundary is null ? null : new DateTime(2022, 1, boundary.Value)),
                _ when type == typeof(DateTimeOffset) => boundary is null ? null : (T?)(object)new DateTimeOffset(2022, 1, boundary.Value, 0, 0, 0, TimeSpan.Zero),
                _ when type == typeof(DateOnly) => (T?)(object?)(boundary is null ? null : new DateOnly(2022, 1, boundary.Value)),
                _ when type == typeof(TimeOnly) => (T?)(object?)(boundary is null ? null : new TimeOnly(boundary.Value, 0)),
                _ => throw new NotSupportedException(type.FullName)
            };
        }

        public static TStep ToStep<T, TStep>(this int step)
            where T : struct
            where TStep : struct
        {
            var type = typeof(T);
            if (type == typeof(int) || type == typeof(DateOnly))
            {
                return (TStep)(object)step;
            }
            if (type == typeof(double))
            {
                return (TStep)(object)(double)step;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                return (TStep)(object)TimeSpan.FromDays(step);
            }
            else if (type == typeof(TimeOnly))
            {
                return (TStep)(object)TimeSpan.FromHours(step);
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }



        public static TResult? ToRadiusResult<T, TResult>(this double? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            var type = typeof(T);
            if (type == typeof(int) || type == typeof(double))
            {
                return (TResult?)(object?)result;
            }
            if (type == typeof(DateOnly))
            {
                return (TResult?)(object?)(result is null ? null : (int)result.Value);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
            {
                return (TResult?)(object?)(result is null ? null : TimeSpan.FromDays(result.Value));
            }
            else if (type == typeof(TimeOnly))
            {
                return (TResult?)(object?)(result is null ? null : TimeSpan.FromHours(result.Value));
            }
            else
            {
                throw new NotSupportedException(type.FullName);
            }
        }
    }
}
