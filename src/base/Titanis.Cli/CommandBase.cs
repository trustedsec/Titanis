using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	/// <summary>
	/// Base class for command implementations
	/// </summary>
	/// <remarks>
	/// Command implementations should derive from <see cref="Command"/> or <see cref="MultiCommand"/>,
	/// which handle parsing of arguments.
	/// </remarks>
	public abstract class CommandBase : IServiceProvider
	{
		/// <summary>
		/// Invokes the command.
		/// </summary>
		/// <param name="args">Arguments to the command</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Result code of executing command</returns>
		public Task<int> InvokeAsync(ICommandContext context, string command, string[] args, CancellationToken cancellationToken)
		{
			return this.InvokeAsync(context, command, CommandLineParser.TokensFromArgs(args), 0, cancellationToken);
		}
		/// <summary>
		/// Invokes the command.
		/// </summary>
		/// <param name="command">Name of invoked command</param>
		/// <param name="args">Command arguments</param>
		/// <param name="startIndex">Index of first argument</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Result code of executing command</returns>
		/// <remarks>
		/// The entire array of arguments is provided so that a command may analyze
		/// what came before its own arguments.
		/// </remarks>
		public async Task<int> InvokeAsync(ICommandContext context, string command, Token[] args, int startIndex, CancellationToken cancellationToken)
		{
			this.Context = context;
			try
			{
				var ret = await this.InvokeAsync(command, args, startIndex, cancellationToken);
				if (this._resultsPending)
					this.FlushOutput();

				if (this._recordsExpected)
				{
					if (this._recordsWritten == 0)
					{
						this.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, "Command completed but no records written"));
					}
					else
					{
						this.WriteMessage(new LogMessage(LogMessageSeverity.Verbose, null, $"{this._recordsWritten} record(s) written"));
					}
				}

				return ret;
			}
			catch (SyntaxException ex)
			{
				if (ex.command == null)
				{
					ex.command = this;
					ex.commandPrefix = command;
				}
				throw;
			}
			finally
			{
				this.Context = null;
			}
		}

		private ICommandContext? _context;
		/// <summary>
		/// Gets the context within which the command is running.
		/// </summary>
		public ICommandContext? Context
		{
			get => this._context;
			private set
			{
				this._context = value;
				if (value != null)
				{
					this._services = new ServiceContainer(value.Services);
				}
			}
		}

		private ServiceContainer? _services;
		/// <summary>
		/// Gets services available to the command.
		/// </summary>
		protected internal ServiceContainer Services => this._services;

		/// <summary>
		/// Gets a value indicating whether the command has a context.
		/// </summary>
		public bool HasContext => this.Context is not null;

		/// <summary>
		/// Invokes the command.
		/// </summary>
		/// <param name="command">Name of invoked command</param>
		/// <param name="args">Command arguments</param>
		/// <param name="startIndex">Index of first argument</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation</param>
		/// <returns>Result code of executing command</returns>
		/// <remarks>
		/// The entire array of arguments is provided so that a command may analyze
		/// what came before its own arguments.
		/// </remarks>
		public abstract Task<int> InvokeAsync(string command, Token[] args, int startIndex, CancellationToken cancellationToken);
		/// <summary>
		/// Runs a program implemented as a command.
		/// </summary>
		/// <typeparam name="TProgram">Program class</typeparam>
		/// <param name="args">Arguments to the command</param>
		/// <returns>Result code of executing command</returns>
		/// <remarks>
		/// This implementation creates a <see cref="CancellationTokenSource"/> and requests
		/// cancellation if the user presses Ctrl+C.
		/// </remarks>
		public static int RunProgramAsync<TProgram>(string[] args)
			where TProgram : CommandBase, new()
		{
			var prog = new TProgram();
			return prog.RunInternal<TProgram>(args).Result;
		}

		#region Informational
		protected string BuildBanner()
		{
			var asm = Assembly.GetEntryAssembly();
			var atrTitle = asm.GetCustomAttribute<AssemblyTitleAttribute>();
			var asmName = asm.GetName();
			var title = atrTitle?.Title ?? asmName.Name;

			var fileVersion = asm.GetCustomAttribute<AssemblyFileVersionAttribute>();
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"{title} Version {fileVersion.Version}");
			return sb.ToString();
		}
		protected void PrintBanner()
		{
			this.WriteMessage(this.BuildBanner());
		}
		#endregion

		#region Command Frames

		/// <summary>
		/// Begins executing within a nested command frame.
		/// </summary>
		/// <param name="func">Delegate to execute</param>
		/// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/></exception>
		/// <remarks>
		/// The nested frame runs with its own <see cref="CancellationToken"/>.
		/// If the user presses Ctrl+C while the frame is executing, the cancellation
		/// is delivered to the nested frame.  However, any resulting exception such as
		/// <see cref="OperationCanceledException"/> is not specifically handled and will
		/// unwind to the caller.  This allows the caller to detect and handle cancellation
		/// as well as propagate the cancellation to the parent frame if it is not handled.
		/// </remarks>
		protected Task ExecuteFrameAsync(Func<CancellationToken, Task> func)
			=> this.VerifyContext().ExecuteFrameAsync(func);

		#endregion

		private async Task<int> RunInternal<TProgram>(string[] args) where TProgram : CommandBase, new()
		{
			var metadata = new CommandMetadataContext(MetadataResolver.Default);
			WindowsConsoleContext context = new WindowsConsoleContext(metadata);
			var command = AppDomain.CurrentDomain.FriendlyName;

			try
			{
				return await this.InvokeAsync(context, command, args, context.CancellationToken);
			}
			catch (OperationCanceledException ex)
			{
				context.WriteError("Operation canceled");
				return -1;
			}
			catch (SyntaxException ex)
			{
				var cmd = ex.command ?? this;
				var prefix = ex.commandPrefix ?? command;
				context.WriteMessage(this.BuildBanner());
				context.WriteMessage(cmd.GetHelpText(prefix, metadata));

				ConsoleColor? color = null;
				try
				{
					color = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.Red;
				}
				catch { }
				context.WriteError(ex.Message);
				if (color.HasValue)
					Console.ForegroundColor = color.Value;
				return -1;
			}
			catch (Exception ex)
			{
				while (ex is AggregateException agg)
				{
					ex = agg.InnerException;
				}
#if DEBUG
				Console.Error.WriteLine(ex);
#else
				Console.Error.WriteLine(ex.Message);
#endif
				Console.Error.WriteLine($"Tool execution failed with exit code {ex.HResult} (0x{ex.HResult:X8})");
				if (ex is IHaveErrorCode hasErrorCode)
					Console.Error.WriteLine($"  Specific error code: {hasErrorCode.ErrorCode} (0x{hasErrorCode.ErrorCode:X8})");
				return ex.HResult;
			}
		}

		#region Logging fields
		private LogMessageSeverity _logLevel;
		/// <summary>
		/// Gets or sets a value specifying the level of messages to log.
		/// </summary>
		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Sets the lowest level of messages to log")]
		public LogMessageSeverity LogLevel
		{
			get => this._logLevel; set
			{
				this._logLevel = value;
				if (this.Log != null)
					this.Log.LogLevel = value;
			}
		}
		private void SetMinLogLevel(LogMessageSeverity level)
		{
			this.LogLevel = (LogMessageSeverity)Math.Min((int)level, (int)this.LogLevel);
		}

		private LogFormat _consoleLogFormat;
		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Sets the format of log messages written to the console")]
		[DefaultValue(LogFormat.Text)]
		public LogFormat ConsoleLogFormat
		{
			get => this._consoleLogFormat;
			set
			{
				this._consoleLogFormat = value;
				if (this.HasContext)
					this.Log.Format = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether to print verbose messages.
		/// </summary>
		[Parameter]
		[Category(ParameterCategories.Output)]
		[Alias("V")]
		[Description("Prints verbose messages")]
		public SwitchParam Verbose
		{
			get => new SwitchParam((this.LogLevel <= LogMessageSeverity.Verbose) ? SwitchParamFlags.Set : SwitchParamFlags.None);
			set
			{
				if (value.IsSet)
					this.SetMinLogLevel(LogMessageSeverity.Verbose);
			}
		}

		/// <summary>
		/// Gets a value indicating whether to print verbose messages.
		/// </summary>
		[Parameter]
		[Alias("vv")]
		[Category(ParameterCategories.Output)]
		[Description("Prints diagnostic messages")]
		public SwitchParam Diagnostic
		{
			get => new SwitchParam((this.LogLevel <= LogMessageSeverity.Diagnostic) ? SwitchParamFlags.Set : SwitchParamFlags.None);
			set
			{
				if (value.IsSet)
					this.SetMinLogLevel(LogMessageSeverity.Diagnostic);
			}
		}
		#endregion

		/// <summary>
		/// Gets a value indicating whether to print values that are more human-readable.
		/// </summary>
		/// <remarks>
		/// For example, a human-readable value indicating the size of the file
		/// may be rounded to the nearest MB.  Other numeric values may be rounded
		/// and printed with the digit separator.
		/// </remarks>
		[Parameter]
		[Category(ParameterCategories.Output)]
		[Description("Formats file sizes as human-readable values")]
		public SwitchParam HumanReadable { get; set; }

		/// <summary>
		/// Verifies that the command has a context.
		/// </summary>
		/// <returns>The currently set <see cref="ICommandContext"/></returns>
		/// <exception cref="InvalidOperationException">The command does not have a context.</exception>
		/// <remarks>
		/// Use this property rather than accessing the context directly to ensure it is non-null.
		/// This also aids in nullability analysis, since the return value will never be <see langword="null"/>.
		/// </remarks>
		private ICommandContext VerifyContext()
		{
			if (this.Context == null)
				throw new InvalidOperationException("This operation requires a context, but there is no context.");

			return this.Context;
		}

		#region Logging
		/// <summary>
		/// Gets the <see cref="ILog"/> to log to.
		/// </summary>
		protected ILog Log => this.VerifyContext().Log;

		protected void WriteMessage(LogMessage message)
		{
			if (message is null) throw new ArgumentNullException(nameof(message));
			this.Log.WriteMessage(message);
		}

		/// <summary>
		/// Writes a diagnostic message
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void WriteDiagnostic(string message)
		{
			this.WriteMessage(new LogMessage(LogMessageSeverity.Diagnostic, null, message));
		}

		/// <summary>
		/// Writes a verbose message
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void WriteVerbose(string message)
		{
			this.WriteMessage(new LogMessage(LogMessageSeverity.Verbose, null, message));
		}

		/// <summary>
		/// Writes a warning message
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void WriteWarning(string message)
		{
			this.WriteMessage(new LogMessage(LogMessageSeverity.Warning, null, message));
		}

		/// <summary>
		/// Writes a normal message
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void WriteMessage(string? message)
		{
			this.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, message));
		}

		/// <summary>
		/// Writes an error message
		/// </summary>
		/// <param name="message">Message to write</param>
		protected void WriteError(string message)
		{
			this.WriteMessage(new LogMessage(LogMessageSeverity.Error, null, message));
		}

		protected void WriteTaskStart(string description)
		{
			this.Log.WriteTaskStart(description);
		}
		#endregion

		#region Output formatting
		private OutputStyle _style;
		private OutputField[]? _outputFields;
		private TextTable? _resultTable;

		/// <summary>
		/// Set when writing records as a table
		/// </summary>
		private bool _resultsPending;

		/// <summary>
		/// Set during any call that anticipates output
		/// </summary>
		private bool _recordsExpected;
		private int _recordsWritten;

		private HashSet<string>? _outputFieldNames;

		protected bool IsFieldInOutput(string fieldName)
			=> this._outputFieldNames?.Contains(fieldName) ?? false;

		protected void SetOutputFormat(OutputStyle style, OutputField[]? fields)
		{
			this._recordsExpected = true;

			this.FlushOutput();

			if (style is OutputStyle.Table or OutputStyle.List && fields.IsNullOrEmpty())
			{
				var recordType = this.GetType().GetCustomAttribute<OutputRecordTypeAttribute>()?.RecordType;
				if (recordType != null)
				{
					fields = OutputField.GetFieldsFor(recordType);
				}
				else
					throw new ArgumentNullException(nameof(fields));
			}

			this._style = style;
			this._outputFields = fields;

			HashSet<string>? fieldNames = null;
			if (fields != null)
			{
				var formatAttrs = this.GetType().GetCustomAttributes<OutputFieldFormatAttribute>(true);
				var byName = formatAttrs.GroupBy(a => a.FieldName).ToDictionary(g => g.Key);
				fieldNames = new HashSet<string>(fields.Select(r => r.Name), StringComparer.OrdinalIgnoreCase);
				foreach (var field in fields)
				{
					if (byName.TryGetValue(field.Name, out var group))
					{
						fieldNames.Add(field.Name);

						var attr = group.First();
						field.FormatStringOverride = attr.FormatString;
						if (attr.FormatterType is not null && typeof(IOutputFormatter).IsAssignableFrom(attr.FormatterType))
						{
							try
							{
								field.formatter = (IOutputFormatter)Activator.CreateInstance(attr.FormatterType);
							}
							catch
							{
								// Silently fail
							}
						}
					}

					if (this.HumanReadable.IsSet && field.IsFileSize && field.formatter == null)
					{
						field.FormatStringOverride = "H2";
						field.formatter = FileSizeFormatter.Instance;
					}
				}
			}
			this._outputFieldNames = fieldNames;

			if (style == OutputStyle.Table)
			{
				TextTable tbl = new TextTable();
				{
					var trHeader = tbl.AddRow();
					var trLine = tbl.AddRow();
					foreach (var field in fields)
					{
						trHeader.AddCell(field.Caption);
						trLine.AddCell(new TextTableCell() { Padding = '-' });
					}
				}

				this._resultTable = tbl;
			}
		}

		protected Stream OpenRawOutputStream()
		{
			this.VerifyContext();
			return this.Context.OpenRawOutputStream();
		}

		protected Stream OpenRawInputStream()
		{
			this.VerifyContext();
			return this.Context.OpenRawInputStream();
		}

		protected void WriteRecords(IEnumerable records)
		{
			// Set regardless of whether there are any records for zero-record message
			this._recordsExpected = true;

			foreach (var rec in records)
			{
				this.WriteRecord(rec);
			}
		}
		protected void WriteRecord(object? record)
		{
			this._recordsExpected = true;
			this._recordsWritten++;

			switch (this._style)
			{
				case OutputStyle.Freeform:
					this.Context.WriteOutputLine(record?.ToString());
					break;
				case OutputStyle.Table:
					{
						Debug.Assert(this._resultTable != null);
						Debug.Assert(this._outputFields != null);

						this._resultsPending = true;

						var tr = this._resultTable.AddRow();
						foreach (var field in this._outputFields)
						{
							var value = field.GetValue(record);
							var formatted = field.FormatValue(value, this._style);

							tr.AddCell(formatted, field.Alignment);
						}
					}
					break;
				case OutputStyle.List:
					Debug.Assert(this._outputFields != null);

					foreach (var field in this._outputFields)
					{
						var value = field.GetValue(record);
						var formatted = field.FormatValue(value, this._style);

						this.Context.WriteOutputLine($"{field.Caption}: {formatted}");
					}
					this.Context.WriteOutputLine(string.Empty);
					break;
				default:
					break;
			}
		}

		private void FlushOutput()
		{
			if (this._resultsPending && this._resultTable != null)
			{
				this.Context.WriteOutputLine(this._resultTable.ToString());
				this._resultsPending = false;
				this._resultTable = null;
			}
		}
		#endregion
		protected string ResolveFsPath(string path)
		{
			return Path.GetFullPath(path);
		}

		internal const string Indent = "  ";

		/// <summary>
		/// Gets the help text for the command.
		/// </summary>
		/// <param name="commandName">Gets the name used to invoke the command</param>
		/// <returns>Command documentation</returns>
		public abstract void GetHelpText(IDocWriter writer, string commandName, CommandMetadataContext context);
		public string GetHelpText(string commandName, CommandMetadataContext context)
		{
			const int ConsoleWidth = 80;
			StringDocWriter writer = new StringDocWriter(ConsoleWidth, Indent);
			this.GetHelpText(writer, commandName, context);
			return writer.ToString();
		}

		internal static string GetDetailedHelp(Type commandType, CommandMetadataContext context)
		{
			StringBuilder sb = new StringBuilder();
			DocumentationPlacement place = 0;
			while (commandType != null)
			{
				var det = context.Resolver.GetCustomAttribute<DetailedHelpTextAttribute>(commandType, true);
				if (det != null)
				{
					if (sb.Length > 0)
					{
						if (place == DocumentationPlacement.BeforeBase)
						{
							sb.AppendLine();
							sb.AppendLine(det.GetText(context));
						}
						else if (place == DocumentationPlacement.AfterBase)
						{
							var detailedText = det.GetText(context) ?? string.Empty;
							sb.Insert(0, detailedText);
							sb.Insert(det.GetText(context).Length, Environment.NewLine + Environment.NewLine);
						}
					}
					else
					{
						sb.AppendLine(det.GetText(context));
					}

					if (det.Placement == DocumentationPlacement.ReplacesBase)
						break;
					else
						place = det.Placement;
				}

				commandType = commandType.BaseType;
			}

			return sb.ToString();
		}

		/// <inheritdoc/>
		public object? GetService(Type serviceType)
		{
			return _services?.GetService(serviceType);
		}
	}
}
