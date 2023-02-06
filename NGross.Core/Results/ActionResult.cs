using NGross.Core.Runners;

namespace NGross.Core.Results;

public class ActionResult : IActionResult
{
    public ActionResult(long stopwatchElapsedMilliseconds, Exception exception)
    {
        this.Milliseconds = stopwatchElapsedMilliseconds;
        this.InternalException = exception;
    }

    public long Milliseconds { get; set; }
    public Exception? InternalException { get; set; }
}