using Lightweight_Directory_Access_Protocol_V3;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Ldap.FilterExpressions
{
	public abstract partial class FilterExpression
	{
		public LdapFilter ToFilter()
			=> new LdapFilter(this.ToFilterAsn1());
		internal abstract Filter ToFilterAsn1();

		// [RFC 4515]
		public static FilterExpression Parse(string text)
		{
			ArgumentException.ThrowIfNullOrEmpty(text);

			var ctx = new ParseContext
			{
				text = text,
			};

			var expr = ctx.ReadFilter();
			return expr;
		}

		partial

				// [RFC 4515]
				struct ParseContext
		{
			internal string text;
			internal int readIndex;

			internal char ReadNextChar()
			{
				var c = this.PeekNextChar();
				if (c == -1)
					return '\0';
				else
				{
					this.readIndex++;
					return (char)c;
				}
			}
			internal int PeekNextChar()
			{
				return readIndex < this.text.Length ? this.text[this.readIndex] : -1;
			}
			internal char ReadExpected(char expected)
			{
				var pos = this.readIndex;
				var c = this.ReadNextChar();
				if (c != expected)
					throw CreateFormatException(expected, c, pos);

				return c;
			}

			private readonly FormatException CreateFormatException(char expected, char actual, int position)
			{
				return new FormatException($"The LDAP filter is not in the expected format.  Expected '{expected}' at position {position} but found '{actual}'.");
			}

			private FilterExpression? TryReadFilter()
			{
				return this.PeekNextChar() == '(' ? this.ReadFilter() : null;
			}

			internal FilterExpression ReadFilter()
			{
				var startPos = this.readIndex;
				this.ReadExpected('(');

				FilterExpression filter;

				var c = this.PeekNextChar();
				switch (c)
				{
					case '&':
					case '|':
						{
							this.ReadExpected((char)c);

							List<FilterExpression> clauses = new List<FilterExpression>();
							{
								FilterExpression? clause;
								while ((clause = this.TryReadFilter()) != null)
								{
									clauses.Add(clause);
								}
							}

							if (clauses.Count == 0)
								throw new FormatException($"Expected one or more filter clauses after '{c}' at position {this.readIndex}.");

							var comps = clauses.ToArray();
							filter = c switch
							{
								'&' => All(comps),
								'|' => Any(comps),
								_ => throw null
							};
						}
						break;

					case '!':
						{
							this.ReadExpected('!');
							var inner = this.ReadFilter();
							filter = inner.Not();
						}
						break;

					case '(':
						// Not in [RFC 4515] but allow the use of nested parentheses
						filter = this.ReadFilter();
						break;

					case var l when (char.IsLetterOrDigit((char)l)):
						{
							StringBuilder sb = new StringBuilder();
							while (IsAttrChar(PeekNextChar()))
							{
								sb.Append(ReadNextChar());
							}

							var attrDesc = sb.ToString();

							var filterType = this.ReadFilterType();
							var assertionValue = this.ReadAssertionValue();
							assertionValue = ParseSpecialValue(attrDesc, assertionValue);

							switch (filterType)
							{
								case FilterType.Equal:
									{
										Match mSubstr;
										if (assertionValue == "*")
											filter = HasAttribute(attrDesc);
										else if ((mSubstr = rgxSubstring.Match(assertionValue)).Success)
										{
											var initial = mSubstr.Groups["initial"];
											var anyGroup = mSubstr.Groups["any"].Captures;
											var final = mSubstr.Groups["final"];

											filter = new SubstringMatchExpression(attrDesc, initial.Value, anyGroup.Select(r => r.Value).ToArray(), final.Value);
										}
										else
											filter = Equal(attrDesc, assertionValue);
									}
									break;
								case FilterType.Greater:
									filter = GreaterOrEqual(attrDesc, assertionValue);
									break;
								case FilterType.Less:
									filter = LessOrEqual(attrDesc, assertionValue);
									break;
								case FilterType.Approx:
									filter = ApproxEqual(attrDesc, assertionValue);
									break;
								case FilterType.AllBits:
									filter = new ExtensibleMatchExpression(attrDesc, FilterFactory.LDAP_MATCHING_RULE_BIT_AND, assertionValue);
									break;
								case FilterType.AnyBits:
									filter = new ExtensibleMatchExpression(attrDesc, FilterFactory.LDAP_MATCHING_RULE_BIT_OR, assertionValue);
									break;
								case FilterType.Transitive:
									filter = new ExtensibleMatchExpression(attrDesc, FilterFactory.LDAP_MATCHING_RULE_TRANSITIVE_EVAL, assertionValue);
									break;
								default: throw new NotImplementedException($"Unknown filter {filterType}.");
							}
						}
						break;

					case -1:
						throw new FormatException($"Expected a filter, but encountered the end of string.");

					default:
						throw new FormatException($"Expected a filter, but encountered '{(char)c} at position {this.readIndex}.");
				}

				if ((c = this.PeekNextChar()) != ')')
					throw new FormatException($"Expected closing ')' at {this.readIndex} to match '(' at {startPos} but encountered '{(char)c}'.");

				this.ReadExpected(')');

				return filter;
			}

			private static Regex rgxSubstring = SubstringRegex();

			private string ParseSpecialValue(string attrDesc, string assertionValue)
			{
				if (assertionValue.StartsWith("0x") && ulong.TryParse(assertionValue.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out var ul))
					return ul.ToString();
				else if (NamedBitGroups.GroupsByName.TryGetValue(attrDesc, out var group))
				{
					string[] parts = assertionValue.Split(',', options: StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

					ulong value = 0;
					foreach (var part in parts)
					{
						if (
							ulong.TryParse(part, out ul)
							|| group.NamedBits.TryGetValue(part, out ul)
							)
							value |= ul;
						else
							return assertionValue;
					}

					assertionValue = value.ToString();
				}

				return assertionValue;
			}

			private static bool IsAttrChar(int c)
			{
				return char.IsLetterOrDigit((char)c) || c is '.' or ';' or '-';
			}

			private enum FilterType
			{
				Approx = '~',
				Equal = '=',
				Greater = '>',
				Less = '<',
				AllBits = '&',
				AnyBits = '|',
				Transitive = '*',
			}

			private FilterType ReadFilterType()
			{
				var pos = this.readIndex;
				var c1 = this.ReadNextChar();
				if (c1 == '=')
					return FilterType.Equal;
				else if (c1 is '~' or '<' or '>' or '&' or '|' or '*')
				{
					var c2 = this.ReadExpected('=');
					return (FilterType)c1;
				}
				else
				{
					throw new FormatException($"Unknown filter type '{c1}' at position {pos}.");
				}
			}

			private string ReadAssertionValue()
			{
				StringBuilder sb = new StringBuilder();

				int c;
				while ((c = this.PeekNextChar()) is not (')' or -1))
				{
					c = this.ReadNextChar();
					if (c == '\\')
					{
						var pos = this.readIndex;
						c = this.ReadNextChar();
						if (!char.IsAsciiHexDigit((char)c))
							throw new FormatException($"Expected hex digit after \\ at position {pos} but encountered '{(char)c}'.");

						var escValue = BinaryHelper.ParseHexChar((char)c) << 4;

						c = this.ReadNextChar();
						if (!char.IsAsciiHexDigit((char)c))
							throw new FormatException($"Expected hex digit after \\ at position {pos} but encountered '{(char)c}'.");

						escValue |= BinaryHelper.ParseHexChar((char)c);

						sb.Append((char)escValue);
					}
					else
						sb.Append((char)c);
				}

				return sb.ToString();
			}

			[GeneratedRegex(@"^(?<initial>[^\*]+)?\*((?<any>[^\*]*)\*)*(?<final>.+)?$")]
			private static partial Regex SubstringRegex();
		}

		#region Operators
		public virtual FilterExpression Not() => new NotExpression(this);
		public static AndExpression All(FilterExpression[] clauses)
		{
			ArgumentNullException.ThrowIfNull(clauses);
			if (clauses.Contains(null)) throw new ArgumentNullException(nameof(clauses));
			return new AndExpression(clauses);
		}
		public static OrExpression Any(FilterExpression[] clauses)
		{
			ArgumentNullException.ThrowIfNull(clauses);
			if (clauses.Contains(null)) throw new ArgumentNullException(nameof(clauses));
			return new OrExpression(clauses);
		}

		public static PresentExpression HasAttribute(string attributeDesc)
		{
			ArgumentException.ThrowIfNullOrEmpty(attributeDesc);
			return new PresentExpression(attributeDesc);
		}

		public static EqualsExpression Equal(string attributeDesc, string assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new EqualsExpression(attributeDesc, assertionValue);
		}

		public static GreaterOrEqualExpression GreaterOrEqual(string attributeDesc, string assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new GreaterOrEqualExpression(attributeDesc, assertionValue);
		}

		public static LessOrEqualExpression LessOrEqual(string attributeDesc, string assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new LessOrEqualExpression(attributeDesc, assertionValue);
		}

		public static ApproxEqualExpression ApproxEqual(string attributeDesc, string assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new ApproxEqualExpression(attributeDesc, assertionValue);
		}

		public static ExtensibleMatchExpression ExtensibleMatch(string attributeDesc, string extension, string assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new ExtensibleMatchExpression(attributeDesc, extension, assertionValue);
		}
		#endregion
	}

	public sealed class NotExpression : FilterExpression
	{
		internal NotExpression(FilterExpression operand)
		{
			Operand = operand;
		}

		public FilterExpression Operand { get; }

		internal override Filter ToFilterAsn1() => new Filter { Not = this.Operand.ToFilterAsn1() };
	}

	public sealed class AndExpression : FilterExpression
	{
		internal AndExpression(FilterExpression[] clauses)
		{
			Clauses = clauses;
		}
		public FilterExpression[] Clauses { get; }

		internal override Filter ToFilterAsn1() => new Filter { And = Array.ConvertAll(this.Clauses, r => r.ToFilterAsn1()) };
	}

	public sealed class OrExpression : FilterExpression
	{
		internal OrExpression(FilterExpression[] clauses)
		{
			Clauses = clauses;
		}
		public FilterExpression[] Clauses { get; }

		internal override Filter ToFilterAsn1() => new Filter { Or = Array.ConvertAll(this.Clauses, r => r.ToFilterAsn1()) };
	}

	public sealed class PresentExpression : FilterExpression
	{
		internal PresentExpression(string attributeDescription)
		{
			AttributeDescription = attributeDescription;
		}

		public string AttributeDescription { get; }

		internal override Filter ToFilterAsn1() => new Filter() { Present = Encoding.UTF8.GetBytes(this.AttributeDescription) };
	}

	public abstract class AssertionExpression : FilterExpression
	{
		internal AssertionExpression(string attributeDescription, string assertionValue)
		{
			AttributeDescription = attributeDescription;
			AssertionValue = assertionValue;
		}

		public string AttributeDescription { get; }
		public string AssertionValue { get; }

		private protected AttributeValueAssertion ToAssertion() => new AttributeValueAssertion(Encoding.UTF8.GetBytes(this.AttributeDescription), Encoding.UTF8.GetBytes(this.AssertionValue));
	}
	public sealed class EqualsExpression : AssertionExpression
	{
		internal EqualsExpression(string attributeDescription, string assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1() => new Filter() { EqualityMatch = this.ToAssertion() };
	}
	public sealed class SubstringMatchExpression : FilterExpression
	{
		private readonly string attributeDescription;
		private readonly string? initial;
		private readonly string[]? any;
		private readonly string? final;

		internal SubstringMatchExpression(string attributeDescription, string? initial, string[]? any, string? final)
		{
			this.attributeDescription = attributeDescription;
			this.initial = initial;
			this.any = any;
			this.final = final;
		}

		internal override Filter ToFilterAsn1()
		{
			List<SubstringFilter_Substrings_Element> elems = new List<SubstringFilter_Substrings_Element>();
			if (!string.IsNullOrEmpty(this.initial))
				elems.Add(new SubstringFilter_Substrings_Element() { Initial = Encoding.UTF8.GetBytes(this.initial) });
			if (!any.IsNullOrEmpty())
			{
				foreach (var any in this.any)
				{
					elems.Add(new SubstringFilter_Substrings_Element() { Any = Encoding.UTF8.GetBytes(any) });
				}
			}

			if (!string.IsNullOrEmpty(this.final))
				elems.Add(new SubstringFilter_Substrings_Element() { Final = Encoding.UTF8.GetBytes(this.final) });

			return new Filter()
			{
				Substrings = new SubstringFilter(Encoding.UTF8.GetBytes(this.attributeDescription), elems.ToArray())
			};
		}
	}
	public sealed class GreaterOrEqualExpression : AssertionExpression
	{
		internal GreaterOrEqualExpression(string attributeDescription, string assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1() => new Filter() { GreaterOrEqual = this.ToAssertion() };
	}
	public sealed class LessOrEqualExpression : AssertionExpression
	{
		internal LessOrEqualExpression(string attributeDescription, string assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1() => new Filter() { LessOrEqual = this.ToAssertion() };
	}
	public sealed class ApproxEqualExpression : AssertionExpression
	{
		internal ApproxEqualExpression(string attributeDescription, string assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1() => new Filter() { ApproxMatch = this.ToAssertion() };
	}
	public sealed class ExtensibleMatchExpression : AssertionExpression
	{
		internal ExtensibleMatchExpression(string attributeDescription, string extension, string assertionValue)
			: base(attributeDescription, assertionValue)
		{
			Extension = extension;
		}

		public string Extension { get; }

		internal override Filter ToFilterAsn1() => new Filter() { ExtensibleMatch = new MatchingRuleAssertion(Encoding.UTF8.GetBytes(this.AssertionValue), Encoding.UTF8.GetBytes(this.Extension), Encoding.UTF8.GetBytes(this.AttributeDescription)) };
	}
}
