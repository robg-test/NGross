using mock_assembly;
using NGross.Core.Calculators;
using NGross.Core.Elements;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Elements;

[TestFixture]
public class ThreadGroupTest
{
    [Test]
    public void CreateThreadGroupTest()
    {
        ThreadGroup threadGroup = new(typeof(MockFixture), "ConfigA");

        threadGroup.ThreadGroupConfigurationReference.ShouldBe("ConfigA");
        threadGroup.ThreadGroupName.ShouldBe("mock_assembly.MockFixture");
        threadGroup.Calculator.ShouldBeOfType<ThreadCalculator>();
        threadGroup.ThreadGroupInstance.ShouldBeOfType<MockFixture>();
        threadGroup.Actions.ShouldBeEmpty();
        threadGroup.ThreadGroupContext.Configuration.ShouldNotBeNull();
    }
}