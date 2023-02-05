using IntervalRecord.Tests.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public sealed record class IntegerOverlappingDataSet : BaseOverlappingDataSet<int>
    {
        private readonly int _offset;

        public IntegerOverlappingDataSet(int start, int end, BoundaryType boundaryType, int offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start.Value - _offset;
            var beforeStart = beforeEnd - length;
            var containsStart = Reference.Start.Value + _offset;
            var containsEnd = Reference.End.Value - _offset;
            var afterStart = Reference.End.Value + _offset;
            var afterEnd = afterStart + length;

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public sealed record class DoubleOverlappingDataSet : BaseOverlappingDataSet<double>
    {
        private readonly double _offset;

        public DoubleOverlappingDataSet(double start, double end, BoundaryType boundaryType, double offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start - _offset;
            var beforeStart = beforeEnd - length;
            var containsStart = Reference.Start + _offset;
            var containsEnd = Reference.End - _offset;
            var afterStart = Reference.End + _offset;
            var afterEnd = afterStart + length;

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public sealed record class DateOnlyOverlappingDataSet : BaseOverlappingDataSet<DateOnly>
    {
        private readonly int _offset;

        public DateOnlyOverlappingDataSet(DateOnly start, DateOnly end, BoundaryType boundaryType, int offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start.Value.AddDays(-_offset);
            var beforeStart = beforeEnd.AddDays(-length);
            var containsStart = Reference.Start.Value.AddDays(_offset);
            var containsEnd = Reference.End.Value.AddDays(-_offset);
            var afterStart = Reference.End.Value.AddDays(_offset);
            var afterEnd = afterStart.AddDays(length);

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public sealed record class TimeOnlyOverlappingDataSet : BaseOverlappingDataSet<TimeOnly>
    {
        private readonly TimeSpan _offset;

        public TimeOnlyOverlappingDataSet(TimeOnly start, TimeOnly end, BoundaryType boundaryType, TimeSpan offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start.Value.Add(-_offset);
            var beforeStart = beforeEnd.Add(-length);
            var containsStart = Reference.Start.Value.Add(_offset);
            var containsEnd = Reference.End.Value.Add(-_offset);
            var afterStart = Reference.End.Value.Add(_offset);
            var afterEnd = afterStart.Add(length);

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public sealed record class DateTimeOverlappingDataSet : BaseOverlappingDataSet<DateTime>
    {
        private readonly TimeSpan _offset;

        public DateTimeOverlappingDataSet(DateTime start, DateTime end, BoundaryType boundaryType, TimeSpan offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start.Value.Add(-_offset);
            var beforeStart = beforeEnd.Add(-length);
            var containsStart = Reference.Start.Value.Add(_offset);
            var containsEnd = Reference.End.Value.Add(-_offset);
            var afterStart = Reference.End.Value.Add(_offset);
            var afterEnd = afterStart.Add(length);

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }

    public sealed record class DateTimeOffsetOverlappingDataSet : BaseOverlappingDataSet<DateTimeOffset>
    {
        private readonly TimeSpan _offset;

        public DateTimeOffsetOverlappingDataSet(DateTimeOffset start, DateTimeOffset end, BoundaryType boundaryType, TimeSpan offset)
            : base(start, end, boundaryType)
        {
            _offset = offset;
            Init();
        }

        protected override void Init()
        {
            var length = Reference.Length().Value;
            var beforeEnd = Reference.Start.Value.Add(-_offset);
            var beforeStart = beforeEnd.Add(-length);
            var containsStart = Reference.Start.Value.Add(_offset);
            var containsEnd = Reference.End.Value.Add(-_offset);
            var afterStart = Reference.End.Value.Add(_offset);
            var afterEnd = afterStart.Add(length);

            Before = Reference with { Start = beforeStart, End = beforeEnd };
            ContainedBy = Reference with { Start = containsStart, End = containsEnd };
            After = Reference with { Start = afterStart, End = afterEnd };
            base.Init();
        }
    }
}
