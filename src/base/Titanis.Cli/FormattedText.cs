using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents a string formatted for a terminal.
	/// </summary>
	/// <remarks>
	/// Formatted text is composed of <see cref="FormattedTextPart"/> objects
	/// that specify text or format commands.
	/// Use <see cref="FormattedTextFactory"/> to create the individual parts.
	/// </remarks>
	/// <seealso cref="FormattedTextFactory"/>
	public class FormattedText
	{
		/// <summary>
		/// Initializes a new <see cref="FormattedText"/>.
		/// </summary>
		/// <param name="parts">Parts constituting the string</param>
		/// <exception cref="ArgumentNullException"><paramref name="parts"/> is <see langword="null"/> or contains a null element.</exception>
		public FormattedText(params FormattedTextPart[] parts)
		{
			if (parts is null || parts.Contains(null))
				throw new ArgumentNullException(nameof(parts));
			this.Parts = parts;
		}

		/// <summary>
		/// Gets the parts constituting formatted text.
		/// </summary>
		public FormattedTextPart[] Parts { get; }

		/// <summary>
		/// Prints the text to a terminal.
		/// </summary>
		/// <param name="terminal">Terminal</param>
		internal void PrintTo(ITerminal terminal)
		{
			foreach (var part in this.Parts)
			{
				part.PrintTo(terminal);
			}
		}
	}

	public static class FormattedTextFactory
	{
		public static FormattedTextPart Text(string? text) => new TextPart(text);
		public static FormattedTextPart LineBreak() => new TextPart(Environment.NewLine);
		public static PushTextColorPart PushTextColor(ConsoleColor color) => new PushTextColorPart(color);
		public static PopTextColorPart PopTextColor() => Singleton.SingleInstance<PopTextColorPart>();
	}

	/// <summary>
	/// Describes part of a <see cref="FormattedText"/>.
	/// </summary>
	/// <seealso cref="FormattedTextFactory"/>
	public abstract class FormattedTextPart
	{
		/// <summary>
		/// Prints the part to a terminal.
		/// </summary>
		/// <param name="terminal">Terminal</param>
		public abstract void PrintTo(ITerminal terminal);
	}

	public sealed class TextPart : FormattedTextPart
	{
		public TextPart(string? text)
		{
			this.Text = text;
		}

		public string? Text { get; }
		public sealed override string ToString()
			=> this.Text;

		public sealed override void PrintTo(ITerminal terminal)
		{
			if (!string.IsNullOrEmpty(this.Text))
				terminal.WriteOutput(this.Text);
		}
	}

	public sealed class PushTextColorPart : FormattedTextPart
	{
		public PushTextColorPart(ConsoleColor color)
		{
			this.Color = color;
		}

		public ConsoleColor Color { get; }
		public sealed override string ToString()
			=> $"<PushColor: {this.Color}>";

		public sealed override void PrintTo(ITerminal terminal)
		{
			terminal.PushTextColor(this.Color);
		}
	}

	public sealed class PopTextColorPart : FormattedTextPart
	{
		public PopTextColorPart()
		{
		}

		public sealed override string ToString()
			=> $"<PopColor>";

		public sealed override void PrintTo(ITerminal terminal)
		{
			terminal.PopTextColor();
		}
	}
}
