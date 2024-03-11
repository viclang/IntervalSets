namespace IntervalRecords.TestData.Builders;
public static class IntervalTestDataDirector
{
    public static void WithOverlapping(IIntervalTestDataBuilder builder, IntervalType intervalType)
    {
        if (intervalType is IntervalType.Closed)
        {
            builder.WithMeets();
            builder.WithMetBy();
        }
        builder.WithOverlaps();
        builder.WithStarts();
        builder.WithContainedBy();
        builder.WithFinishes();
        builder.WithEqual();
        builder.WithFinishedBy();
        builder.WithContains();
        builder.WithStartedBy();
        builder.WithOverlappedBy();

        WithUnBoundedIntervals(builder);
    }

    public static void WithNonOverlapping(IIntervalTestDataBuilder builder, IntervalType intervalType)
    {
        if (intervalType is not IntervalType.Closed)
        {
            builder.WithMeets();
            builder.WithMetBy();
        }
        builder.WithBefore();
        builder.WithAfter();
    }

    public static void WithConnected(IIntervalTestDataBuilder builder, IntervalType intervalType)
    {
        if (intervalType is not IntervalType.Open)
        {
            builder.WithMeets();
            builder.WithMetBy();
        }
        builder.WithOverlaps();
        builder.WithStarts();
        builder.WithContainedBy();
        builder.WithFinishes();
        builder.WithEqual();
        builder.WithFinishedBy();
        builder.WithContains();
        builder.WithStartedBy();
        builder.WithOverlappedBy();

        WithUnBoundedIntervals(builder);
    }

    public static void WithDisjoint(IIntervalTestDataBuilder builder, IntervalType intervalType)
    {
        if (intervalType is IntervalType.Open)
        {
            builder.WithMeets();
            builder.WithMetBy();
        }
        builder.WithBefore();
        builder.WithAfter();
    }

    public static void WithBoundedIntervals(IIntervalTestDataBuilder builder)
    {
        builder.WithBefore();
        builder.WithMeets();
        builder.WithOverlaps();
        builder.WithStarts();
        builder.WithContainedBy();
        builder.WithFinishes();
        builder.WithEqual();
        builder.WithFinishedBy();
        builder.WithContains();
        builder.WithStartedBy();
        builder.WithOverlappedBy();
        builder.WithMetBy();
        builder.WithAfter();
    }

    public static void WithUnBoundedIntervals(IIntervalTestDataBuilder builder)
    {
        builder.WithLeftboundedFinishes();
        builder.WithLeftBoundedEqual();
        builder.WithLeftBoundedFinishedBy();
        builder.WithRightboundedStarts();
        builder.WithRightBoundedEqual();
        builder.WithRightboundedStartedBy();
        builder.WithUnBoundedEqual();
    }
}
