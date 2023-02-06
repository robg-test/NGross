namespace NGross.Core.Sleeper;

public class Sleeper : ISleeper
{
    public void Sleep(int duration)
    {
        Thread.Sleep(duration);
    }
}