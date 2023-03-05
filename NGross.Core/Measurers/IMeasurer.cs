using NGross.Core.Measurers.Statistics;

namespace NGross.Core.Measurers;

public interface IMeasurer
{
    public void StartMeasure();

    public IStatistic EndMeasure();
}