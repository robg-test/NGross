using BoDi;
using Microsoft.Extensions.Configuration;

namespace NGross.Core.Context;

public class ThreadGroupContext
{
    public ThreadGroupContext(IConfiguration configuration)
    {
        this.Configuration = configuration;
        this.ThreadGroupContainer = new ObjectContainer();
    }
    public IConfiguration Configuration { get; }
    public IObjectContainer ThreadGroupContainer;
}