﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions
{
    public interface IHasInterval<T>
        where T : struct, IComparable<T>, IComparable
    {
        Interval<T> Interval { get; }
    }
}