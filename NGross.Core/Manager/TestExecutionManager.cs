using System.Reflection;
using NGross.Core.Context;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Plan;
using NGross.Core.Sleeper;

namespace NGross.Core.Manager;

public class TestExecutionManager : ITestExecutionManager
{
    private readonly List<Task> _threadGroupThreads;
    private INGrossLogger _logger;
    private ISleeper _sleeper;

    public TestExecutionManager(INGrossLogger logger, ITest test)
    {
        _threadGroupThreads = new List<Task>();
        this._logger = logger;
        this.Test = test;
        this._sleeper = new Sleeper.Sleeper();
        foreach (var testThreadGroup in this.Test.ThreadGroups!)
        {
            var config = testThreadGroup.ThreadGroupContext.Configuration;
            var threadConfig = config.GetSection("ThreadGroupConfig")
                .GetChildren().SingleOrDefault(c =>  c.Key == 
                                                     testThreadGroup.ThreadGroupConfigurationReference)!;

            testThreadGroup.ThreadGroupConfiguration = threadConfig;
        }
    }

    private ITest Test { get; set; }

    public async Task Execute()
    {
        foreach (var testThreadGroup in this.Test.ThreadGroups!)
        {
            var threadGroupCount = 1;
            var userCount = testThreadGroup.Calculator.CalculateThreadCount(testThreadGroup);
            for (var i = 0; i < userCount; i++)
            {
                var userContext = new UserContext();
                var currentThread = threadGroupCount;
                userContext.UserIdentifier = currentThread;
                var threadGroupTask =
                    Task.Run(async () =>
                    {
                        var iterations = testThreadGroup.Calculator.CalculateLoops(testThreadGroup);
                        for (var currentIteration = 0; currentIteration <= iterations; currentIteration++)
                        {
                            userContext.UserIteration = currentIteration + 1;
                            await RunThreadGroup(testThreadGroup, userContext,
                                testThreadGroup.Calculator.CalculatePacing(testThreadGroup, currentIteration, currentThread));
                        }
                    });
                _threadGroupThreads.Add(threadGroupTask);
                threadGroupCount++;
            }
        }

        await Task.WhenAll(_threadGroupThreads.ToArray());
    }

    private static object GetParameterObject(ParameterInfo specificParam, IEnumerable<object> context)
    {
        if (specificParam.ParameterType == typeof(UserContext))
        {
            return context.OfType<UserContext>().Single();
        }

        if (specificParam.ParameterType == typeof(ThreadGroupContext))
        {
            return context.OfType<ThreadGroupContext>().Single();
        }
        throw new Exception($"Unsupported Parameter Detected {specificParam.Name}");
    }

    private async Task RunThreadGroup(IThreadGroup threadGroup, UserContext userContext, PacingStats pacingController)
    {
        _sleeper.Sleep(pacingController.Before);
        foreach (var threadAction in threadGroup.Actions)
        {
            var parameters = threadAction.MethodInfo.GetParameters();
            var list = new List<object>();

            foreach (var p in parameters)
            {
                list.Add(
                    GetParameterObject(p, new List<object>()
                    {
                        userContext,
                        threadGroup.ThreadGroupContext
                    })
                );
            }

            await (Task)threadAction.MethodInfo.Invoke(threadGroup.ThreadGroupInstance,
                list.ToArray())!;
        }

        _sleeper.Sleep(pacingController.After);
    }

    public struct PacingStats
    {
        public int Before;
        public int After;
    }

    public void Stop()
    {
        _threadGroupThreads.ForEach(c => c.Dispose());
    }
}