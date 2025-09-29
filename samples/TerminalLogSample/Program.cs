using Titanis;
using Titanis.Cli;

namespace TerminalLogSample;

internal class Program : Command
{
	static void Main(string[] args)
		=> RunProgramAsync<Program>(args);

	protected sealed override async Task<int> RunAsync(CancellationToken cancellationToken)
	{
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Error, "LogSample", "Error message"));
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Warning, "LogSample", "Warning message"));
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Info, "LogSample", "Informational message"));
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Verbose, "LogSample", "Verbose message"));
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Diagnostic, "LogSample", "Diagnostic message"));
		this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Debug, "LogSample", "Debug message"));

		return 0;
	}
}
