using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	public class TerminalLog : TextWriterLog
	{
		public TerminalLog(ITerminal terminal) : base((terminal ?? throw new ArgumentNullException(nameof(terminal))).OutputWriter)
		{
			if (terminal is null) throw new ArgumentNullException(nameof(terminal));
			Terminal = terminal;
		}

		public ITerminal Terminal { get; }

		public override void WriteTaskStart(string description)
		{
			this.CurrentTask = description;
			this.WriteMessage(new LogMessage(LogMessageSeverity.Info, TaskSourceName, description), false);
		}

		public override void WriteTaskError(Exception ex)
		{
			new FormattedText(
				FormattedTextFactory.Text(" ["),
				FormattedTextFactory.PushTextColor(ConsoleColor.Red),
				FormattedTextFactory.Text("failed"),
				FormattedTextFactory.PopTextColor(),
				FormattedTextFactory.Text("]"),
				FormattedTextFactory.LineBreak(),
				FormattedTextFactory.PushTextColor(ConsoleColor.Red),
				FormattedTextFactory.Text(ex.ToString()),
				FormattedTextFactory.LineBreak(),
				FormattedTextFactory.PopTextColor()
				).PrintTo(this.Terminal);
		}
		public override void MarkTaskComplete()
		{
			new FormattedText(
				FormattedTextFactory.Text(" ["),
				FormattedTextFactory.PushTextColor(ConsoleColor.Green),
				FormattedTextFactory.Text("complete"),
				FormattedTextFactory.PopTextColor(),
				FormattedTextFactory.Text("]"),
				FormattedTextFactory.LineBreak()
				).PrintTo(this.Terminal);
		}
	}
}
