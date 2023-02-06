using System.Reflection;
using NGross.Core.Attributes.TestAttributes;
using NGross.Core.Config.Reader;
using NGross.Core.Elements;
using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadGroup;

public class ThreadGroupParser : IThreadGroupParser
{
    public IEnumerable<IThreadGroup>? Parse(Assembly? assembly)
    {
        return assembly?.GetTypes().Where(type => type.IsDefined(typeof(ThreadGroupAttribute)))
            //TODO - Use some form of service library for the config reader.
            .Select(threadGroupType => new Elements.ThreadGroup(threadGroupType, 
                threadGroupType.GetCustomAttribute<ThreadGroupAttribute>()?.ConfigReference ?? string.Empty));
    }
}