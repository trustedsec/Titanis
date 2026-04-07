using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis
{
	public enum LogMessageSeverity
	{
		Debug = -300,
		Diagnostic = -200,
		Verbose = -100,
		Info = 0,
		Warning = 100,
		Error = 200,
		Critical = 300,
	}
	public class LogMessageType
	{
		[Obsolete("Use the the log schema generater instead of creating your own messages.", true)]
		public LogMessageType(LogMessageSeverity severity, string? source, int messageId, string text, params string[] parameterNames)
			: this(severity, source, null, messageId, text, parameterNames)
		{

		}
		public LogMessageType(LogMessageSeverity severity, string? source, string? name, int messageId, string text, params string[] parameterNames)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));
			this.LogDate = DateTime.UtcNow;

			Severity = severity;
			Source = source;
			Name = name;
			MessageId = messageId;
			MessageFormat = text;
			ParameterNames = parameterNames;

			string[] formats = new string[parameterNames.Length];
			var matches = rgxPlaceholders.Matches(text);
			bool hasNumericParams = false;
			for (int i = 0; i < matches.Count; i++)
			{
				var m = (Match)matches[i];

				Group phGroup = m.Groups["p"];
				if (phGroup.Success)
				{
					if (int.TryParse(phGroup.Value, out var ordinal))
					{
						if (ordinal >= parameterNames.Length)
							throw new ArgumentException("The placeholders in the format string do not have corresponding parameters.", nameof(text));

						Group fGroup = m.Groups["f"];
						formats[ordinal] = fGroup.Value;

						hasNumericParams = true;
					}
					else
					{
						this._hasNamedParams = true;
						var phName = phGroup.Value;
						if (Array.IndexOf(ParameterNames, phName) < 0)
							throw new ArgumentException($"The format string includes a parameter name '{phGroup.Value}', but this name is not in the parameter list.", nameof(text));
					}
				}
			}
			if (hasNumericParams && this._hasNamedParams)
				throw new ArgumentException("The format string contains both named and numeric parameters.  This is not allowed.", nameof(text));
			this.ParameterFormats = formats;
#if DEBUG
			// Test format string
			if (!this._hasNamedParams)
			{
				object[] args = new object[formats.Length];
				for (int i = 0; i < args.Length; i++)
				{
					args[i] = new DummyArg();
				}
				string test = string.Format(text, args);
			}
#endif
		}

		private readonly bool _hasNamedParams;

#if DEBUG
		class DummyArg : IFormattable
		{
			public DummyArg() { }

			public string? Format { get; set; }
			public string ToString(string format, IFormatProvider formatProvider)
			{
				this.Format = format;
				return format;
			}
		}
#endif

		private static readonly Regex rgxPlaceholders = new Regex(@"({{)|({(?<p>\w+)(:(?<f>[^}]*))?})");

		public DateTime LogDate { get; }
		public LogMessageSeverity Severity { get; }
		public string? Source { get; }
		public string? Name { get; }
		public int MessageId { get; }
		public string MessageFormat { get; }
		public string[] ParameterNames { get; }
		public string[] ParameterFormats { get; }

		public LogMessage Create(params object?[] parameters)
		{
			if (parameters is null) throw new ArgumentNullException(nameof(parameters));
			if (parameters.Length != this.ParameterNames.Length)
				Array.Resize(ref parameters, this.ParameterNames.Length);
			//throw new ArgumentException($"Incorrect number of parameters specified for {this.MessageId}.", nameof(parameters));

			return new LogMessage(this, null, parameters);
		}

		public LogMessage CreateWithText(string formattedText, params object?[] parameters)
		{
			if (parameters is null) throw new ArgumentNullException(nameof(parameters));
			if (parameters.Length != this.ParameterNames.Length)
				Array.Resize(ref parameters, this.ParameterNames.Length);
			//throw new ArgumentException($"Incorrect number of parameters specified for {this.MessageId}.", nameof(parameters));

			return new LogMessage(this, formattedText, parameters);
		}

		#region FactoryMethods
		public static LogMessage Diagnostic(string? source, string text) => new LogMessage(LogMessageSeverity.Diagnostic, source, text);
		public static LogMessage Verbose(string? source, string text) => new LogMessage(LogMessageSeverity.Verbose, source, text);
		public static LogMessage Info(string? source, string text) => new LogMessage(LogMessageSeverity.Info, source, text);
		public static LogMessage Warning(string? source, string text) => new LogMessage(LogMessageSeverity.Warning, source, text);
		public static LogMessage Error(string? source, Exception ex) => new LogMessage(LogMessageSeverity.Error, source, ex.Message);
		#endregion
	}

	public class LogMessage
	{
		public LogMessage(LogMessageSeverity severity, string? source, string? text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));
			this.LogDate = DateTime.UtcNow;

			this.Severity = severity;
			this.Source = source;
			this._text = text;
			this.Parameters = Array.Empty<object>();
		}
		internal LogMessage(LogMessageType messageType, string formattedText, object?[] parameters)
		{
			this.LogDate = DateTime.UtcNow;

			this.Severity = messageType.Severity;
			this.Source = messageType.Source;
			this.MessageId = messageType.MessageId;
			this.MessageType = messageType;
			this.Parameters = parameters;

			this._text = formattedText;
			this._messageFormat = messageType.MessageFormat;
		}

		public LogMessageType? MessageType { get; }
		public int MessageId { get; }
		public DateTime LogDate { get; }
		public LogMessageSeverity Severity { get; }
		public string? Source { get; }
		public object?[] Parameters { get; }

		private string? _messageFormat;


		private string? _text;
		public string? Text
		{
			get
			{
				if (this._text is null && this._messageFormat is not null)
					this._text = this.BuildMessageText();
				return _text;
			}
		}

		private string BuildMessageText()
		{
			Debug.Assert(this._messageFormat is not null);
			return string.Format(this._messageFormat, this.Parameters);
		}

		#region FactoryMethods
		public static LogMessage Diagnostic(string? source, string text) => new LogMessage(LogMessageSeverity.Diagnostic, source, text);
		public static LogMessage Verbose(string? source, string text) => new LogMessage(LogMessageSeverity.Verbose, source, text);
		public static LogMessage Info(string? source, string text) => new LogMessage(LogMessageSeverity.Info, source, text);
		public static LogMessage Warning(string? source, string text) => new LogMessage(LogMessageSeverity.Warning, source, text);
		public static LogMessage Error(string? source, Exception ex) => new LogMessage(LogMessageSeverity.Error, source, ex.Message);
		#endregion
	}
}
