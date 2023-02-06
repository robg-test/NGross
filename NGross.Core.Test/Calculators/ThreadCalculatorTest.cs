using Moq;
using NGross.Core.Calculators;
using NGross.Core.Elements;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Calculators;

[TestFixture]
public class ThreadCalculatorTest
{
    private readonly Mock<IThreadGroup> _threadGroupMock = new();
    private readonly ThreadCalculator _calculator;

    public ThreadCalculatorTest()
    {
        _calculator = new ThreadCalculator();
        _threadGroupMock.Setup(c =>
                c.ThreadGroupConfiguration["Ramp-up"])
            .Returns("100");
        _threadGroupMock.Setup(c =>
                c.ThreadGroupConfiguration["Users"])
            .Returns("5");
        _threadGroupMock.Setup(c =>
                c.ThreadGroupConfiguration["Loop"])
            .Returns("5");
    }

    [TestCase(1, 0)]
    [TestCase(2, 25000)]
    [TestCase(3, 50000)]
    [TestCase(4, 75000)]
    [TestCase(5, 100000)]
    public void ShouldCalculateThreads(int thread, int expectedDelay)
    {
        var stats = _calculator.CalculatePacing(_threadGroupMock.Object, 0, thread);
        stats.After.ShouldBe(0);
        stats.Before.ShouldBe(expectedDelay);
    }

    [Test]
    public void ShouldCalculateLoops()
    {
        var loops = _calculator.CalculateLoops(_threadGroupMock.Object);
        loops.ShouldBe(5);
    }

    [Test]
    public void ShouldCalculateThreadGroup()
    {
        var users = _calculator.CalculateThreadCount(_threadGroupMock.Object);
        users.ShouldBe(5);
    }
}