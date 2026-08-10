using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	public interface ICommandResultHook
	{
		void OnResult(object? record, RecordInfo? info);
	}

	public class RecordAlert
	{
		public RecordAlert(string? fieldName, int fieldValueIndex = NoValueIndex)
		{
			this.FieldName = fieldName;
			this.FieldValueIndex = fieldValueIndex;
		}

		public const int NoValueIndex = -1;
		public string? FieldName { get; }
		public int FieldValueIndex { get; }
	}
	public class RecordInfo
	{
		public RecordInfo(ImmutableArray<RecordAlert> alerts = default)
		{
			Alerts = alerts;
		}

		public ImmutableArray<RecordAlert> Alerts { get; }

		public bool HasAlertOnField(string? fieldName) => HasAlertOnField(fieldName, RecordAlert.NoValueIndex);
		public bool HasAlertOnField(string? fieldName, int valueIndex)
		{
			return this.Alerts.Any(r => r.FieldName == fieldName && r.FieldValueIndex == valueIndex);
		}
	}

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
		void AddLogListener(ILog listener);

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

		void AddResultHook(ICommandResultHook hook);
		void WriteRecords(IEnumerable records);
		void WriteRecord(object? record);
		void WriteRecord(object? record, RecordInfo? info);
	}
}
