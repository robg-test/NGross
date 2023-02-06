using System.Reflection;
using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadGroup;

public interface IThreadGroupParser
{
    public IEnumerable<IThreadGroup>? Parse(Assembly? assembly);
}