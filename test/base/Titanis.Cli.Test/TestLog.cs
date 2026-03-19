
namespace Titanis.Cli.Test
{
	class TestLog : ILog
	{
		public LogMessageSeverity LogLevel { get; set; }
		public LogFormat Format { get; set; }

		public void MarkTaskComplete()
		{
		}

		public void WriteMessage(LogMessage message)
		{
		}

		public void WriteTaskError(Exception ex)
		{
		}

		public void WriteTaskStart(string description)
		{
		}
	}
}
