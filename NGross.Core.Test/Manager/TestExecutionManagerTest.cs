using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using mock_assembly;
using Moq;
using NGross.Core.Calculators;
using NGross.Core.Context;
using NGross.Core.Elements;
using NGross.Core.Logging;
using NGross.Core.Manager;
using NGross.Core.Models;
using NGross.Core.Plan;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Manager;

[TestFixture]
public class TestExecutionManagerTest
{
    private readonly Mock<ITest> _mockTest = new();
    private readonly Mock<IThreadGroup> _mockThreadGroup = new();
    private readonly Mock<IThreadAction> _mockThreadAction = new();
    private readonly Mock<IThreadingCalculator> _mockThreadCalculator = new();
    private readonly Mock<IConfiguration> _mockConfiguration = new();
    private readonly Mock<IConfigurationSection> _mockConfigurationSection = new();
    
    private MethodInfo? _providedMethodInfo;
    private MockFixture _mockTestClass;
    
    [SetUp]
    public void SetupMocks()
    {
        SetupConfigurationMocks();
        SetupThreadGroupMock();
        SetupThreadCalculatorMock();
        SetupThreadTestMock();
    }

    private void SetupConfigurationMocks()
    {
        _mockConfiguration.Setup(c => c.GetSection("ThreadGroupConfig").GetChildren())
            .Returns(new List<IConfigurationSection>() { _mockConfigurationSection.Object });
        _mockConfigurationSection.Setup(c => c.Key).Returns("A");
    }

    private void SetupThreadTestMock()
    {
        _mockTest.Setup(c => c.ThreadGroups).Returns(new List<IThreadGroup>()
        {
            _mockThreadGroup.Object
        });
    }

    private void SetupThreadCalculatorMock()
    {
        _mockThreadCalculator.Setup(c => c.CalculateThreadCount(It.IsAny<IThreadGroup>())).Returns(1);
    }

    private void SetupThreadActionMock()
    {
        _mockThreadAction.Setup(c => c.MethodInfo).Returns(_providedMethodInfo!);
    }

    private void SetupThreadGroupMock()
    {
        _mockTestClass = new MockFixture();
        var method = _mockTestClass.Execute;
        _providedMethodInfo = method.Method;

        _mockThreadGroup.Setup(c => c.ThreadGroupConfigurationReference).Returns("A");
        _mockThreadGroup.Setup(c => c.ThreadGroupConfiguration).Returns(_mockConfiguration.Object);
        _mockThreadGroup.Setup(c => c.Actions).Returns(new List<IThreadAction>()
        {
            _mockThreadAction.Object
        });
        _mockThreadGroup.Setup(c => c.Calculator).Returns(_mockThreadCalculator.Object);
        _mockThreadGroup.Setup(c => c.ThreadGroupInstance).Returns(_mockTestClass);
        _mockThreadGroup.Setup(c => c.ThreadGroupContext).Returns(new ThreadGroupContext(_mockConfiguration.Object));
    }

    [Test]
    public async Task StartExecutionManager()
    {
        SetupThreadActionMock();
        var manager = new TestExecutionManager(
            new NGrossLogger(),
            _mockTest.Object);
        
        await manager.Execute();
    }

    [Test]
    public async Task StopExecutionManager()
    {
        SetupThreadActionMock();
        var manager = new TestExecutionManager(
            new NGrossLogger(),
            _mockTest.Object);
        
        await manager.Execute();
        manager.Stop();
    }

    [Test]
    public async Task UnsupportedParameterInMethod()
    {
        _mockTestClass = new MockFixture();
        var method = _mockTestClass.FaultExecute;
        _providedMethodInfo = method.Method;
        SetupThreadActionMock();
        var manager = new TestExecutionManager(
            new NGrossLogger(),
            _mockTest.Object);
        
        await manager.Execute().ShouldThrowAsync<Exception>();
    }
}