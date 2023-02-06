using NGross.Core.Elements;
using NGross.Core.Manager;

namespace NGross.Core.Calculators;

public interface IThreadingCalculator
{
    TestExecutionManager.PacingStats
        CalculatePacing(IThreadGroup threadGroup,
            int currentIteration,
            int currentThread);


    int CalculateLoops(IThreadGroup threadGroup);

    int CalculateThreadCount(IThreadGroup threadGroup);
}