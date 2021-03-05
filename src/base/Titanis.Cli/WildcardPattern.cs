using System;
using System.Collections.Generic;

namespace Titanis.Cli
{
	/// <summary>
	/// Represents a wildcard pattern for matching file names.
	/// </summary>
	/// <remarks>
	/// A wildcard patterns is generally used by the user to select files based
	/// on the file name.  An asterisk may match zero or more characters.  A
	/// question mark matches exactly one character.  Any other character is
	/// interpreted literally.
	/// </remarks>
	public class WildcardPattern
	{
		enum SegmentType
		{
			Literal,
			Asterisk,
			QMark
		}

		struct Segment
		{
			internal string? text;
			internal SegmentType type;
		}

		private readonly Segment[] _segments;
		private readonly int _minLength;

		private static readonly char[] WildcardChars = new char[] { '*', '?' };
		/// <summary>
		/// Determines whether a string contains any wildcard characters.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <returns><see langword="true"/> if <paramref name="text"/> contains a wildcard character; otherwise, <see langword="false"/>.</returns>
		public static bool ContainsWildcardCharacter(string text)
			=> (text != null) && (text.IndexOfAny(WildcardChars) >= 0);

		/// <summary>
		/// Initializes a new <see cref="WildcardPattern"/>.
		/// </summary>
		/// <param name="pattern">Pattern to check</param>
		/// <exception cref="ArgumentException"><paramref name="pattern"/> is <see langword="null"/>.</exception>
		public WildcardPattern(string pattern)
		{
			if (string.IsNullOrEmpty(pattern)) throw new ArgumentException($"'{nameof(pattern)}' cannot be null or empty.", nameof(pattern));

			List<Segment> segs = new List<Segment>();

			int startIndex = 0;
			int minLength = 0;
			{
				int i;
				for (i = 0; i < pattern.Length; i++)
				{
					char c = pattern[i];
					if (c is '*' or '?')
					{
						if (startIndex < i)
						{
							int segLength = i - startIndex;
							minLength += segLength;
							segs.Add(new Segment { type = SegmentType.Literal, text = pattern.Substring(startIndex, segLength) });
						}

						segs.Add(new Segment { type = c switch { '*' => SegmentType.Asterisk, '?' => SegmentType.QMark } });
						if (c == '?')
							minLength++;

						startIndex = i + 1;
					}
				}
				if (startIndex < i)
				{
					int segLength = i - startIndex;
					minLength += segLength;
					segs.Add(new Segment { type = SegmentType.Literal, text = pattern.Substring(startIndex, segLength) });
				}
			}

			this._segments = segs.ToArray();
			this._minLength = minLength;
		}

		/// <summary>
		/// Checks whether a string matches the pattern.
		/// </summary>
		/// <param name="text">String to check</param>
		/// <returns><see langword="true"/> if <paramref name="text"/> matches the pattern; otherwise, <see langword="false"/>.</returns>
		public bool Matches(string text)
			=> this.Matches(text, 0, 0);
		private bool Matches(string text, int startCharIndex, int startSegIndex)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));

			if (text.Length < this._minLength)
				return false;

			int charIndex = startCharIndex;
			for (int segIndex = startSegIndex; segIndex < this._segments.Length; segIndex++)
			{
				var seg = this._segments[segIndex];
				bool matches = false;
				switch (seg.type)
				{
					case SegmentType.Literal:
						if (text.Length - charIndex >= seg.text.Length)
						{
							matches = 0 == string.Compare(text, charIndex, seg.text, 0, seg.text.Length, true);
							if (matches)
								charIndex += seg.text.Length;
						}
						break;
					case SegmentType.Asterisk:
						for (int j = charIndex; j < text.Length; j++)
						{
							matches = this.Matches(text, charIndex + j, segIndex + 1);
							if (matches)
								return true;
						}
						return false;
					case SegmentType.QMark:
						if (text.Length - charIndex > 0)
						{
							matches = true;
							charIndex++;
						}
						break;
				}

				if (!matches)
					return false;
			}

			return true;
		}
	}
}
