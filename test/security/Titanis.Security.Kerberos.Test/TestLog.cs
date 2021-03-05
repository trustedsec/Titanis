using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Security.Kerberos.Test;
public class TestLog : ILog
{
	public TestLog(TestContext context)
	{
		ArgumentNullException.ThrowIfNull(context);
		Context = context;
	}

	public TestContext Context { get; }
	public LogMessageSeverity LogLevel { get; set; }
	public LogFormat Format { get; set; }

	public void MarkTaskComplete()
	{
	}

	public void WriteMessage(LogMessage message)
	{
		ArgumentNullException.ThrowIfNull(message);
		var level = message.Severity switch
		{
			LogMessageSeverity.Error => MessageLevel.Error,
			LogMessageSeverity.Warning => MessageLevel.Warning,
			_ => MessageLevel.Informational
		};
		this.Context.DisplayMessage(level, message.Text);
	}

	public void WriteTaskError(Exception ex)
	{
	}

	public void WriteTaskStart(string description)
	{
	}
}
