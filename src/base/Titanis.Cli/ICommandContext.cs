using System;
using System.Collections;
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
		/// <summary>
		/// Provides access to services offered by the host.
		/// </summary>
		IServiceProvider HostServices { get; }

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




		void FlushOutput();
		void OnCommandComplete();

		/// <summary>
		/// Indicates whether a field is selected to be printed in the output.
		/// </summary>
		/// <param name="fieldName">Name of field</param>
		/// <returns><see langword="true"/> if the field will be in the output; otherwise, <see langword="false"/>.</returns>
		bool IsFieldInOutput(string fieldName);
		void SetOutputFormat(OutputStyle style, IOutputFieldProvider? fields, bool includeHeaders);
		void WriteRecords(IEnumerable records);
		void WriteRecord(object? record);
	}
}
