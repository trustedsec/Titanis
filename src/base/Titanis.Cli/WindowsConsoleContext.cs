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

	public class WindowsConsoleContext : CommandContextBase, ICommandContext, IServiceProvider
	{
		internal WindowsConsoleContext(CommandMetadataContext metadata)
			: base(metadata)
		{
			this.Terminal = new WindowsConsoleInfo();
			this.WorkingDirectory = Environment.CurrentDirectory;
			this.Log = new TerminalLog(this.Terminal);

			this.FileAccess = new HostFileAccess();

			Console.CancelKeyPress += this.Console_CancelKeyPress;

			this._services.AddService(typeof(ILog), this.Log);
			this._services.AddService(typeof(IFileAccess), this.FileAccess);
		}

		private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
		{
			var cancelSource = this.GetCancellationSource();
			if (cancelSource != null)
			{
				Console.Error.WriteLine("Ctrl+C pressed, cancelling operation");
				cancelSource.Cancel(true);
				e.Cancel = true;
			}
		}


		public ITerminal Terminal { get; }

		public override ILog Log { get; }

		public string WorkingDirectory { get; }

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

		public override void WriteOutput(string? message)
		{
			this.Terminal.WriteOutput(message);
		}

		public override void WriteOutputLine(string? message)
		{
			this.Terminal.WriteOutputLine(message);
		}

		public string Prompt(string prompt)
		{
			this.Terminal.WriteOutput(prompt);
			var response = Console.ReadLine();
			return response;
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

		protected override void PrintTable(TextTable table)
		{
			DocWriterTableFormatter formatter = new(
				table.ColumnSeparator,
				table.LeftMargin,
				table.RightMargin
				);
			table.Render(formatter);
			formatter.Complete(new StringDocWriter(this.Terminal, 80, string.Empty));
		}
	}
}
