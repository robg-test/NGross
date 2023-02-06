using System.Diagnostics;
using NGross.Core.Results;

namespace NGross.Core.Runners;

public class ActionRunner : IActionRunner
{
    private readonly Task _taskToExecute;
    private readonly Stopwatch _stopwatch;
    public ActionRunner(Task taskToExecute)
    {
        this._taskToExecute = taskToExecute;
        this._stopwatch = new Stopwatch();
    }
    public async Task<IActionResult> Run()
    {
        this._stopwatch.Start();
        try
        {
            await _taskToExecute;
            return new ActionResult(this._stopwatch.ElapsedMilliseconds, null!);
        }
        catch (Exception e)
        {;
            return new ActionResult(this._stopwatch.ElapsedMilliseconds, e);
        }
    }
}