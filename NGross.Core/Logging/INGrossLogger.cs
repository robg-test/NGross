namespace NGross.Core.Logging;

public interface INGrossLogger
{
    void Log(string message);
    void LogStatistic(int result, string name);
    void Dump();
}

public class NGrossLogger : INGrossLogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }

    public void LogStatistic(int result, string name)
    {
        Console.WriteLine(result + name);
    }

    public void Dump()
    {
    }
}