using System;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	public abstract class DocWriterBase : IDocWriter, ITextTableRenderCallback
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

		public IDocWriter AppendLine()
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



		protected void WriteText(string? text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this.MarkDirty(text!.Length);
				this.WriteTextImpl(text);
			}
		}

		protected abstract void WriteTextImpl(string text);


		public IDocWriter WriteBodyText(string? text)
		{
			if (string.IsNullOrEmpty(text))
				return this;

			var maxWidth = this.MaxLineWidth;

			text = ReplaceDocMacros(text)!;

			DocHelper.DocContext context = new DocHelper.DocContext(text!);
			DocHelper.TextRunInfo run;
			do
			{
				run = context.GetNextRun(maxWidth);
				if (!this.IsLineDirty)
					WriteIndent();
				this.WriteText(text.Substring(run.startIndex, run.length));
				if (run.reason != DocHelper.TextRunBreakReason.EndOfText)
					this.AppendLine();
			} while (run.reason != DocHelper.TextRunBreakReason.EndOfText);

			return this;
		}

		protected virtual void WriteIndent()
		{
			this.WriteText("  ");
		}

		public IDocWriter WriteBodyTextLine(string? text)
		{
			this.WriteBodyText(text);
			this.AppendLine();
			return this;
		}

		protected bool InCodeBlock { get; private set; }
		protected virtual void BeginCodeBlockImpl() { }
		public void BeginCodeBlock()
		{
			this.InCodeBlock = true;
			this.BeginCodeBlockImpl();
		}

		protected virtual void EndCodeBlockImpl()
		{
			this.AppendLine();
		}
		public void EndCodeBlock()
		{
			this.InCodeBlock = false;
			this.EndCodeBlockImpl();
		}

		protected virtual void RenderText(string? text, StringBuilder sb)
		{
			sb.Append(text);
		}

		#region Doc macros
		private const string TokenSequence = "##doc[";

		protected string? ReplaceDocMacros(string? text)
		{
			if (text is null || !text.Contains(TokenSequence))
				return text;

			var sb = new StringBuilder();
			this.ReplaceDocMacros(text, sb);
			text = sb.ToString();
			sb.Clear();
			return text;
		}
		protected void ReplaceDocMacros(string? text, StringBuilder sb)
		{
			if ((text is null) || !text!.Contains(TokenSequence))
			{
				RenderText(text, sb);
				return;
			}

			int startIndex = 0;
			int tokenIndex;
			while ((tokenIndex = text.IndexOf(TokenSequence, startIndex)) >= 0)
			{
				if (tokenIndex > startIndex)
					this.RenderText(text.Substring(startIndex, tokenIndex - startIndex), sb);

				tokenIndex += TokenSequence.Length;
				int endIndex = text.IndexOf(']', tokenIndex);
				string content = text.Substring(tokenIndex, endIndex - tokenIndex);

				var tokens = content.Split(';');
				string? replacement = null;
				switch (tokens[0])
				{
					case "link":
						{
							var linkText = tokens.ElementAtOrDefault(1) ?? string.Empty;
							var target = tokens.ElementAtOrDefault(2);
							if (target is null && linkText.StartsWith("#"))
							{
								target = linkText;
								linkText = linkText.Substring(1);
							}
							replacement = this.RenderLinkTo(linkText, target);
						}
						break;
					default:
						break;
				}

				if (replacement != null)
					this.RenderText(replacement, sb);

				startIndex = endIndex + 1;
			}
		}

		protected virtual string RenderLinkTo(string linkText, string? target)
			=> linkText;

		#endregion

		void ITextTableRenderCallback.RenderCellText(TextTableCell cell, StringBuilder sb)
			=> this.RenderCellText(cell, sb);
		protected virtual void RenderCellText(TextTableCell cell, StringBuilder sb)
		{
			var text = cell.Text;
			ReplaceDocMacros(cell?.Text, sb);
		}
	}
}
