using System;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	public abstract class DocWriterBase : FormattedTextTarget, IDocWriter
	{
		public DocWriterBase(int maxLineWidth, string indent)
		{
			MaxLineWidth = maxLineWidth;
			Indent = indent;
		}

		public virtual int MaxLineWidth { get; }
		public string Indent { get; }


		#region Line tracking
		private int _lineLength;
		protected bool IsLineDirty => this._lineLength > 0;
		private void MarkDirty(int length)
		{
			this._lineLength += length;
		}
		private void ClearDirty()
		{
			this._lineLength = 0;
		}
		#endregion

		public IDocWriter WriteLine()
		{
			this.AppendLineImpl();
			this.ClearDirty();
			return this;
		}
		protected abstract void AppendLineImpl();

		public IDocWriter WriteHeading(string text)
		{
			this.WriteHeadingImpl(text);
			ClearDirty();
			return this;
		}
		protected abstract void WriteHeadingImpl(string text);

		public IDocWriter WriteSubheading(string text)
		{
			this.WriteSubheadingImpl(text);
			ClearDirty();
			return this;
		}
		protected abstract void WriteSubheadingImpl(string text);

		public IDocWriter WriteTable(TextTable table, params string[] columnNames)
		{
			if (table is null) throw new ArgumentNullException(nameof(table));
			this.WriteTableImpl(table, columnNames);
			this.ClearDirty();
			return this;
		}
		protected abstract void WriteTableImpl(TextTable table, params string[] columnNames);



		private void WriteTextToOutput(string? text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this.MarkDirty(text!.Length);
				this.WriteTextImpl(text);
			}
		}

		protected abstract void WriteTextImpl(string text);


		IDocWriter IDocWriter.WriteText(string? text)
		{
			this.WriteText(text);
			return this;
		}

		protected virtual FormattedTextTarget? GetTextTarget() => this;
		public IDocWriter WriteText(FormattedText text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));

			text.PrintTo(this.GetTextTarget() ?? this);
			return this;
		}
		public override void WriteText(string? text)
		{
			if (string.IsNullOrEmpty(text))
				return;

			var maxWidth = this.MaxLineWidth;

			DocHelper.DocContext context = new DocHelper.DocContext(text!);
			DocHelper.TextRunInfo run;
			do
			{
				run = context.GetNextRun(maxWidth);
				if (!this.IsLineDirty)
					WriteIndent();
				this.WriteTextToOutput(text.Substring(run.startIndex, run.length));
				if (run.reason != DocHelper.TextRunBreakReason.EndOfText)
					this.WriteLine();
			} while (run.reason != DocHelper.TextRunBreakReason.EndOfText);
		}

		protected virtual void WriteIndent()
		{
			this.WriteTextToOutput(this.Indent);
		}

		public IDocWriter WriteLine(string? text)
		{
			this.WriteText(text);
			this.WriteLine();
			return this;
		}

		protected bool InCodeBlock { get; private set; }
		protected virtual void BeginCodeBlockImpl() { }
		public IDocWriter BeginCodeBlock()
		{
			this.InCodeBlock = true;
			this.BeginCodeBlockImpl();
			return this;
		}

		protected virtual void EndCodeBlockImpl()
		{
			this.WriteLine();
		}
		public IDocWriter EndCodeBlock()
		{
			this.InCodeBlock = false;
			this.EndCodeBlockImpl();
			return this;
		}

		protected virtual void RenderText(string? text, StringBuilder sb)
		{
			sb.Append(text);
		}


		protected override void SetTextColor(ConsoleColor color)
		{
		}
		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			return FormattedTextStyles.None;
		}
	}
}
