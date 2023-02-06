namespace NGross.Core.Attributes.TestAttributes;

/// <summary>
/// Defines a thread group, controlling a set of actions with a thread throughput configuration.
/// </summary>
public class ThreadGroupAttribute : NGrossBaseAttribute
{
    public string ConfigReference;
    public ThreadGroupAttribute(string configReference)
    {
        this.ConfigReference = configReference;
    }
}