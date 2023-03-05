using Microsoft.Extensions.Configuration;
using NGross.Core.Calculators;
using NGross.Core.Config.Reader;
using NGross.Core.Context;
using NGross.Core.Models;

namespace NGross.Core.Elements;

public class ThreadGroup : IThreadGroup
{
    public Type InnerTestType { get; set; }
    public ThreadGroupContext ThreadGroupContext { get; set; }
    
    public string? ThreadGroupName { get; set; }

    public ThreadGroup(Type assemblyType, string config)
    {
        this.ThreadGroupConfigurationReference = config;
        this.InnerTestType = assemblyType;
        this.ThreadGroupName = assemblyType.FullName;
        this.ThreadGroupInstance = Activator.CreateInstance(assemblyType);
        if (NGrossConfigManager.Configuration != null)
            this.ThreadGroupContext = new ThreadGroupContext(NGrossConfigManager.Configuration);
        else
        {
            throw new FileNotFoundException("No configuration file provided - Are you missing a ngross_config.json file?");
        }
        Actions = new List<IThreadAction>();
        this.Calculator = new ThreadCalculator();
    }

    public IConfiguration ThreadGroupConfiguration { get; set; }
    public IThreadingCalculator Calculator { get; set; }

    public object? ThreadGroupInstance { get; set; }
    public string ThreadGroupConfigurationReference { get; set; }

    public IEnumerable<IThreadAction> Actions { get; set; }
}