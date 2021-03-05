using System.Text;

namespace Titanis.Cli
{
	public class StringDocWriter : DocWriterBase
	{
		public StringDocWriter(int maxLineWidth, string indent)
			: base(maxLineWidth, indent)
		{ }

		/// <inheritdoc/>
		public override string ToString() => this._sb.ToString();

		private StringBuilder _sb = new StringBuilder();

		protected sealed override void WriteHeadingImpl(string text)
		{
			this._sb.AppendLine(text.ToUpper());
			this._sb.Append('-', text.Length + 1).AppendLine();
		}

		protected sealed override void WriteSubheadingImpl(string text)
		{
			this._sb.AppendLine(text);
			this._sb.Append('-', text.Length + 1).AppendLine();
		}

		protected sealed override void WriteTextImpl(string text)
		{
			this._sb.Append(text);
		}

		protected sealed override void AppendLineImpl()
		{
			this._sb.AppendLine();
		}

		protected sealed override void WriteTableImpl(TextTable table, params string[] columnNames)
		{
			table.ToString(this._sb, this);
		}
	}
}
