using NGross.Core.Logging;

namespace NGross.Core.Manager;

public interface ITestExecutionManager
{
    Task Execute();
    void Stop();
}