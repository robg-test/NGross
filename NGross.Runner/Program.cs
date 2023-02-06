// See https://aka.ms/new-console-template for more information

using System.Reflection;
using NGross.Core.Builders;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using NGross.Core.Plan;
using System.CommandLine;
using Figgle;
using mock_assembly;
using NGross.Core.Calculators;
using NGross.Core.Logging;
using NGross.Core.Manager;

var cmd = new RootCommand();
var path = new Argument<string>("path");
var startCommand = new Command("start", "Execute a Test")
{
    path
};

Console.ForegroundColor = ConsoleColor.Green;

cmd.AddCommand(startCommand);

startCommand.SetHandler(async (pathValue) =>
{
    Console.WriteLine(
        FiggleFonts.Standard.Render("NGross") + "v0.0.1");
    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine($"Starting new test run on file: {pathValue}...");
    Console.WriteLine("Loading Tests...");
    var builder = new TestBuilder(pathValue, 
        new AssemblyLoader(), 
        new ThreadGroupParser(), 
        new ActionParser());
    var test = builder.Build();
    Console.WriteLine("Tests Loaded");

    TestExecutionManager manager = new TestExecutionManager(new NGrossLogger(), test);
    await manager.Execute();
    Console.ForegroundColor = ConsoleColor.White;

}, path);

await cmd.InvokeAsync(args);