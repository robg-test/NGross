using System.Reflection;

namespace NGross.Core.Engine.Loader;

public interface IAssemblyLoader
{
    public Assembly? LoadFromAssembly(string assembly);
}