using System.Collections.Generic;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis.Cli
{
	public interface IDocWriter
	{
		IDocWriter WriteHeading(string text);
		IDocWriter WriteSubheading(string text);
		IDocWriter WriteText(string text);
		IDocWriter WriteText(FormattedText text);
		IDocWriter WriteLine(string? text);
		IDocWriter WriteLine();

		IDocWriter WriteTable(TextTable table, params string[] columnNames);
		abstract IDocWriter BeginCodeBlock();
		abstract IDocWriter EndCodeBlock();
	}
}
