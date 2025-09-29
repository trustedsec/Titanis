using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Implements <see cref="ITerminal"/> that uses <see cref="Console"/>.
	/// </summary>
	public class WindowsConsoleInfo : ITerminal
	{
		public int Width
		{
			get
			{
				int width;
				try
				{
					width = Console.WindowWidth;
				}
				catch
				{
					width = 80;
				}
				return width;
			}
		}

		public TextWriter OutputWriter => Console.Out;

		/// <summary>
		/// Used to synchronize output
		/// </summary>
		private object _outputLock = new object();

		private Stack<ConsoleColor>? _textColorStack;
		public void PopTextColor()
		{
			lock (this._outputLock)
			{
				var stack = this._textColorStack;
				if (stack is not null && stack.Count > 0)
					Console.ForegroundColor = stack.Pop();
			}
		}

		public void PushTextColor(ConsoleColor color)
		{
			lock (this._outputLock)
			{
				var stack = (this._textColorStack ??= new Stack<ConsoleColor>());
				stack.Push(Console.ForegroundColor);
				Console.ForegroundColor = color;
			}
		}

		public void WriteError(string? text)
		{
			lock (this._outputLock)
			{
				WriteErrorUnsync(text);
			}
		}

		private static void WriteErrorUnsync(string? text)
		{
			Console.Error.Write(text);
		}

		public void WriteErrorLine(string? text)
		{
			lock (this._outputLock)
			{
				Console.Error.WriteLine(text);
			}
		}

		public void WriteOutput(string? text)
		{
			lock (this._outputLock)
			{
				WriteOutputUnsync(text);
			}
		}

		private static void WriteOutputUnsync(string? text)
		{
			Console.Write(text);
		}

		public void WriteOutputLine(string? text)
		{
			lock (this._outputLock)
			{
				Console.WriteLine(text);
			}
		}

		public void WriteFormattedOutput(FormattedText text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));
			lock (this._outputLock)
			{
				text.PrintTo(new TerminalTarget(this, (t, s) => WriteOutputUnsync(s)));
			}
		}

		public void WriteFormattedError(FormattedText text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));
			lock (this._outputLock)
			{
				text.PrintTo(new TerminalTarget(this, (t, s) => WriteErrorUnsync(s)));
			}
		}
	}
}
