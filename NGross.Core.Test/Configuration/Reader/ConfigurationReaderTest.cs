using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

using NGross.Core.Config.Reader;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Configuration.Reader;

[TestFixture]
public class ConfigurationReader
{
    //TODO - Somehow add more tests to this?
    [Test]
    public void CanReadConfig()
    {
        NGrossConfigManager.Configuration?["ThreadGroupConfig:ConfigA:Users"].ShouldBe("3");
        NGrossConfigManager.Configuration?["ThreadGroupConfig:ConfigB:Users"].ShouldBe("1");
        NGrossConfigManager.Configuration?["ThreadGroupConfig:ConfigC:Users"].ShouldBe("5");
    }
}

