using System.IO;
using NGross.Core.Engine.Loader;
using NUnit.Framework;
using Shouldly;

namespace NGross.Core.Test.Loader;

[TestFixture]
public class AssemblyLoaderTest
{
    [Test]
    public void LoadAssemblyTest()
    {
        var assemblyLoader = new AssemblyLoader();
        
        var pathToTest = Path.Combine(TestContext.CurrentContext.TestDirectory, "mock-assembly.dll");
        var loadedAssembly = assemblyLoader.LoadFromAssembly(pathToTest);

        var assembly = assemblyLoader.LoadFromAssembly(pathToTest);
        assembly.ToString().ShouldBe("mock-assembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
    }
}