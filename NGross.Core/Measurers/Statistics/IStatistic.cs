namespace NGross.Core.Measurers.Statistics;

public interface IStatistic
{
    public long Milliseconds { get; set; }
    public Exception InternalException { get; set; }
    public bool Success { get; set; }
}