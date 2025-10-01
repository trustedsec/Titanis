using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	public interface ICommandContext
	{
		ITerminal Terminal { get; }
		string WorkingDirectory { get; }
		IServiceProvider Services { get; }

		CommandMetadataContext MetadataContext { get; }
		object? GetVariable(string name);

		Stream OpenRawInputStream();
		Stream OpenRawOutputStream();
		void WriteError(string text);
		void WriteMessage(string? text);
		void WriteOutput(string? text);
		void WriteOutputLine(string? text);

		string Prompt(string prompt);

		ILog Log { get; }

		Task ExecuteFrameAsync(Func<CancellationToken, Task> func);
	}
}
