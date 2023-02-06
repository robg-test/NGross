using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Plan;

/// <summary>
/// Denotes a User (Aka a Thread) with a set of actions to perform, a wait time to start and a count of loops
/// </summary>
public class Test : ITest
{
    public Test(IEnumerable<IThreadGroup>? threadGroups)
    {
        ThreadGroups = threadGroups;
    }

    public IEnumerable<IThreadGroup>? ThreadGroups { get; set; }
}

public interface ITest
{
    public IEnumerable<IThreadGroup>? ThreadGroups { get; set; }

}