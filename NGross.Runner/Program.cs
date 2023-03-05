// See https://aka.ms/new-console-template for more information

using NGross.Core.Builders;
using NGross.Core.Engine.Loader;
using NGross.Core.Engine.Parser.ThreadAction;
using NGross.Core.Engine.Parser.ThreadGroup;
using System.CommandLine;
using NGross.Core.Logging;
using NGross.Core.Manager;
using NGross.Core.Plan;
using Spectre.Console;


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
    AnsiConsole.Status()
        .Start("Preparing Tests!", ctx =>
        {
            AnsiConsole.MarkupLine("Loading tests");
            ctx.Spinner(Spinner.Known.Arrow3);
            var builder = new TestBuilder(pathValue,
                new AssemblyLoader(),
                new ThreadGroupParser(),
                new ActionParser());
            _test = builder.Build();

            Thread.Sleep(1000);

            AnsiConsole.MarkupLine("Creating Execution Manager");
            _testExecutionManager = new TestExecutionManager(new NGrossLogger(), _test);

            ctx.Status("Tests ready - Starting!");
            ctx.SpinnerStyle(Style.Parse("Green"));
            ctx.Refresh();
        });


    await AnsiConsole.Progress().HideCompleted(false)
        .AutoRefresh(true)
        .Columns(new ProgressColumn[]
        {
            new TaskDescriptionColumn(), // Task description
            new ProgressBarColumn(), // Progress bar
            new PercentageColumn(), // Percentage
            new ElapsedTimeColumn(),
            new SpinnerColumn(), // Spinner
        })
        .StartAsync(async ctx =>
        {
            foreach (var threadGroup in _test!.ThreadGroups!)
            {
                var task = ctx.AddTask(
                    $"[green]{threadGroup.ThreadGroupName}[/]");
                _taskDict!.Add(threadGroup.ThreadGroupName, task);
                task.StartTask();
            }

            await _testExecutionManager!.Execute();
            _taskDict!.ToList().ForEach(c => c.Value.StopTask());
        });
}, path);

_taskDict = new Dictionary<string, ProgressTask>();

await cmd.InvokeAsync(args);


public partial class Program
{
    private static ITest? _test;
    private static ITestExecutionManager? _testExecutionManager;
    private static Dictionary<string, ProgressTask>? _taskDict;
}