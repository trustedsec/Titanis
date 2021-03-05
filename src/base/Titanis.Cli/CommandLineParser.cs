using System.Collections.Generic;
using System.Data;
using System.Text;
using Titanis.Reflection;

namespace Titanis.Cli
{

	/// <summary>
	/// Describes a token from a command line.
	/// </summary>
	public struct Token
	{
		/// <summary>
		/// Offset of the first character of this token within the original command line.
		/// </summary>
		/// <remarks>
		/// If the token is quoted, the offset indicates the position of the quote.
		/// </remarks>
		public int CharacterOffset { get; set; }
		/// <summary>
		/// Index of this token relative to other tokens.
		/// </summary>
		public int Index { get; set; }
		/// <summary>
		/// Text of this token.
		/// </summary>
		/// <remarks>
		/// If the token is quoted, this property does not includes the quotes.
		/// </remarks>
		public string Text { get; set; }
		/// <summary>
		/// Gets the original text, including quotes.
		/// </summary>
		public string OriginalText { get; set; }
		/// <summary>
		/// Length this token occupies in the command line.
		/// </summary>
		/// <remarks>
		/// If the token is quoted, the length includes the quotes.
		/// </remarks>
		public int OriginalLength { get; set; }

		/// <inheritdoc/>
		public override string ToString()
			=> this.Text;
	}

	/// <summary>
	/// Implements functionality for parsing a command line into tokens.
	/// </summary>
	public static class CommandLineParser
	{
		//public static string GetHelpString(object obj, bool showLongHelp = false, string commandName = null)
		//{
		//	StringBuilder fullHelp = new StringBuilder();
		//	GetHelp(obj, fullHelp, showLongHelp, commandName);
		//	return fullHelp.ToString();
		//}

		//public static void GetHelp(object obj, StringBuilder fullHelp, bool showLongHelp = false, string commandName = null) => HelpGenerator.GenerateFullHelp(obj, fullHelp, showLongHelp, commandName);

		internal static Token[] TokensFromArgs(string[] args)
		{
			Token[] tokens = (args != null)
				? args.ConvertAll(a => new Token { Text = a, OriginalText = a })
				: new Token[0];

			return tokens;
		}

		/// <summary>
		/// Breaks a command line into tokens.
		/// </summary>
		/// <param name="commandLine">Command line to parse</param>
		/// <returns>An array of <see cref="Token"/> objects.</returns>
		/// <remarks>
		/// Tokens are separated by a space.  If a token is to contain a space, it must be quoted.
		/// To include a quote within a quoted string, use two consecutive quotes.
		/// A quote may occur within a token te begin a quoted passage without quoting
		/// the entire token.
		/// It is not an error to end the command line without a matching quote.
		/// </remarks>
		public static Token[] Tokenize(string commandLine)
		{
			bool quoted = false;
			int startOffset = 0;
			char lastChar = '\0';
			StringBuilder sb = new StringBuilder();
			List<Token> tokens = new List<Token>();
			int i;
			for (i = 0; i <= commandLine.Length; i++)
			{
				bool isEnd = (i == commandLine.Length);
				char c = isEnd ? '\0' : commandLine[i];
				if (quoted)
				{
					if (lastChar == '"')
					{
						if (c != '"')
							quoted = false;
						else
						{
							sb.Append(c);
							lastChar = '\0';
							continue;
						}
					}
					else if (c == '"')
					{
						// Do nothing
					}
					else if (isEnd)
					{
						quoted = false;
					}
					else
					{
						sb.Append(c);
					}
				}

				if (!quoted)
				{
					if (c == ' ' || isEnd)
					{
						if (i > startOffset)
						{
							tokens.Add(new Token
							{
								CharacterOffset = startOffset,
								OriginalLength = i - startOffset,
								OriginalText = commandLine.Substring(startOffset, i - startOffset),
								Text = sb.ToString(),
							});
							sb.Clear();
						}
						else
						{
							// Empty token
						}

						startOffset = i + 1;
					}
					else if (c == '"')
					{
						quoted = true;
						lastChar = '\0';
						continue;
					}
					else
					{
						sb.Append(c);
					}
				}

				lastChar = c;
			}

			return tokens.ToArray();
		}
	}
}
