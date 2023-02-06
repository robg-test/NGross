using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace NGross.Core.Config.Reader;

public static class NGrossConfigManager
{
    private static readonly ConfigurationBuilder Builder = new();
    private static readonly List<string> ParsedConfigs = new();

    public static IConfiguration? Configuration { get; set; }
    
    /// <summary>
    /// Reads Configuration from the provided filepath, will attempt to read sub-configurations
    /// </summary>
    /// <param name="path">The path to the configuration file</param>
    /// <returns>A configuration object containing the parsed configurations</returns>
    static NGrossConfigManager()
    {
        Configuration = Read("ngross_config.json");
    }

    private static IConfigurationRoot Read(string path)
    {
        Builder.AddJsonFile(path);
        var rootConfig = Builder.Build();
        ReadSubConfigs(Builder, rootConfig);
        return Builder.Build();
    }

    private static void ReadSubConfigs(IConfigurationBuilder builder, IConfiguration config)
    {
        foreach (var configsEntry in config.GetSection("Configs")
                     .GetChildren()
                     .AsEnumerable())
        {
            var configFile = configsEntry.Value!;
            
            //TODO logging here, dependent on the outcome of parsing the file
            if (!File.Exists(configFile) || ParsedConfigs.Contains(configFile)) continue;
            
            ParsedConfigs.Add(configFile);
            Read(configsEntry.Value!);
        }
    }
}