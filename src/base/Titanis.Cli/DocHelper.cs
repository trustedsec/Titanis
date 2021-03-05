using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Titanis.Cli
{
	internal static class DocHelper
	{
		private static readonly char[] BreakChars = new char[] { ' ', '.', ',', ':', ';', '\r', '\n' };
		private static bool IsLineBreak(char c)
			=> (c is '\r' or '\n');
		private static bool IsBreakChar(char c)
			=> BreakChars.Contains(c);

		internal enum TextRunBreakReason
		{
			Overflow,
			WordBreak,
			LineBreak,
			EndOfText,
		}
		internal struct TextRunInfo
		{
			internal int startIndex;
			internal int length;
			internal TextRunBreakReason reason;

			internal TextRunInfo(TextRunBreakReason reason)
			{
				this.reason = reason;
				this.startIndex = 0;
				this.length = 0;
			}
			internal TextRunInfo(TextRunBreakReason reason, int startIndex, int length)
			{
				this.reason = reason;
				this.startIndex = startIndex;
				this.length = length;
			}
		}
		internal struct DocContext
		{
			internal string text;
			internal int startIndex;

			internal DocContext(string text)
			{
				this.text = text;
			}

			internal TextRunInfo GetNextRun(int maxLength)
			{
				var callerMaxLength = maxLength;
				maxLength = Math.Min(maxLength, this.text.Length - this.startIndex);
				if (maxLength <= 0)
					return new TextRunInfo(TextRunBreakReason.EndOfText);

				int startIndex = this.startIndex;
				int breakIndex = -1;
				TextRunBreakReason reason = TextRunBreakReason.Overflow;
				int i;
				for (i = 0; i < maxLength; i++)
				{
					char c = this.text[startIndex + i];

					if (reason == TextRunBreakReason.LineBreak)
					{
						if (c == '\n')
							this.startIndex++;

						break;
					}

					if (c is ' ')
					{
						breakIndex = i;
						this.startIndex = startIndex + i + 1;
						reason = TextRunBreakReason.WordBreak;

						// Keep going until another break is encountered
					}
					else if (c is '\r')
					{
						breakIndex = i;
						this.startIndex = startIndex + i + 1;
						reason = TextRunBreakReason.LineBreak;

						// Keep going and check if the next character is \n
					}
					else if (c is '\n')
					{
						breakIndex = i;
						this.startIndex = startIndex + i + 1;
						reason = TextRunBreakReason.LineBreak;
						break;
					}
				}

				if (reason == TextRunBreakReason.WordBreak && i < callerMaxLength)
				{
					reason = TextRunBreakReason.EndOfText;
					breakIndex = i;
					this.startIndex = this.text.Length;
				}
				else if (reason == TextRunBreakReason.Overflow)
				{
					if (i < callerMaxLength)
						reason = TextRunBreakReason.EndOfText;

					this.startIndex += i;
					breakIndex = i;
				}

				return new TextRunInfo(reason, startIndex, breakIndex);
			}
		}
	}
}
