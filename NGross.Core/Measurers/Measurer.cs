using System.Diagnostics;
using NGross.Core.Measurers.Statistics;

namespace NGross.Core.Measurers;

public class Measurer : IMeasurer
{
    private Stopwatch _stopwatch;

    public void StartMeasure()
    {
        this._stopwatch = new Stopwatch();
        this._stopwatch.Start();
    }

    public IStatistic EndMeasure()
    {
        _stopwatch.Stop();
        return new Statistic
        {
            Milliseconds = _stopwatch.ElapsedMilliseconds,
            Success = true
        };
    }
    
    public IStatistic EndMeasure(Exception e)
    {
        _stopwatch.Stop();
        return new Statistic
        {
            Milliseconds = _stopwatch.ElapsedMilliseconds,
            Success = false,
            InternalException = e
        };
    }
}