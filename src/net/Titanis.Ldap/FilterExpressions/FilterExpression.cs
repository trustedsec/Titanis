using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;

namespace Titanis.Ldap.FilterExpressions
{
	public abstract class FilterExpressionContext
	{
		public abstract string ResolveParameter(string parameterName);
	}

	public struct FilterParameterUsage
	{
		public FilterParameterUsage(string name, string attribute)
		{
			Name = name;
			Attribute = attribute;
		}

		public string Name { get; }
		public string Attribute { get; }
	}

	[Flags]
	public enum LdapFilterParseOptions
	{
		None = 0,
		UseAttributeOids = 1,
	}

	public sealed partial class FilterExpression
	{
		public FilterExpression(FilterClause rootClause, ImmutableArray<FilterParameterUsage> paramUsages = default)
		{
			ArgumentNullException.ThrowIfNull(rootClause);
			RootClause = rootClause;
			ParameterUsages = paramUsages;
		}

		public FilterClause RootClause { get; }
		public ImmutableArray<FilterParameterUsage> ParameterUsages { get; }

		class NullFilterContext : FilterExpressionContext
		{
			internal static readonly NullFilterContext Instance = new NullFilterContext();

			public override string ResolveParameter(string parameterName)
			{
				throw new InvalidOperationException($"The filter references parameter '{parameterName}', but no parameters are defined.");
			}
		}

		public LdapFilter ToFilter(LdapFilterParseOptions options = LdapFilterParseOptions.None, FilterExpressionContext? context = null)
		{
			context ??= NullFilterContext.Instance;
			Asn1FilterBuilder b = new Asn1FilterBuilder(options, context);
			return new LdapFilter(this.RootClause.Accept(b));
		}

		// [RFC 4515]
		public static FilterExpression Parse(string text)
		{
			ArgumentException.ThrowIfNullOrEmpty(text);

			List<FilterParameterUsage> paramUsages = new();
			var ctx = new ParseContext(paramUsages)
			{
				text = text,
			};

			var clause = ctx.ReadFilter();
			return new FilterExpression(clause, ImmutableArray.CreateRange(paramUsages));
		}

		// [RFC 4515]
		partial struct ParseContext
		{
			internal ParseContext(List<FilterParameterUsage> paramUsages)
			{
				this.paramUsages = paramUsages;
			}

			internal readonly List<FilterParameterUsage> paramUsages;

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

			private readonly FormatException CreateFormatException(string error, char actual, int position)
			{
				return new FormatException($"The LDAP filter is not in the expected format.  Character '{actual}' at position {position}: {error}");
			}

			private readonly FormatException CreateFormatException(string error, int position)
			{
				return new FormatException($"The LDAP filter is not in the expected format.  At position {position}: {error}");
			}

			private FilterClause? TryReadFilter()
			{
				return this.PeekNextChar() == '(' ? this.ReadFilter() : null;
			}

			internal FilterClause ReadFilter()
			{
				var startPos = this.readIndex;
				this.ReadExpected('(');

				FilterClause filter;

				var c = this.PeekNextChar();
				switch (c)
				{
					case '&':
					case '|':
						{
							this.ReadExpected((char)c);

							List<FilterClause> clauses = new List<FilterClause>();
							{
								FilterClause? clause;
								while ((clause = this.TryReadFilter()) != null)
								{
									clauses.Add(clause);
								}
							}

							if (clauses.Count == 0)
								throw CreateFormatException($"Expected one or more filter clauses introduced by '('", this.readIndex);

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

							bool isParam = false;
							{
								c = this.PeekNextChar();
								if (c == '@')
								{
									isParam = true;
									this.ReadNextChar();
								}
							}
							var filterType = this.ReadFilterType();
							string? extoid = null;
							if (filterType is FilterType.Extensible)
							{
								StringBuilder sbOid = new StringBuilder();
								char cx;
								// TODO: Allow symbolic names?
								static bool IsOidChar(char c) => c == '.' || char.IsDigit(c);
								while (IsOidChar(cx = this.ReadNextChar()))
								{
									sbOid.Append(cx);
								}
								if (cx != ':')
									throw CreateFormatException("Expected an OID of the form n.n followed by ':='.", cx, this.readIndex);

								this.ReadExpected('=');
								extoid = sbOid.ToString();
							}

							AssertionValue assertionValue;
							var assertionValueStr = this.ReadAssertionValue();
							{
								if (isParam)
								{
									this.paramUsages.Add(new FilterParameterUsage(assertionValueStr, attrDesc));

									assertionValue = new ParameterizedAssertionValue(assertionValueStr, attrDesc);
								}
								else
								{
									assertionValue = new LiteralAssertionValue(LdapAttribute.ParseSpecialValue(attrDesc, assertionValueStr));
								}
							}

							switch (filterType)
							{
								case FilterType.Equal:
									{
										Match mSubstr;
										if (assertionValueStr == "*")
											filter = HasAttribute(attrDesc);
										else if ((mSubstr = rgxSubstring.Match(assertionValueStr)).Success)
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
								case FilterType.Extensible:
									filter = ExtensibleMatch(attrDesc, extoid, assertionValue);
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
				Extensible = ':',
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
				else if (c1 is ':')
					return FilterType.Extensible;
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
		public static AndExpression All(FilterClause[] clauses)
		{
			ArgumentNullException.ThrowIfNull(clauses);
			if (clauses.Contains(null)) throw new ArgumentNullException(nameof(clauses));
			return new AndExpression(clauses);
		}
		public static OrExpression Any(FilterClause[] clauses)
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

		public static EqualsExpression Equal(string attributeDesc, AssertionValue assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new EqualsExpression(attributeDesc, assertionValue);
		}

		public static GreaterOrEqualExpression GreaterOrEqual(string attributeDesc, AssertionValue assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new GreaterOrEqualExpression(attributeDesc, assertionValue);
		}

		public static LessOrEqualExpression LessOrEqual(string attributeDesc, AssertionValue assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new LessOrEqualExpression(attributeDesc, assertionValue);
		}

		public static ApproxEqualExpression ApproxEqual(string attributeDesc, AssertionValue assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new ApproxEqualExpression(attributeDesc, assertionValue);
		}

		public static ExtensibleMatchExpression ExtensibleMatch(string attributeDesc, string extension, AssertionValue assertionValue)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(attributeDesc);
			ArgumentNullException.ThrowIfNull(assertionValue);

			return new ExtensibleMatchExpression(attributeDesc, extension, assertionValue);
		}
		#endregion
	}

	public abstract class AssertionValue
	{
		internal abstract string Resolve(FilterExpressionContext context);
	}

	public class LiteralAssertionValue : AssertionValue
	{
		public LiteralAssertionValue(string value)
		{
			ArgumentNullException.ThrowIfNull(value);
			LiteralValue = value;
		}

		public string LiteralValue { get; }

		internal override string Resolve(FilterExpressionContext context) => this.LiteralValue;
	}

	public class ParameterizedAssertionValue : AssertionValue
	{
		private readonly string attrDesc;

		public ParameterizedAssertionValue(string parameterName, string attrDesc)
		{
			ArgumentException.ThrowIfNullOrEmpty(parameterName);
			ParameterName = parameterName;
			this.attrDesc = attrDesc;
		}

		public string ParameterName { get; }

		internal override string Resolve(FilterExpressionContext context)
		{
			var text = context.ResolveParameter(this.ParameterName);
			text = LdapAttribute.ParseSpecialValue(attrDesc, text);
			return text;
		}
	}
}
