using System.Reflection;

namespace NGross.Core.Models;

public interface IThreadAction
{
    public MethodInfo MethodInfo { get; set; }
}