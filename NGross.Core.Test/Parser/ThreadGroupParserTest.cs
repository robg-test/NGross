using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NGross.Core.Elements;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Models;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Parser;

[TestFixture]
public class ProjectParserTest
{
    private Assembly? _mockAssembly;
    
    [SetUp]
    public void LoadMockAssembly()
    {
        var assemblyLoader = new AssemblyLoader();
        var pathToTest = Path.Combine(TestContext.CurrentContext.TestDirectory, "mock-assembly.dll");
        _mockAssembly = assemblyLoader.LoadFromAssembly(pathToTest);
    }
    
    [Test]
    public void ParsesThreadGroup()
    {
        var groupParser = new ThreadGroupParser();
        var threadGroups = groupParser.Parse(_mockAssembly);
        threadGroups.ToList().Count.ShouldBeGreaterThan(0);
    }
}