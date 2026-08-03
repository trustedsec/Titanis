using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	[Flags]
	public enum FormattedTextStyles
	{
		None = 0,
		Bold = 1,
		Italic = 2,
		BoldItalic = 3,

		All = 0xFF
	}
	public abstract class FormattedTextTarget
	{
		public abstract void WriteText(string? text);


		#region Text colors
		private Stack<ConsoleColor>? _textColorStack;
		public void PopTextColor()
		{
			var stack = this._textColorStack;
			if (stack is not null && stack.Count > 0)
				this.SetTextColor(stack.Pop());
		}

		public void PushTextColor(ConsoleColor color)
		{
			var stack = (this._textColorStack ??= new Stack<ConsoleColor>());
			stack.Push(Console.ForegroundColor);
			this.SetTextColor(color);
		}

		protected abstract void SetTextColor(ConsoleColor color);
		#endregion

		#region Text styles
		private Stack<FormattedTextStyles>? _textStyleStack;
		private FormattedTextStyles _curStyles;
		public void PopTextStyles()
		{
			var stack = this._textStyleStack;
			if (stack is not null && stack.Count > 0)
			{
				var style = stack.Pop();
				this._curStyles = SetTextStyles(this._curStyles, style, FormattedTextStyles.All);
			}
		}

		public void PushTextStyles(FormattedTextStyles styles, FormattedTextStyles mask)
		{
			var stack = (this._textStyleStack ??= new Stack<FormattedTextStyles>());
			stack.Push(this._curStyles);

			this._curStyles = SetTextStyles(this._curStyles, styles, mask);
		}

		protected abstract FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask);
		internal virtual void WriteLink(string text, string linkTarget)
		{
			this.WriteText(text);
		}
		#endregion
	}

	public class PlaintextTarget : FormattedTextTarget
	{
		private StringBuilder _sb = new StringBuilder();
		public override void WriteText(string? text) => this._sb.Append(text);
		protected override void SetTextColor(ConsoleColor color)
		{
		}

		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			return FormattedTextStyles.None;
		}

		public string GetText() => this._sb.ToString();
	}

	public class MeasureTarget : FormattedTextTarget
	{
		public int Size { get; private set; }
		public override void WriteText(string? text) => this.Size += (text?.Length ?? 0);
		protected override void SetTextColor(ConsoleColor color)
		{
		}

		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			return FormattedTextStyles.None;
		}

		public void Reset()
		{
			this.Size = 0;
		}
	}

	public class TerminalTarget : FormattedTextTarget
	{
		private readonly Action<ITerminal, string> writeFunc;

		internal TerminalTarget(ITerminal terminal, Action<ITerminal, string> writeFunc)
		{
			if (terminal is null) throw new ArgumentNullException(nameof(terminal));
			if (writeFunc is null) throw new ArgumentNullException(nameof(writeFunc));
			Terminal = terminal;
			this.writeFunc = writeFunc;
		}

		public ITerminal Terminal { get; }

		public sealed override void WriteText(string? text)
		{
			this.writeFunc(this.Terminal, text);
		}

		protected override void SetTextColor(ConsoleColor color)
		{
			this.Terminal.SetTextColor(color);
		}

		protected override FormattedTextStyles SetTextStyles(FormattedTextStyles baseStyles, FormattedTextStyles styles, FormattedTextStyles mask)
		{
			var changes = (baseStyles ^ styles) & mask;
			this.Terminal.SetTextStyles(changes & styles, changes);
			return baseStyles ^ changes;
		}
	}
}
