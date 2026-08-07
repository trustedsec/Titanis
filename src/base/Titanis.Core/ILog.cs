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

	public class LogBroadcaster : ILog
	{
		public LogBroadcaster()
		{

		}

		private List<ILog> _listeners = new List<ILog>();
		public void AddListener(ILog listener)
		{
			if (listener is null) throw new ArgumentNullException(nameof(listener));
			this._listeners.Add(listener);
		}

		private void ForAll(Action<ILog> action)
		{
			foreach (var log in this._listeners)
			{
				try
				{
					action(log);
				}
				catch
				{
					// TODO: Notify diagnostic?
					// Ignore
				}
			}
		}

		private LogMessageSeverity _level;

		public LogMessageSeverity LogLevel
		{
			get { return _level; }
			set
			{
				_level = value;
				this.ForAll(r => r.LogLevel = value);
			}
		}

		private LogFormat _format;

		public LogFormat Format
		{
			get { return _format; }
			set
			{
				_format = value;
				this.ForAll(R => R.Format = value);
			}
		}

		void ILog.WriteMessage(LogMessage message) => this.ForAll(r => r.WriteMessage(message));
		void ILog.WriteTaskStart(string description) => this.ForAll(r => r.WriteTaskStart(description));
		void ILog.WriteTaskError(Exception ex) => this.ForAll(r => r.WriteTaskError(ex));
		void ILog.MarkTaskComplete() => this.ForAll(r => r.MarkTaskComplete());
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
