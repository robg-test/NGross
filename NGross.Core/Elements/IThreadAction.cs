using System.Reflection;
using NGross.Core.Measurers;

namespace NGross.Core.Models;

public interface IThreadAction
{
    public MethodInfo MethodInfo { get; set; }
    
    public IMeasurer Measurer { get; set; }
}