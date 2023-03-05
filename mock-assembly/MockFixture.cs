using System.Reflection.Metadata;
using BoDi;
using NGross.Core.Attributes.TestAttributes;
using NGross.Core.Context;

namespace mock_assembly;


/// <summary>
/// A Thread Group denotes a list of tasks to run on a group of threads
/// Controlled by the config passed in read from the config files
///
/// Setup allows you to 
/// </summary>
[ThreadGroup("ConfigA")]
public class MockFixture
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="tgContext">The context belonging to the ThreadGroup /\ See above. All threads share this context.</param>
    /// <param name="userContext">The context belonging to a User (Thread). Each Thread has it's own context</param>
    [Action]
    public void Execute(UserContext userContext, ThreadGroupContext tgContext)
    {
        Console.Write("Hello!");
    }

    public async Task FaultExecute(string context)
    {
    }      
}

