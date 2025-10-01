using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Titanis.Cli
{
	public class WindowsConsoleContext : ICommandContext
	{
		internal WindowsConsoleContext(CommandMetadataContext metadata)
		{
			this.Terminal = new WindowsConsoleInfo();
			this.WorkingDirectory = Environment.CurrentDirectory;
			this.Log = new TerminalLog(this.Terminal);
			this.MetadataContext = metadata;

			this._rootFrame = new CommandFrame(null);
			this._currentFrame = this._rootFrame;
			Console.CancelKeyPress += this.Console_CancelKeyPress;

			this._services.AddService(typeof(ILog), this.Log);
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
		public IServiceProvider Services => this._services;

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
	}
}
