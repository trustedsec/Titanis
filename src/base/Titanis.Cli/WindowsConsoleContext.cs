using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	public class WindowsConsoleContext : ICommandContext, IServiceProvider
	{
		internal WindowsConsoleContext(CommandMetadataContext metadata)
		{
			this.Terminal = new WindowsConsoleInfo();
			this.WorkingDirectory = Environment.CurrentDirectory;
			this.Log = new TerminalLog(this.Terminal);
			this.MetadataContext = metadata;

			this.FileAccess = new HostFileAccess();

			this._rootFrame = new CommandFrame(null);
			this._currentFrame = this._rootFrame;
			Console.CancelKeyPress += this.Console_CancelKeyPress;

			this._services.AddService(typeof(ILog), this.Log);
			this._services.AddService(typeof(IFileAccess), this.FileAccess);
		}

		private CommandFrame _rootFrame;

		private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			var cancelSource = this._currentFrame?.CancellationSource;
			if (cancelSource != null)
			{
				Console.Error.WriteLine("Ctrl+C pressed, cancelling operation");
				cancelSource.Cancel(true);
				e.Cancel = true;
			}
		}

		public CancellationToken CancellationToken => this._rootFrame.CancellationSource.Token;

		public ITerminal Terminal { get; }

		public ILog Log { get; }

		public string WorkingDirectory { get; }

		public CommandMetadataContext MetadataContext { get; }

		private ServiceContainer _services = new ServiceContainer();
		public IServiceProvider HostServices => this;

		public IFileAccess FileAccess { get; }

		public Stream OpenRawInputStream()
		{
			return Console.OpenStandardInput();
		}

		public Stream OpenRawOutputStream()
		{
			return Console.OpenStandardOutput();
		}

		public void WriteError(string error)
		{
			this.Terminal.WriteErrorLine(error);
		}

		public void WriteMessage(string? message)
		{
			this.Terminal.WriteErrorLine(message);
		}

		public void WriteOutput(string? message)
		{
			this.Terminal.WriteOutput(message);
		}

		public void WriteOutputLine(string? message)
		{
			this.Terminal.WriteOutputLine(message);
		}

		public string Prompt(string prompt)
		{
			this.Terminal.WriteOutput(prompt);
			var response = Console.ReadLine();
			return response;
		}

		private CommandFrame? _currentFrame;
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

		public object? GetVariable(string name) => Environment.GetEnvironmentVariable(name);

		/// <inheritdoc/>
		/// <remarks>
		/// Wrap the <see cref="ServiceContainer"/> rather than granting access to it directly.  This prevents components from adding host services.
		/// </remarks>
		object IServiceProvider.GetService(Type serviceType)
		{
			return _services.GetService(serviceType);
		}


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
		private TextTable? _resultTable;

		public void FlushOutput()
		{
			if (this._resultsPending)
			{
				if (this._resultTable != null)
				{
					this.WriteOutputLine(this._resultTable.ToString());
					this._resultsPending = false;
					this._resultTable = null;
				}
			}
			if (this._outputStyle is OutputStyle.Json)
				this.WriteOutput("]");

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
			}

		}





		#region Formatting support
		private static TextTable BuildResultTable(OutputField[]? fields, bool includeHeaders)
		{
			TextTable tbl = new TextTable();
			if (includeHeaders)
			{
				var trHeader = tbl.AddRow();
				var trLine = tbl.AddRow();
				foreach (var field in fields!)
				{
					trHeader.AddCell(field.Caption);
					trLine.AddCell(new TextTableCell() { Padding = '-' });
				}
			}

			return tbl;
		}

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

		public void WriteRecords(System.Collections.IEnumerable records)
		{
			// Set regardless of whether there are any records for zero-record message
			this._recordsExpected = true;

			foreach (var rec in records)
			{
				this.WriteRecord(rec);
			}
		}
		public void WriteRecord(object? record)
		{
			this._recordsExpected = true;

			var fields = this._outputFieldList;
			if ((this._outputStyle is OutputStyle.Table or OutputStyle.List or OutputStyle.Csv or OutputStyle.Tsv or OutputStyle.Json) && fields is null)
			{
				if (record != null)
				{
					fields = (this._outputFieldProvider ??= CreateDefaultFieldProvider()).GetFieldsForRecord(record);
				}
				else
					throw new ArgumentNullException(nameof(fields));

				// These formats require consistent fields across records
				if (this._outputStyle is OutputStyle.Table or OutputStyle.Csv or OutputStyle.Tsv)
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
				case OutputStyle.Table:
					if (this._resultTable is null)
					{
						Debug.Assert(fields != null);

						TextTable tbl = BuildResultTable(fields, this._includeHeaders);
						this._resultTable = tbl;
					}

					{
						var tbl = this._resultTable;
						if (tbl != null)
						{
							Debug.Assert(fields != null);

							this._resultsPending = true;

							if (record is not null)
							{
								int maxArrayLength = 1;
								for (int arrayIndex = 0; arrayIndex < maxArrayLength; arrayIndex++)
								{
									var tr = tbl.AddRow();
									for (int fieldIndex = 0; fieldIndex < fields!.Length; fieldIndex++)
									{
										OutputField? field = fields![fieldIndex];
										var value = field.GetValue(record);
										string? formatted;

										if (value is Array arr)
										{
											maxArrayLength = Math.Max(maxArrayLength, arr.Length);
											if (arrayIndex < arr.Length)
											{
												value = arr.GetValue(arrayIndex);
												formatted = field.FormatValue(value, this._outputStyle);
											}
											else
												formatted = null;
										}
										else if (arrayIndex == 0 || fieldIndex == 0)
										{
											formatted = field.FormatValue(value, this._outputStyle);
										}
										else
											formatted = null;

										tr.AddCell(formatted, field.Alignment);
									}
								}
							}
							else
							{
								var tr = tbl.AddRow();
							}
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
								if (!(value is Array array))
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
			this._recordsWritten++;
		}

		#endregion
	}
}
