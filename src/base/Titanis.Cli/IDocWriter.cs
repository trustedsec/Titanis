using System.Collections.Generic;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Titanis.Cli
{
	public interface IDocWriter
	{
		IDocWriter WriteHeading(string text);
		IDocWriter WriteSubheading(string text);
		IDocWriter WriteBodyText(string text);
		IDocWriter WriteBodyTextLine(string? text);
		IDocWriter AppendLine();

		IDocWriter WriteTable(TextTable table, params string[] columnNames);
		abstract void BeginCodeBlock();
		abstract void EndCodeBlock();
	}
}
