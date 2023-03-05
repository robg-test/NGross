using NGross.Core.Attributes.TestAttributes;

namespace mock_assembly;

[ThreadGroup("ConfigB")]
public class SecondFixture
{
    public async Task FaultExecute(string context)
    {
    }
}