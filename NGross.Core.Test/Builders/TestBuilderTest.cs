using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using NGross.Core.Builders;
using NGross.Core.Elements;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Models;
using NUnit.Framework;

namespace NGross.Core.Test.Builders;

[TestFixture]
public class TestBuilderTest
{
    private readonly Mock<IThreadGroupParser> _parser = new();
    private readonly IAssemblyLoader _loader = new AssemblyLoader();
    private readonly Mock<IActionParser> _actionParser = new();
    private TestBuilder _builder;

    public TestBuilderTest()
    {
        _builder = new TestBuilder("mock-assembly", 
            _loader, 
            _parser.Object, 
            _actionParser.Object);
    }

    [Test]
    public void ShouldPerformABuild()
    {
        _parser.Setup(c => c.Parse(It.IsAny<Assembly?>()))
            .Returns(new List<IThreadGroup>()
        {
            new Mock<IThreadGroup>().Object,
        });
        _actionParser.Setup(d => d.Parse(It.IsAny<Type>()))
            .Returns(new List<IThreadAction>()
            {
                new Mock<IThreadAction>().Object
            });
        _builder.Build();
    }
}