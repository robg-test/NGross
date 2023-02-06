using Microsoft.Extensions.Configuration;
using NGross.Core.Elements;
using NGross.Core.Manager;

namespace NGross.Core.Calculators;

//TODO - Handle Mathematical Mishaps
public class ThreadCalculator : IThreadingCalculator
{
    public TestExecutionManager.PacingStats
        CalculatePacing(IThreadGroup threadGroup, 
            int currentIteration, 
            int currentThread)
    {
        var configuration = threadGroup.ThreadGroupConfiguration;
        var rampUp = Convert.ToInt32(configuration["Ramp-up"]);
        var users = Convert.ToInt32(configuration["Users"]);

        if (currentThread > 1 && rampUp > 0 && currentIteration==0)
        {
            var delaySegment = rampUp * 1000 / (users - 1);
            var delay = (currentThread - 1) * delaySegment;
            
            return new TestExecutionManager.PacingStats()
            {
                After = 0,
                Before = delay
            };
        }

        return new TestExecutionManager.PacingStats()
        {
            After = 0,
            Before = 0
        };
    }
    
    public int CalculateLoops(IThreadGroup threadGroup)
    {
        var configuration = threadGroup.ThreadGroupConfiguration;
        return Convert.ToInt32(configuration["Loop"]);
    }

    public int CalculateThreadCount(IThreadGroup threadGroup)
    {
        var configuration = threadGroup.ThreadGroupConfiguration;
        return Convert.ToInt32(configuration["Users"]);
    }
}
