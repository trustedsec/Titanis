using System;
using System.Text;

namespace Titanis.Cli
{
	public class StringDocWriter : DocWriterBase
	{
		public StringDocWriter(ITerminal terminal, int maxLineWidth, string indent)
			: base(maxLineWidth, indent)
		{
			this._terminal = terminal;
			this._target = new TerminalTarget(terminal, (t, s) => this.WriteText(s));
		}

		private readonly ITerminal _terminal;
		private readonly TerminalTarget _target;
		protected override FormattedTextTarget? GetTextTarget() => this._target;

		private void _WriteLine(string? text = null) => this._terminal.WriteOutputLine(text);
		private void _WriteText(string? text = null) => this._terminal.WriteOutput(text);
		protected sealed override void WriteHeadingImpl(string text)
		{
			this._WriteLine(text.ToUpper());
			this._WriteText(new string('-', text.Length + 1));
			this._WriteLine();
		}

		protected sealed override void WriteSubheadingImpl(string text)
		{
			this._WriteLine(text);
			this._WriteText(new string('-', text.Length + 1));
			this._WriteLine();
		}

		protected sealed override void WriteTextImpl(string text)
		{
			this._WriteText(text);
		}

		protected sealed override void AppendLineImpl()
		{
			this._WriteLine();
		}

		protected sealed override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			DocWriterTableFormatter formatter = new DocWriterTableFormatter(
				table.ColumnSeparator,
				table.LeftMargin,
				table.RightMargin
				);
			table.Render(formatter);
			formatter.Complete(this);
		}
	}
}
