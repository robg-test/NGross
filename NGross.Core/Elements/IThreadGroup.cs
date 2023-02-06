using Microsoft.Extensions.Configuration;
using NGross.Core.Calculators;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public interface IThreadGroup
{
    public IEnumerable<IThreadAction> Actions { get; set; }
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }
    public object? ThreadGroupInstance { get; set; }
    public IConfiguration ThreadGroupConfiguration { get; set; }
    public IThreadingCalculator Calculator { get; set; }
    public string ThreadGroupConfigurationReference { get; set; }
}