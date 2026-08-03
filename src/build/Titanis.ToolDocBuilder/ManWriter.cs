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

		public ManWriter(TextWriter writer) : base(80, "\t")
		{
			if (writer is null) throw new ArgumentNullException(nameof(writer));
			this.writer = writer;
		}


		private StringBuilder _line = new StringBuilder();
		private void _FlushLine()
		{
			if (this._line.Length > 0)
			{
				this._WriteLiteralTextLine(this._line.ToString());
				this._line.Clear();
			}
		}

		private void _WriteLiteralTextLine(string text)
		{
			this.writer.WriteLine(text);
		}

		public void SectionHeader(string header)
		{
			this._FlushLine();
			this.writer.WriteLine($".SH {header}");
		}

		public void WriteComment(string comment)
		{
			this._FlushLine();
			this.writer.WriteLine(@$"'.\"" {comment}");
		}

		protected override void WriteHeadingImpl(string text)
		{
			this.SectionHeader(text?.ToUpper());
		}

		protected override void WriteSubheadingImpl(string text)
		{
			this._FlushLine();
			this.writer.WriteLine($".SS {text}");
		}

		protected override void WriteTextImpl(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this._line.Append(text);
			}
		}

		protected override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			if (table is null) throw new ArgumentNullException(nameof(table));

			this._FlushLine();

			this.writer.WriteLine(".TS");



			this.writer.WriteLine(".TE");
		}

		protected override void AppendLineImpl()
		{
			this._FlushLine();
			this.writer.WriteLine(".br");
		}
	}
}
