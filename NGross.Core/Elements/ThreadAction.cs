using System.Reflection;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadAction : IThreadAction
{
    public ThreadAction(MethodInfo methodInfo)
    {
        this.MethodInfo = methodInfo;
    }

    public MethodInfo MethodInfo { get; set; }
}