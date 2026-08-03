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
using System.Text.Json;
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
		/// Determines whether a token indicates whether the user is requesting help.
		/// </summary>
		/// <param name="candidate">Token to check</param>
		/// <returns><see langword="true"/></returns>
		protected static bool IsDistressCall(string candidate)
			=> candidate is "-?" or "-h" or "--help";

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
			// TODO: This is a bit of a kludge
			if (context is CommandContextBase ctxBase)
				ctxBase.currentCommand = this;

			try
			{
				var ret = await this.InvokeAsync(command, args, startIndex, cancellationToken);
				context.FlushOutput();

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
				context.OnCommandComplete();
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
			private protected set
			{
				this._context = value;
				if (value != null)
				{
					this._services = new ServiceContainer(value.HostServices);
				}
			}
		}

		private ServiceContainer? _services;
		/// <summary>
		/// Gets services available to the command.
		/// </summary>
		protected internal ServiceContainer Services => this._services;
		protected IFileAccess FileAccessService => this.Services.RequireService<IFileAccess>();

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
		protected abstract Task<int> InvokeAsync(string command, Token[] args, int startIndex, CancellationToken cancellationToken);
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
				return ex.HResult;
			}
			catch (SyntaxException ex)
			{
				var cmd = ex.command ?? this;
				var prefix = ex.commandPrefix ?? command;
				context.WriteMessage(this.BuildBanner());
				cmd.Context = context;
				cmd.PrintHelpText(prefix, metadata);

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

				if (this._logLevel >= LogMessageSeverity.Diagnostic)
				{
					Console.Error.WriteLine(ex);
				}
				else
				{
					Console.Error.WriteLine(ex.Message);
				}

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

		private LogFormat _logFormat;
		[Parameter]
		[Advanced]
		[Alias("LogFormat")]
		[Category(ParameterCategories.Output)]
		[Description("Sets the format of log messages written to the console")]
		[DefaultValue(LogFormat.Text)]
		public LogFormat ConsoleLogFormat
		{
			get => this._logFormat;
			set
			{
				this._logFormat = value;
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
		[Advanced]
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

		/// <summary>
		/// Gets a value indicating whether to print verbose messages.
		/// </summary>
		[Parameter]
		[Advanced]
		[Alias("vvv")]
		[Category(ParameterCategories.Output)]
		[Description("Prints debug messages")]
		public SwitchParam DebugLog
		{
			get => new SwitchParam((this.LogLevel <= LogMessageSeverity.Debug) ? SwitchParamFlags.Set : SwitchParamFlags.None);
			set
			{
				if (value.IsSet)
					this.SetMinLogLevel(LogMessageSeverity.Debug);
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
		[Advanced]
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
		protected ICommandContext VerifyContext()
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

		/// <summary>
		/// Indicates whether a field is selected to be printed in the output.
		/// </summary>
		/// <param name="fieldName">Name of field</param>
		/// <returns><see langword="true"/> if the field will be in the output; otherwise, <see langword="false"/>.</returns>
		protected bool IsFieldInOutput(string fieldName) => this.VerifyContext().IsFieldInOutput(fieldName);

		protected void WriteRecords(System.Collections.IEnumerable records)
		{
			if (records != null)
				this.VerifyContext().WriteRecords(records);
		}

		protected void WriteRecord(object? record) => this.VerifyContext().WriteRecord(record);

		protected Stream OpenRawOutputStream()
		{
			this.VerifyContext();
			return this.VerifyContext().OpenRawOutputStream();
		}

		protected Stream OpenRawInputStream()
		{
			this.VerifyContext();
			return this.VerifyContext().OpenRawInputStream();
		}
		#endregion
		protected string ResolveFsPath(FileSpec path)
		{
			return this.FileAccessService.ResolveFsPath(path);
		}

		internal const string Indent = "  ";

		/// <summary>
		/// Gets the help text for the command.
		/// </summary>
		/// <param name="commandName">Gets the name used to invoke the command</param>
		/// <returns>Command documentation</returns>
		public abstract void PrintHelpText(IDocWriter writer, string commandName, CommandMetadataContext context);
		internal void PrintHelpText(string commandName, CommandMetadataContext context)
		{
			const int ConsoleWidth = 80;
			StringDocWriter writer = new StringDocWriter(this.Context.Terminal, ConsoleWidth, Indent);
			this.PrintHelpText(writer, commandName, context);
		}

		internal static string GetDetailedHelp(
			Type commandType,
			CommandMetadataContext context)
		{
			StringBuilder sb = new StringBuilder();
			DocumentationPlacement place = 0;
			while (commandType != null)
			{
				var typeDescr = context.Resolver.GetDescriptor(commandType);
				var det = typeDescr.GetCustomAttribute<DetailedHelpTextAttribute>(true);
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
							sb.Insert(detailedText.Length, Environment.NewLine + Environment.NewLine);
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
