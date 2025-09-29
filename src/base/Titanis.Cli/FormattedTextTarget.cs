using System;

namespace Titanis.Cli
{
	public abstract class FormattedTextTarget
	{
		internal abstract void PopTextColor();
		internal abstract void PushTextColor(ConsoleColor color);
		internal abstract void WriteText(string? text);
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

		internal sealed override void PopTextColor()
		{
			this.Terminal.PopTextColor();
		}

		internal sealed override void PushTextColor(ConsoleColor color)
		{
			this.Terminal.PushTextColor(color);
		}

		internal sealed override void WriteText(string? text)
		{
			this.writeFunc(this.Terminal, text);
		}
	}
}
