using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Engine.Parser.ThreadAction;

public interface IActionParser
{
    public IEnumerable<IThreadAction> Parse(Type t);
}