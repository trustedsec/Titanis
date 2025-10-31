using System;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	public sealed class MarkdownDocWriter : DocWriterBase
	{
		private readonly TextWriter writer;

		public MarkdownDocWriter(TextWriter writer, int maxLineWidth, string indent = "")
			: base(maxLineWidth, indent)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			if (maxLineWidth < 1)
				throw new ArgumentException("Width must be >= 1.", nameof(maxLineWidth));

			this.writer = writer;
		}

		public sealed override int MaxLineWidth => this.InCodeBlock ? int.MaxValue : base.MaxLineWidth;

		protected sealed override void AppendLineImpl()
		{
			this.writer.WriteLine();
		}

		protected sealed override void WriteHeadingImpl(string text)
		{
			this.writer.WriteLine($"## {text}");
		}

		protected sealed override void WriteSubheadingImpl(string text)
		{
			this.writer.WriteLine($"### {text}");
		}

		protected sealed override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			if (table is null) throw new ArgumentNullException(nameof(table));

			this.writer.WriteLine();

			StringBuilder sb = new StringBuilder();

			foreach (var colName in columnNames)
			{
				this.writer.Write('|');
				this.writer.Write(colName);
			}
			this.writer.WriteLine('|');
			for (int i = 0; i < columnNames.Length; i++)
			{
				this.writer.Write("|-");
			}
			this.writer.WriteLine('|');

			int colCount = columnNames.Length;
			foreach (var row in table.Rows)
			{
				bool hasContent = false;
				foreach (var cell in row.Cells)
				{
					if (cell?.Text is not null)
					{
						hasContent = true;
						break;
					}
				}
				if (!hasContent)
					continue;

				for (int i = 0; i < colCount; i++)
				{
					this.writer.Write('|');
					if (i < row.Cells.Count)
					{
						var cell = row.Cells[i];

						if (cell != null)
						{
							string? preamble = null;
							string? postamble = null;
							if (0 != (cell.StyleOptions & TextStyleOptions.Bold))
							{
								preamble += "**";
								postamble += "**";
							}
							if (0 != (cell.StyleOptions & TextStyleOptions.Italic))
							{
								preamble += "*";
								postamble += "*";
							}
							ITextTableRenderCallback render = this;


							this.writer.Write(preamble);
							render.RenderCellText(cell, sb);
							this.writer.Write(sb.ToString());
							sb.Clear();
							this.writer.Write(postamble);
						}
					}
				}
				this.writer.WriteLine('|');
			}

			this.writer.WriteLine();
		}

		protected sealed override void RenderCellText(TextTableCell cell, StringBuilder sb)
		{
			StringBuilder tempSB = (sb.Length == 0) ? sb : new StringBuilder();
			base.RenderCellText(cell, tempSB);
			tempSB.Replace("\\", "\\\\")
				.Replace("|", "\\|")
				;
			if (tempSB != sb)
				sb.Append(tempSB);
		}


		// TODO: What other characters must be escaped?
		private static readonly char[] SpecialChars = new char[] { '<', '>', '&' };

		private static bool RequiresEscaping(string text)
		{
			return text.IndexOfAny(SpecialChars) >= 0;
		}

		protected sealed override void RenderText(string? text, StringBuilder sb)
		{
			if (!this.InCodeBlock && text != null)
			{
				if (RequiresEscaping(text))
				{
					// Let's do it the hard way
					bool inCode = false;
					foreach (var c in text)
					{
						if (inCode)
						{
							sb.Append(c);
							if (c == '`')
								inCode = false;
						}
						else
						{
							sb = c switch
							{
								'<' => sb.Append("&lt;"),
								'>' => sb.Append("&gt;"),
								'&' => sb.Append("&amp;"),
								_ => sb.Append(c)
							};
							if (c == '`')
								inCode = true;
						}
					}
				}
				else
					sb.Append(text);
			}
		}

		protected sealed override void WriteTextImpl(string text)
		{
			if (!this.InCodeBlock && RequiresEscaping(text))
			{
				StringBuilder sb = new StringBuilder();
				RenderText(text, sb);
				this.writer.Write(sb);
			}
			else
				this.writer.Write(text);
		}

		protected sealed override void BeginCodeBlockImpl()
		{
			this.writer.WriteLine("```");
		}

		protected sealed override void EndCodeBlockImpl()
		{
			if (this.IsLineDirty)
				this.writer.WriteLine();
			this.writer.WriteLine("```");
		}

		protected sealed override void WriteIndent()
		{
			if (!this.InCodeBlock)
				base.WriteIndent();
		}

		protected override string RenderLinkTo(string linkText, string? target)
		{
			return $"[{linkText}]({target?.ToLower()?.Replace(" ", "-")})";
		}
	}
}
