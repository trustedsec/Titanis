using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	public interface ITerminal
	{
		int Width { get; }

		TextWriter OutputWriter { get; }
		void WriteOutput(string? text);
		void WriteOutputLine(string? text);
		void WriteError(string? text);
		void WriteErrorLine(string? text);
		void PushTextColor(ConsoleColor color);
		void PopTextColor();
	}

	public static class TerminalExtensions
	{
		public static void Write(this ITerminal terminal, FormattedText text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));

			text.PrintTo(terminal);
		}
	}
}
