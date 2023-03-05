namespace NGross.Core.Measurers.Statistics;

public class Statistic : IStatistic
{
    public long Milliseconds { get; set; }
    public Exception InternalException { get; set; }
    public bool Success { get; set; }
}