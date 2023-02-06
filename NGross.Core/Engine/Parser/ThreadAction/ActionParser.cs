using NGross.Core.Attributes.TestAttributes;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadAction;

public class ActionParser : IActionParser
{
    public IEnumerable<IThreadAction> Parse(Type type)
    {
        IEnumerable<IThreadAction> types = type.GetMethods()
            .Where(m => m.GetCustomAttributes(typeof(ActionAttribute), true).Length > 0)
            .Select(c => new Elements.ThreadAction(c));
        return types;
    }
}