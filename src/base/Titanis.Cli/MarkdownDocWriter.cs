using System;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	public sealed class MarkdownDocWriter : DocWriterBase
	{
		private readonly TextWriter writer;

		public MarkdownDocWriter(TextWriter writer, int maxLineWidth)
			: base(maxLineWidth, string.Empty)
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

		private void _WriteRaw(string text)
		{
			this.writer.Write(text);
		}
		private void _WriteRawLine(string text)
		{
			this.writer.WriteLine(text);
		}

		class TableFormatter : TextTableFormatterBase
		{
			private readonly MarkdownDocWriter _writer;
			private readonly int _colCount;

			internal TableFormatter(MarkdownDocWriter writer, int colCount)
			{
				this._writer = writer;
				this._colCount = colCount;
			}

			public override void RenderRow(TextTableRow? row)
			{
				bool hasContent = false;
				foreach (var cell in row.Cells)
				{
					if (!(cell?.IsEmpty ?? true))
					{
						hasContent = true;
						break;
					}
				}
				if (!hasContent)
					return;

				var writer = this._writer;
				for (int i = 0; i < this._colCount; i++)
				{
					writer._WriteRaw("|");
					if (i < row.Cells.Count)
					{
						var cell = row.Cells[i];

						if (cell != null)
						{
							cell.FormattedText.PrintTo(this._writer);
						}
					}
				}
				writer._WriteRawLine("|");
			}
		}

		private bool _inTable;
		protected sealed override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			if (table is null) throw new ArgumentNullException(nameof(table));

			this.writer.WriteLine();

			// Render headings
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

			table.Render(new TableFormatter(this, columnNames.Length));

			this.writer.WriteLine();
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

		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			var changed = (baseStyles ^ styles) & mask;
			if (0 != (changed & FormattedTextStyles.Bold))
				this._WriteRaw("**");
			if (0 != (changed & FormattedTextStyles.Italic))
				this._WriteRaw("*");

			return baseStyles ^= changed;
		}

		internal override void WriteLink(string text, string linkTarget)
		{
			this._WriteRaw($"[{text}]({linkTarget})");
		}
	}
}
