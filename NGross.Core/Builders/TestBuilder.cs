using System.Runtime.CompilerServices;
using NGross.Core.Elements;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Plan;

namespace NGross.Core.Builders;

public class TestBuilder : ITestBuilder
{
    private readonly string _assemblyName;
    private IAssemblyLoader _assemblyLoader;
    private IThreadGroupParser _threadGroupParser;
    private IActionParser _actionParse;
    
    public TestBuilder(string assemblyName, 
        IAssemblyLoader assemblyLoader, 
        IThreadGroupParser threadGroupParser, 
        IActionParser actionParse)
    {
        _assemblyName = assemblyName;
        _assemblyLoader = assemblyLoader;
        _threadGroupParser = threadGroupParser;
        _actionParse = actionParse;
    }
    
    public ITest Build()
    {
        var loader = new AssemblyLoader();
        var assembly = loader.LoadFromAssembly($"{_assemblyName}");

        var groupParser = new ThreadGroupParser();
        var groups = groupParser.Parse(assembly)!.ToList();

        foreach (var threadGroup in groups)
        {
            var actionParser = new ActionParser();
            threadGroup.Actions = actionParser.Parse(threadGroup.InnerTestType);
        }

        return new Test(groups);
    }
}