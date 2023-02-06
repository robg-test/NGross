using System.Diagnostics;
using NGross.Core.Results;

namespace NGross.Core.Runners;

public interface IActionRunner
{
    public Task<IActionResult> Run();
}