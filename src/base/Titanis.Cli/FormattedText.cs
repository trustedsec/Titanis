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

		public FormattedText(string? text)
		{
			this.Parts = [new TextPart(text ?? string.Empty)];
		}

		/// <summary>
		/// Gets the parts constituting formatted text.
		/// </summary>
		public FormattedTextPart[] Parts { get; }

		/// <summary>
		/// Prints the text to a terminal.
		/// </summary>
		/// <param name="target">Target</param>
		/// <seealso cref="ITerminal.WriteFormattedOutput(FormattedText)"/>
		/// <seealso cref="ITerminal.WriteFormattedError(FormattedText)"/>
		public void PrintTo(FormattedTextTarget target)
		{
			if (target is null) throw new ArgumentNullException(nameof(target));
			foreach (var part in this.Parts)
			{
				part.PrintTo(target);
			}
		}

	}

	public static class FormattedTextFactory
	{
		private static readonly FormattedText _empty = new FormattedText([]);
		public static FormattedText Empty => _empty;

		public static FormattedTextBuilder Builder() => new FormattedTextBuilder();
		public static FormattedTextPart Text(string? text) => new TextPart(text);
		public static FormattedTextPart LineBreak() => new TextPart(Environment.NewLine);
		public static PushTextColorPart PushTextColor(ConsoleColor color) => new PushTextColorPart(color);
		public static PopTextColorPart PopTextColor() => Singleton.SingleInstance<PopTextColorPart>();

		public static FormattedText Bold(string text) => Styled(text, FormattedTextStyles.Bold);
		public static FormattedText Italic(string text) => Styled(text, FormattedTextStyles.Bold);
		public static FormattedText Styled(string text, FormattedTextStyles styles) => new FormattedText([
			new PushTextStylePart(styles),
			new TextPart(text),
			_popStyle,
			]);
		private static readonly PopTextStylePart _popStyle = new PopTextStylePart();
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
		/// <param name="target">Print target</param>
		internal abstract void PrintTo(FormattedTextTarget target);
	}

	public sealed class TextPart : FormattedTextPart
	{
		public TextPart(string? text)
		{
			this.Text = text ?? string.Empty;
		}

		public string Text { get; }
		/// <inheritdoc/>
		public sealed override string ToString()
			=> this.Text;

		/// <inheritdoc/>
		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			if (!string.IsNullOrEmpty(this.Text))
				target.WriteText(this.Text);
		}
	}

	public sealed class LinkTextPart : FormattedTextPart
	{
		public LinkTextPart(string? text, string linkTarget)
		{
			this.Text = text ?? string.Empty;
			LinkTarget = linkTarget;
		}

		public string Text { get; }
		public string LinkTarget { get; }

		/// <inheritdoc/>
		public sealed override string ToString()
			=> this.Text;

		/// <inheritdoc/>
		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			if (!string.IsNullOrEmpty(this.Text))
				target.WriteLink(this.Text, this.LinkTarget);
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

		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			target.PushTextColor(this.Color);
		}
	}

	public sealed class PopTextColorPart : FormattedTextPart
	{
		public PopTextColorPart()
		{
		}

		public sealed override string ToString()
			=> $"<PopColor>";

		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			target.PopTextColor();
		}
	}



	public sealed class PushTextStylePart : FormattedTextPart
	{
		public PushTextStylePart(FormattedTextStyles styles, FormattedTextStyles mask)
		{
			Styles = styles;
			Mask = mask;
		}
		public PushTextStylePart(FormattedTextStyles styles)
		{
			Styles = styles;
			Mask = styles;
		}

		public FormattedTextStyles Styles { get; }
		public FormattedTextStyles Mask { get; }

		public sealed override string ToString()
			=> $"<PushStyle: {this.Styles} & {this.Mask}>";

		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			target.PushTextStyles(this.Styles, this.Mask);
		}
	}

	public sealed class PopTextStylePart : FormattedTextPart
	{
		public PopTextStylePart()
		{
		}

		public sealed override string ToString()
			=> $"<PopStyles>";

		internal sealed override void PrintTo(FormattedTextTarget target)
		{
			target.PopTextStyles();
		}
	}
}
