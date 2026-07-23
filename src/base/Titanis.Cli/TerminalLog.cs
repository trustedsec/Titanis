using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	public class TerminalLog : LogWriter
	{
		public TerminalLog(ITerminal terminal)
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
			this.Terminal.WriteFormattedError(new FormattedText(
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
				));
		}
		public override void MarkTaskComplete()
		{
			this.Terminal.WriteFormattedError(new FormattedText(
				FormattedTextFactory.Text(" ["),
				FormattedTextFactory.PushTextColor(ConsoleColor.Green),
				FormattedTextFactory.Text("complete"),
				FormattedTextFactory.PopTextColor(),
				FormattedTextFactory.Text("]"),
				FormattedTextFactory.LineBreak()
				));
		}

		protected override void WriteMessage(LogMessage message, bool lineBreak)
		{
			if (message is null) throw new ArgumentNullException(nameof(message));
			if (message.Severity < LogLevel)
				return;

			if (this.Format is LogFormat.Text or LogFormat.TextWithTimestamp)
			{
				var logLine = FormatMessage(message, message.Severity);
				this.Terminal.WriteFormattedError(logLine);
				if (lineBreak)
					this.Terminal.WriteErrorLine(null);
			}
			else
			{
				string text = base.FormatMessage(message);
				if (lineBreak)
					this.Terminal.WriteOutputLine(text);
				else
					this.Terminal.WriteOutput(text);
			}
		}

		private FormattedText FormatMessage(LogMessage message, LogMessageSeverity severity)
			=> this.Format switch
			{
				LogFormat.TextWithTimestamp => new FormattedText(
					FormattedTextFactory.Text($"[{message.LogDate:O}]{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}]")} "),
					FormattedTextFactory.PushTextColor(GetColorFor(severity)),
					FormattedTextFactory.Text(GetLogLevelToken(severity)),
					FormattedTextFactory.PopTextColor(),
					FormattedTextFactory.Text($": {message.Text}")
					),
				_ => new FormattedText(
					FormattedTextFactory.Text($"{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}]")} "),
					FormattedTextFactory.PushTextColor(GetColorFor(severity)),
					FormattedTextFactory.Text(GetLogLevelToken(severity)),
					FormattedTextFactory.PopTextColor(),
					FormattedTextFactory.Text($": {message.Text}")
					)
			};

		private ConsoleColor GetColorFor(LogMessageSeverity severity)
			=> severity switch
			{
				LogMessageSeverity.Error => ConsoleColor.Red,
				LogMessageSeverity.Warning => ConsoleColor.Yellow,
				LogMessageSeverity.Info => ConsoleColor.White,
				LogMessageSeverity.Verbose => ConsoleColor.Cyan,
				LogMessageSeverity.Diagnostic or LogMessageSeverity.Debug => ConsoleColor.DarkGray,
				_ => ConsoleColor.Gray
			};
	}
}
