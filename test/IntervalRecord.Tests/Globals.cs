global using Xunit;
global using FluentAssertions;
using System;

namespace IntervalRecord.Tests
{
    public static class Globals
    {
        public const int StartInt = 6;
        public const int EndInt = 10;
        public static readonly DateOnly StartDateOnly = new DateOnly(2022, 7, 30);
        public static readonly DateOnly EndDateOnly = StartDateOnly.AddDays(4);
        private static readonly DateTime StartDateTime = new DateTime(2022, 7, 30);
        private static readonly DateTime EndDateTime = StartDateTime.AddDays(4);
        private static readonly DateTimeOffset StartDateTimeOffset = new DateTimeOffset(new DateTime(2022, 7, 30), TimeSpan.Zero);
        private static readonly DateTimeOffset EndDateTimeOffset = StartDateTimeOffset.AddDays(4);

        public const int OffsetInt = 1;
        public static readonly TimeSpan OffsetTimeSpan = TimeSpan.FromDays(1);

    }
}
