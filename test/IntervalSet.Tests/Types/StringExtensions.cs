using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSet.Tests.Types;
internal static class StringExtensions
{
    internal static (T? start, T? end) ParseEndpoints<T>(
        this string value,
        char separator = ',',
        IFormatProvider? provider = null)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return value.AsSpan().ParseEndpoints<T>(separator, provider);
    }

    internal static (T? start, T? end) ParseNullableEndpoints<T>(
        this string value,
        char separator = ',',
        IFormatProvider? provider = null)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        return value.AsSpan().ParseNullableEndpoints<T>(separator, provider);
    }

    internal static (T? start, T? end) ParseNullableEndpoints<T>(
        this ReadOnlySpan<char> value,
        char separator = ',',
        IFormatProvider? provider = null)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        var commaIndex = value.IndexOf(separator);
        if (commaIndex == -1)
        {
            throw new Exception();
        }
        T? start = commaIndex > 0 ? T.Parse(value[..commaIndex].Trim(), provider) : null;
        T? end = value.Length > (commaIndex + 1) ? T.Parse(value[(commaIndex + 1)..].Trim(), provider) : null;
        return (start, end);
    }

    internal static (T start, T end) ParseEndpoints<T>(
        this ReadOnlySpan<char> value,
        char separator = ',',
        IFormatProvider? provider = null)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var  commaIndex = value.IndexOf(separator);
        if(commaIndex == -1)
        {
            throw new Exception();
        }
        T start = commaIndex > 0 ? T.Parse(value[..commaIndex].Trim(), provider) : default!;
        T end = value.Length > (commaIndex + 1) ? T.Parse(value[(commaIndex + 1)..].Trim(), provider) : default!;
        return (start, end);
    }
}
