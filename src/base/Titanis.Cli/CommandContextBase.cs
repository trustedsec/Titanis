using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	public abstract class CommandContextBase
	{
		protected CommandContextBase(CommandMetadataContext metadata)
		{
			this._rootFrame = new CommandFrame(null);
			this._currentFrame = this._rootFrame;
			this.MetadataContext = metadata;
		}

		public CommandMetadataContext MetadataContext { get; }

		#region Command Frames
		private CommandFrame? _currentFrame;
		internal CommandBase currentCommand;
		private CommandFrame _rootFrame;
		protected virtual CancellationTokenSource? GetCancellationSource() => this._currentFrame?.CancellationSource;

		public CancellationToken CancellationToken => this._rootFrame.CancellationSource.Token;

		public async Task ExecuteFrameAsync(Func<CancellationToken, Task> func)
		{
			if (func is null) throw new ArgumentNullException(nameof(func));

			var frame = new CommandFrame(this._currentFrame);
			this._currentFrame = frame;
			try
			{
				await func(frame.CancellationSource.Token);
			}
			finally
			{
				this._currentFrame = frame.Parent;
			}
		}
		#endregion

		#region Record output
		#region Formatting
		private OutputStyle _outputStyle;
		private IOutputFieldProvider? _outputFieldProvider;
		private OutputField[]? _outputFieldList;

		private bool _includeHeaders;
		public void SetOutputFormat(OutputStyle style, IOutputFieldProvider? fields, bool includeHeaders)
		{
			if (style is not OutputStyle.Raw)
				this._recordsExpected = true;
			this._includeHeaders = includeHeaders;

			this.FlushOutput();

			if (style is OutputStyle.TreeTable && this.currentCommand is ISupportTreeOutput treeProvider)
				this._treeHandler = treeProvider.CreateTreeHandler();
			else
				this._treeHandler = null;

			if (style is OutputStyle.TreeTable && this._treeHandler is null)
				style = OutputStyle.Table;

			this._outputStyle = style;
			this._outputFieldProvider = fields ?? new OutputFieldProvider(this.MetadataContext);

			if (style is OutputStyle.Json)
			{
				this.WriteOutputLine("[");
			}
		}

		private OutputFieldProvider CreateDefaultFieldProvider()
		{
			return new(this.MetadataContext);
		}

		#endregion

		/// <summary>
		/// Set when writing records as a table
		/// </summary>
		private bool _resultsPending;

		/// <summary>
		/// Set during any call that anticipates output
		/// </summary>
		private bool _recordsExpected;
		private int _recordsWritten;
		private TableHandler? _resultTable;

		// Tree stuff
		private TreeHandler? _treeHandler;

		public virtual void OnCommandComplete()
		{
			if (this._recordsExpected)
			{
				if (this._recordsWritten == 0)
				{
					this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, "Command completed but no records written"));
				}
				else
				{
					this.Log.WriteMessage(new LogMessage(LogMessageSeverity.Verbose, null, $"{this._recordsWritten} record(s) written"));
				}
				this._recordsExpected = false;
				this._recordsWritten = 0;
			}
		}
		public virtual void FlushOutput()
		{
			if (this._treeHandler != null)
			{
				StringBuilder sb = new StringBuilder();

				var allNodes = this._treeHandler.BuildTree();
				if (allNodes.Count > 0)
				{
					var tbl = new TableHandler(this._treeHandler.GetDisplayFields(), this._includeHeaders, this._outputStyle);
					foreach (var node in allNodes)
					{
						tbl.AddRow(node.Record, node.BuildLineArt(false), node.BuildLineArt(true), false);
					}

					this.PrintTable(tbl.BuildTable());
				}
				this._treeHandler = null;
			}
			else
			{
				if (this._resultsPending)
				{
					if (this._resultTable != null)
					{
						this.PrintTable(this._resultTable.BuildTable());
						this._resultsPending = false;
						this._resultTable = null;
					}
				}

				if (this._outputStyle is OutputStyle.Json)
					this.WriteOutput("]");
			}
		}





		#region Formatting support

		static string FormatValue(string sep, string? text)
		{
			if (string.IsNullOrEmpty(text))
				return text;

			var qual = '"';
			if (text.Contains(sep))
			{
				if (text.Contains(qual))
					text = text.Replace("\"", "\"\"");
				text = qual + text + qual;
			}
			return text;
		}
		#endregion



		/// <inheritdoc/>
		public bool IsFieldInOutput(string fieldName)
			=> this._outputFieldProvider?.IncludesField(fieldName) ?? true;

		protected virtual void OnRecordWritten(object? record)
		{

		}

		public void WriteRecords(System.Collections.IEnumerable records)
		{
			// Set regardless of whether there are any records for zero-record message
			this._recordsExpected = true;

			if (records != null)
			{
				foreach (var rec in records)
				{
					this.WriteRecord(rec);
				}
			}
		}
		public void WriteRecord(object? record)
		{
			this._recordsExpected = true;

			var fields = this._outputFieldList;
			if (this._treeHandler != null)
			{
				this._treeHandler.fields ??= (this._outputFieldProvider ??= CreateDefaultFieldProvider()).GetFieldsForRecord(record);
				this._treeHandler.AddRecord(record);
			}
			else
			{
				if ((this._outputStyle is OutputStyle.Table or OutputStyle.TreeTable or OutputStyle.List or OutputStyle.Csv or OutputStyle.Tsv or OutputStyle.Json) && fields is null)
				{
					if (record != null)
					{
						fields = (this._outputFieldProvider ??= CreateDefaultFieldProvider()).GetFieldsForRecord(record);
					}
					else
						throw new ArgumentNullException(nameof(fields));

					// These formats require consistent fields across records
					if (this._outputStyle is OutputStyle.Table or OutputStyle.TreeTable or OutputStyle.Csv or OutputStyle.Tsv)
					{
						this._outputFieldList = fields;

						if (this._outputStyle is OutputStyle.Csv or OutputStyle.Tsv)
						{
							var sep = this._outputStyle switch { OutputStyle.Csv => ",", OutputStyle.Tsv => "\t" };
							string line = string.Join(sep, fields.Select(r => FormatValue(sep, r.Name)));
							this.WriteOutputLine(line);
						}
					}
				}

				switch (this._outputStyle)
				{
					case OutputStyle.Freeform:
						this.WriteOutputLine(record?.ToString());
						break;
					case OutputStyle.Table or OutputStyle.TreeTable:
						if (this._resultTable is null)
						{
							Debug.Assert(fields != null);

							this._resultTable = new TableHandler(fields, this._includeHeaders, this._outputStyle);
						}

						{
							var tbl = this._resultTable;
							if (tbl != null)
							{
								Debug.Assert(fields != null);

								this._resultsPending = true;
								this._resultTable.AddRow(record);
							}
						}
						break;
					case OutputStyle.List:
						Debug.Assert(fields != null);

						if (record is not null)
						{
							foreach (var field in fields!)
							{
								var value = field.GetValue(record);
								if (value is not null)
								{
									if (!(value is System.Collections.IList array))
										array = new object[] { value };

									foreach (var elem in array)
									{
										var formatted = field.FormatValue(elem, this._outputStyle);

										if (this._includeHeaders)
											this.WriteOutputLine($"{field.Caption}: {formatted}");
										else
											this.WriteOutputLine(formatted);
									}
								}
							}
						}
						this.WriteOutputLine(string.Empty);
						break;
					case OutputStyle.Csv or OutputStyle.Tsv:
						if (fields != null && record is not null)
						{
							if (_includeHeaders)
							{ 
								//TODO: Properly handle array like values
								var sep = this._outputStyle switch { OutputStyle.Csv => ",", OutputStyle.Tsv => "\t" };
								string line = string.Join(sep, fields.Select(r => FormatValue(sep, r.FormatValue(r.GetValue(record), this._outputStyle))));
								this.WriteOutputLine(line);
							}
						}
						break;
					case OutputStyle.Json:
						if (fields != null && record is not null)
						{
							Dictionary<string, object?> values = new Dictionary<string, object?>();
							foreach (var field in fields)
							{
								var fieldValue = field.GetValue(record);
								if (fieldValue != null)
								{
									string formatted = field.FormatValue(fieldValue, OutputStyle.Json);
									values.Add(field.Name, fieldValue);
								}
							}
							if (this._recordsWritten > 0)
								this.WriteOutput(",");
							var jsonLine = JsonSerializer.Serialize(values);
							this.WriteOutputLine(jsonLine);
						}
						break;
					default:
						break;
				}
			}
			this._recordsWritten++;

			this.OnRecordWritten(record);
		}

		#endregion


		public abstract ILog Log { get; }
		public abstract void WriteOutput(string? message);
		public abstract void WriteOutputLine(string? message);
		protected abstract void PrintTable(TextTable table);
	}
}
