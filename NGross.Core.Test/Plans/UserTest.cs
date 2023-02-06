using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using NGross.Core.Elements;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Models;
using NGross.Core.Plan;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Plans;

[TestFixture]
public class UserTest
{
    [Test]
    public void TestExecuteAction()
    {
        AssemblyLoader loader = new AssemblyLoader();
        var assembly = loader.LoadFromAssembly("mock-assembly.dll");
        var threadGroupsParser = new ThreadGroupParser();
        var groups = threadGroupsParser.Parse(assembly);

        var test = new Plan.Test(groups);
        test.ThreadGroups?.ToList().ShouldNotBeEmpty();
    }
}