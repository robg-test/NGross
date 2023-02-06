using System.Reflection;

namespace NGross.Core.Engine.Loader;

public class AssemblyLoader : IAssemblyLoader
{
    public Assembly? LoadFromAssembly(string assembly)
    {
        return Load(assembly);
    }

    private static Assembly? Load(string assembly) => 
        Assembly.LoadFrom(assembly);
}