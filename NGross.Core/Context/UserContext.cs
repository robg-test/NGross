using BoDi;

namespace NGross.Core.Context;

public class UserContext
{
    public UserContext()
    {
        this.UserContainer = new ObjectContainer();
    }
    public int UserIdentifier { get; set; }
    public int UserIteration { get; set; }
    public ObjectContainer UserContainer { get;        }
}