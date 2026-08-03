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

		void SetTextColor(ConsoleColor color);
		void SetTextStyles(FormattedTextStyles styles, FormattedTextStyles mask);
		void WriteFormattedOutput(FormattedText text);
		void WriteFormattedError(FormattedText text);
	}
}
