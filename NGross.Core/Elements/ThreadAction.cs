using System.Reflection;
using NGross.Core.Context;
using NGross.Core.Measurers;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadAction : IThreadAction
{
    public ThreadAction(MethodInfo methodInfo, IMeasurer measurer)
    {
        this.MethodInfo = methodInfo;
    }

    public MethodInfo MethodInfo { get; set; }
    public IMeasurer Measurer { get; set; }
}