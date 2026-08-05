using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using Titanis.Cli;

namespace Titanis.ToolDocBuilder
{
	internal class ManWriter : DocWriterBase
	{
		private readonly TextWriter writer;

		public ManWriter(TextWriter writer) : base(80, string.Empty)
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			this.writer = writer;
		}


		private StringBuilder _line = new StringBuilder();
		private void _FlushLine()
		{
			if (this._line.Length > 0)
			{
				this._WriteTextLineToOutput(this._line.ToString());
				this._line.Clear();
			}
		}

		private void _WriteTextLineToOutput(string text)
		{
			this.writer.WriteLine(text);
		}

		private void WriteDirective(string directive)
		{
			this._FlushLine();
			this._WriteTextLineToOutput(directive);
		}

		public void SectionHeader(string header)
		{
			this.WriteDirective($".SH {header}");
		}

		public void WriteComment(string comment)
		{
			this._FlushLine();
			this.WriteDirective(@$"'.\"" {comment}");
		}

		protected override void WriteHeadingImpl(string text)
		{
			this.SectionHeader(text?.ToUpper());
		}

		protected override void WriteSubheadingImpl(string text)
		{
			this.WriteDirective($".SS {text}");
		}

		protected override void WriteTextImpl(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				// TODO: Check for embedded newlines
				// TODO: Apply character escaping
				this._line.Append(text);
			}
		}

		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			styles &= mask;
			if (0!=(styles & FormattedTextStyles.Bold))
			{
				this.WriteTextImpl(@"\fB");
				return FormattedTextStyles.Bold;
			}
			else if (0 != (styles & FormattedTextStyles.Italic))
			{
				this.WriteTextImpl(@"\fI");
				return FormattedTextStyles.Italic;
			}
			else
			{
				this.WriteTextImpl(@"\fR");
				return FormattedTextStyles.None;
			}
		}

		private int _tableLevel;
		protected override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			if (table is null) throw new ArgumentNullException(nameof(table));

			this.WriteDirective(".TS");
			this.WriteDirective("tab (@);");

			int prevLevel = this._tableLevel;
			try
			{
				int colCount = 0;
				foreach (var row in table.Rows)
				{
					colCount = Math.Max(colCount, row.Cells.Count);
				}

				for (int i = 0; i < colCount; i++)
				{
					this.WriteTextImpl("L");
				}
				this.WriteTextImpl("x.");
				this._FlushLine();

				foreach (var row in table.Rows)
				{
					var cells = row.Cells;
					for (int i = 0; i < cells.Count; i++)
					{
						if (i > 0)
							this.WriteTextImpl("@");

						this.WriteTextImpl("T{");
						this._FlushLine();
						var cl = cells[i];
						var text = cl?.FormattedText;
						if (text != null)
							text.PrintTo(this);

						this._FlushLine();
						this.WriteTextImpl("T}");
					}
					this._FlushLine();
				}

				this.WriteDirective(".TE");
				this.WriteDirective(".br");
			}
			finally
			{
				this._tableLevel = prevLevel;
			}
		}

		protected override void AppendLineImpl()
		{
			this.WriteDirective(".br");
		}
	}
}
