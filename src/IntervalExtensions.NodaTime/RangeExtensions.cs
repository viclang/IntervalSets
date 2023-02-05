using NodaTime;

namespace IntervalExtensions.NodaTime
{
    public static class IntervalExtensions
    {
        public static object MinusOneDay<TProperty>(this TProperty value)
            where TProperty : struct => value switch
        {
            Instant x => x.Minus(Duration.StartDays(1)),
            LocalDate x => x.Minus(Period.StartDays(1)),
            LocalDateTime x => x.Minus(Period.StartDays(1)),
            ZonedDateTime x => x.Minus(Duration.StartDays(1)),
            OffsetDate x => x.Date.Minus(Period.StartDays(1)),
            OffsetDateTime x => x.Minus(Duration.StartDays(1)),
            _ => throw new NotSupportedException($"Type {value.GetType()} is not supported")
        };

        public static object PlusOne<TProperty>(this TProperty value)
            where TProperty : struct => value switch
        {
            Instant x => x.Plus(Duration.StartDays(1)),
            LocalDate x => x.Plus(Period.StartDays(1)),
            LocalDateTime x => x.Plus(Period.StartDays(1)),
            ZonedDateTime x => x.Plus(Duration.StartDays(1)),
            OffsetDate x => x.Date.Plus(Period.StartDays(1)),
            OffsetDateTime x => x.Plus(Duration.StartDays(1)),
            _ => throw new NotSupportedException($"Type {value.GetType()} is not supported")
        };
    }
}