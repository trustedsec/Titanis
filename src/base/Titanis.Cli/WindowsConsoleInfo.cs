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

		private Stack<ConsoleColor>? _textColorStack;
		public void PopTextColor()
		{
			var stack = this._textColorStack;
			if (stack is not null && stack.Count > 0)
				Console.ForegroundColor = stack.Pop();
		}

		public void PushTextColor(ConsoleColor color)
		{
			var stack = (this._textColorStack ??= new Stack<ConsoleColor>());
			stack.Push(Console.ForegroundColor);
			Console.ForegroundColor = color;
		}

		public void WriteError(string? text)
		{
			Console.Error.Write(text);
		}

		public void WriteErrorLine(string? text)
		{
			Console.Error.WriteLine(text);
		}

		public void WriteOutput(string? text)
		{
			Console.Write(text);
		}

		public void WriteOutputLine(string? text)
		{
			Console.WriteLine(text);
		}
	}
}
