using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	public interface ILog
	{
		LogMessageSeverity LogLevel { get; set; }
		LogFormat Format { get; set; }

		void WriteMessage(LogMessage message);

		void WriteTaskStart(string description);
		void WriteTaskError(Exception ex);
		void MarkTaskComplete();
	}

	public static class LogExtensions
	{
		/// <summary>
		/// Writes a diagnostic message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void WriteDiagnostic(this ILog log, string message)
		{
			log.WriteMessage(new LogMessage(LogMessageSeverity.Diagnostic, null, message));
		}
		/// <summary>
		/// Writes a verbose message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void WriteVerbose(this ILog log, string message)
		{
			log.WriteMessage(new LogMessage(LogMessageSeverity.Verbose, null, message));
		}
		/// <summary>
		/// Writes a normal message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void WriteInfo(this ILog log, string? message)
		{
			log.WriteMessage(new LogMessage(LogMessageSeverity.Info, null, message));
		}
		/// <summary>
		/// Writes a warning message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void WriteWarning(this ILog log, string message)
		{
			log.WriteMessage(new LogMessage(LogMessageSeverity.Warning, null, message));
		}
		/// <summary>
		/// Writes an error message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void WriteError(this ILog log, string message)
		{
			log.WriteMessage(new LogMessage(LogMessageSeverity.Error, null, message));
		}

	}
}
