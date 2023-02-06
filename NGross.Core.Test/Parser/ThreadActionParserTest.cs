using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using mock_assembly;
using Moq;
using NGross.Core.Config.Reader;
using NGross.Core.Context;
using NGross.Core.Elements;
using NGross.Core.Engine.Parser;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Models;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Parser;

[TestFixture]
public class ActionParserTest
{
    private readonly ThreadGroup _threadGroup = new(typeof(MockFixture), "ConfigA");
    private readonly ActionParser _parser = new();
    
    [Test]
    public void ParseActionSuccessfullyTest()
    {
        var action = this._parser.Parse(this._threadGroup.InnerTestType);
        action.ToList().Select(c => c.MethodInfo.Name).FirstOrDefault().ShouldBe("Execute");
    }
}