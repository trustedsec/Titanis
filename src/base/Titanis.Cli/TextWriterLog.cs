using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Titanis.Cli
{
	public abstract class LogWriter : ILog
	{
		public LogMessageSeverity LogLevel { get; set; } = LogMessageSeverity.Info;
		public LogFormat Format { get; set; } = LogFormat.Text;

		public const string TaskSourceName = "Task";

		public void WriteMessage(LogMessage message)
		{
			if (message is null) throw new ArgumentNullException(nameof(message));
			this.WriteMessage(message, true);
		}
		protected abstract void WriteMessage(LogMessage message, bool lineBreak);


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


		protected string FormatMessage(LogMessage message)
		{
			var levelToken = GetLogLevelToken(message.Severity);
			return Format switch
			{
				LogFormat.Json => FormatJson(message),
				LogFormat.TextWithTimestamp => $"[{message.LogDate:O}]{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}]")} {levelToken} : {message.Text}",
				_ => $"{(string.IsNullOrEmpty(message.Source) ? null : $"[{message.Source}] ")}{(message.Severity > LogMessageSeverity.Info ? $"{levelToken} : " : null)}{message.Text}"
			};
		}

		protected string FormatJson(LogMessage message)
		{
			LogRecord rec = new LogRecord(
				message.Severity.ToString(),
				message.Severity,
				message.Source,
				message.MessageId,
				message.Text
			);

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


		/// <summary>
		/// Represents a log record with a JSON context.
		/// </summary>
		/// <remarks>
		/// This class is used to serialize log messages to JSON.
		/// </remarks>
		class LogRecord
		{
			public LogRecord() { }
			public LogRecord(string severity, LogMessageSeverity severityValue, string? source, int messageId, string? messageText)
			{
				Severity = severity;
				SeverityValue = severityValue;
				Source = source;
				MessageId = messageId;
				MessageText = messageText;
			}

			public string? Severity { get; set; }
			public LogMessageSeverity? SeverityValue { get; set; }
			public string? Source { get; set; }
			public int MessageId { get; set; }
			public string? MessageText { get; set; }
			public Dictionary<string, string>? Parameters { get; set; }
		}
	}
	/// <summary>
	/// Implements a log that writes events to a <see cref="TextWriter"/>.
	/// </summary>
	public class TextWriterLog : LogWriter
	{
		public TextWriterLog(TextWriter writer)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			Writer = writer;
		}

		public TextWriter Writer { get; }

		protected override void WriteMessage(LogMessage message, bool lineBreak)
		{
			if (message is null) throw new ArgumentNullException(nameof(message));
			if (message.Severity < LogLevel)
				return;

			var logLine = FormatMessage(message);
			if (lineBreak)
				this.Writer.WriteLine(logLine);
			else
				this.Writer.Write(logLine);
		}
	}
}
