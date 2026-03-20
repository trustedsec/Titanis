using System.ComponentModel.Design;
using Titanis.Cli;
using Titanis.Mocks;

namespace Titanis;

public abstract class CliCommandTest<TCommand>
	where TCommand : Command, new()
{
	protected MockRepository mocks;
	protected ServiceContainer hostServices;
	protected TestFileAccess fileAccess;

	[TestInitialize]
	public void InitializeCommandTest()
	{
		MockRepository mocks = new MockRepository();
		this.mocks = mocks;

		// Host services
		ServiceContainer services = new ServiceContainer();
		this.hostServices = services;

		this.InitializeHostServices(services);
	}

	protected virtual void InitializeHostServices(ServiceContainer hostServices)
	{
		TestFileAccess fileAccess = this.CreateTestFileAccess();
		this.fileAccess = fileAccess;
		hostServices.AddService(typeof(IFileAccess), fileAccess);
	}

	protected TestFileAccess CreateTestFileAccess()
	{
		return new TestFileAccess(this.GetType().Assembly, this.GetType().Namespace);
	}
	public TestContext TestContext { get; set; }

	protected Task<object[]> TestCommand(string args)
	{
		return this.TestCommand(CommandLineParser.Tokenize(args));
	}
	protected async Task<object[]> TestCommand(Token[] args)
	{
		var cmd = new TCommand();
		const OutputStyle outputStyle = OutputStyle.Table;
		this.TestContext.WriteLine($"Command line arguments: " + string.Join(" ", args.Select(r => r.Text.Contains(' ') ? $"\"{r.Text}\"" : r.Text)));

		// Command context
		TestCommandContext cmdContext = new TestCommandContext(this.TestContext, this.hostServices);

		List<object> outputReturns = new List<object>();

		await cmd.InvokeAsync(cmdContext, "<cmd>", args, 0, this.TestContext.CancellationToken);

		this.mocks.VerifyExpectations();

		return cmdContext.OutputRecords.ToArray();
	}
}
