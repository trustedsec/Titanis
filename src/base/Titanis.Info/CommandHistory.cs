using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Info.Schema;

namespace Titanis.Info
{
	public class CommandHistory
	{
		private readonly CommandLog _log;
		private readonly IList<CommandArg> args;
		private readonly InfoBase owner;

		internal CommandHistory(
			CommandLog log,
			IList<CommandArg> args,
			InfoBase owner
			)
		{
			this._log = log;
			this.args = args;
			this.owner = owner;
		}

		public string CommandName => this._log.CommandName;
		public string CommandLine => this._log.CommandLine;
		public DateTime StartTime => this._log.StartTime;
		public DateTime? EndTime => this._log.EndTime;
		public TimeSpan? Duration => this.EndTime - this.StartTime;
		public int? ExitCode => this._log.ExitCode;
		public string? ExitCodeHex => $"0x{this._log.ExitCode:X8}";
		public string? ErrorDetails => this._log.ErrorDetails;
	}
}
