namespace IntervalRecords.Experiment.Endpoints;
public enum EndpointType : byte
{
    /// <summary>
    /// Lower is Greater than Upper if values are equal ("[1" > "1]")
    /// </summary>
    Lower = 1,
    /// <summary>
    /// Upper is Less than Lower if values are equal ("1]" < "[1").
    /// </summary>
    Upper = 0,
}
