namespace NGross.Core.Results;

public interface IActionResult
{
    public long Milliseconds { get; set; }
    public Exception? InternalException { get; set; }
}