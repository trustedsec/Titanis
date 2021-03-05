using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Titanis.Cli
{
	public class TextWriterLog : ILog
	{
		public TextWriterLog(TextWriter writer)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			Writer = writer;
		}

		public TextWriter Writer { get; }
		public LogMessageSeverity LogLevel { get; set; } = LogMessageSeverity.Info;
		public LogFormat Format { get; set; } = LogFormat.Text;

		public virtual void WriteMessage(LogMessage message)
		{
			WriteMessage(message, true);
		}

		class LogRecord
		{
			public string Severity { get; set; }
			public LogMessageSeverity SeverityValue { get; set; }
			public string? Source { get; set; }
			public int MessageId { get; set; }
			public string MessageText { get; set; }
			public Dictionary<string, string>? Parameters { get; set; }
		}

		protected void WriteMessage(LogMessage message, bool lineBreak)
		{
			if (message is null) throw new ArgumentNullException(nameof(message));
			if (message.Severity < LogLevel)
				return;

			var levelToken = GetLogLevelToken(message.Severity);
			var logLine = FormatMessage(message, levelToken);
			if (lineBreak)
				Writer.WriteLine(logLine);
			else
				Writer.Write(logLine);
		}

		private string FormatMessage(LogMessage message, string levelToken)
			=> Format switch
			{
				LogFormat.Json => FormatJson(message),
				LogFormat.TextWithTimestamp => $"[{message.LogDate:O}]{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}]")} {levelToken} : {message.Text}",
				_ => $"{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}] ")}{(message.Severity > LogMessageSeverity.Info ? $"{levelToken} : " : null)}{message.Text}"
			};

		private string FormatJson(LogMessage message)
		{
			LogRecord rec = new LogRecord()
			{
				Severity = message.Severity.ToString(),
				SeverityValue = message.Severity,
				Source = message.Source,
				MessageId = message.MessageId,
				MessageText = message.Text,
			};

			if (message.MessageType != null)
			{
				var parms = new Dictionary<string, string>(message.Parameters.Length);
				for (int i = 0; i < message.Parameters.Length; i++)
				{
					var parm = message.Parameters[i];
					var fmt = message.MessageType.ParameterFormats[i];
					if (parm != null)
					{
						string paramText = fmt != null && parm is IFormattable parmfmt ? parmfmt.ToString(fmt, null) : parm.ToString();
						parms[message.MessageType.ParameterNames[i]] = paramText;
					}
				}
				rec.Parameters = parms;
			}

			var json = JsonSerializer.Serialize(rec);
			return json;
		}

		protected string? CurrentTask { get; set; }
		public virtual void WriteTaskStart(string description)
		{
			CurrentTask = description;
			WriteMessage(new LogMessage(LogMessageSeverity.Info, TaskSourceName, description));
		}

		public virtual void WriteTaskError(Exception ex)
		{
			WriteMessage(new LogMessage(LogMessageSeverity.Info, TaskSourceName, ex.ToString()));
		}

		public virtual void MarkTaskComplete()
		{
			WriteMessage(new LogMessage(LogMessageSeverity.Info, TaskSourceName, CurrentTask + " [complete]"));
		}

		public const string TaskSourceName = "Task";

		public static string GetLogLevelToken(LogMessageSeverity level)
		{
			return level switch
			{
				LogMessageSeverity.Debug => "DBG",
				LogMessageSeverity.Diagnostic => "DIAG",
				LogMessageSeverity.Verbose => "VERBOSE",
				LogMessageSeverity.Info => "INFO",
				LogMessageSeverity.Warning => "WARN",
				LogMessageSeverity.Error => "ERROR",
				LogMessageSeverity.Critical => "ERROR",
				_ => $"({level})"
			};
		}
	}
}
