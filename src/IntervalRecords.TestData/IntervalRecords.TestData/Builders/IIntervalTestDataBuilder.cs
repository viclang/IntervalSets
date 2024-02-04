namespace IntervalRecords.TestData.Builders;

public interface IIntervalTestDataBuilder
{
    IIntervalTestDataBuilder WithAfter();
    IIntervalTestDataBuilder WithBefore();
    IIntervalTestDataBuilder WithContainedBy();
    IIntervalTestDataBuilder WithContains();
    IIntervalTestDataBuilder WithEqual();
    IIntervalTestDataBuilder WithFinishedBy();
    IIntervalTestDataBuilder WithFinishes();
    IIntervalTestDataBuilder WithLeftBoundedEqual();
    IIntervalTestDataBuilder WithLeftBoundedFinishedBy();
    IIntervalTestDataBuilder WithLeftboundedFinishes();
    IIntervalTestDataBuilder WithMeets();
    IIntervalTestDataBuilder WithMetBy();
    IIntervalTestDataBuilder WithOverlappedBy();
    IIntervalTestDataBuilder WithOverlaps();
    IIntervalTestDataBuilder WithRightBoundedEqual();
    IIntervalTestDataBuilder WithRightboundedStartedBy();
    IIntervalTestDataBuilder WithRightboundedStarts();
    IIntervalTestDataBuilder WithStartedBy();
    IIntervalTestDataBuilder WithStarts();
    IIntervalTestDataBuilder WithUnBoundedEqual();
}