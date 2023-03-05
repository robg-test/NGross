using System.Reflection;
using NGross.Core.Context;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Measurers;
using NGross.Core.Plan;
using NGross.Core.Sleeper;

namespace NGross.Core.Manager;

public class TestExecutionManager : ITestExecutionManager
{
    private readonly List<Task> _threadGroupThreads;
    private INGrossLogger _logger;
    private ISleeper _sleeper;
    private IMeasurer _measurer;

    public TestExecutionManager(INGrossLogger logger, ITest? test)
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

    private ITest? Test { get; set; }

    //TODO - Delegate the execution of a Thread Group to the Thread Group class
    //It shouldn't be here
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

    //TODO Refactor this into the ThreadGroup Class
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
            var isAwaitable = threadAction.MethodInfo.ReturnType.GetMethod(nameof(Task.GetAwaiter)) != null;

            if (isAwaitable)
            {
                await (Task)threadAction.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, list.ToArray())!;
                threadAction.Measurer.EndMeasure();
            }
            threadAction.MethodInfo.Invoke(threadGroup.ThreadGroupInstance, list.ToArray());
        }

        //TODO This should sleep after the thread has executed not at the end of this method.
        //The sleep should start earlier, and this line should await it.
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