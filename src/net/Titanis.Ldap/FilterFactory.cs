using Lightweight_Directory_Access_Protocol_V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Ldap.FilterExpressions;

namespace Titanis.Ldap
{
	/// <summary>
	/// Represents a filter in an LDAP query.
	/// </summary>
	/// <seealso cref="LdapQuery.Filter"/>
	/// <seealso cref="FilterFactory"/>
	public class LdapFilter
	{
		internal readonly Filter struc;

		internal LdapFilter(Filter struc)
		{
			this.struc = struc;
		}

		public static LdapFilter Parse(string filterExpression, LdapFilterParseOptions options = LdapFilterParseOptions.None)
		{
			return FilterExpression.Parse(filterExpression).ToFilter(options);
		}
	}

	public static class FilterFactory
	{
		/// <summary>
		/// Constructs a search filter that matches any entry.
		/// </summary>
		public static LdapFilter Any() => FilterFactory.Present(LdapAttributeTypes.ObjectClass);
		/// <summary>
		/// Constructs a search filter that matches entries with an attribute present.
		/// </summary>
		/// <param name="attribute">Attribute that must be present</param>
		public static LdapFilter Present(AttributeSpec attribute) => new LdapFilter(new Filter() { Present = attribute.rep });
		/// <summary>
		/// Constructs a search filter that matches entries with an attribute equal to a value.
		/// </summary>
		/// <param name="attribute">Attribute to test</param>
		/// <param name="value">Match value</param>
		public static LdapFilter Matches(AttributeTypeDescription attribute, object? value) => new LdapFilter(new Filter() { EqualityMatch = new AttributeValueAssertion(attribute.EncodedName, attribute.Syntax.Encode(value)) });
		/// <summary>
		/// Constructs a search filter that matches entries with an attribute that contains a substring.
		/// </summary>
		/// <param name="attribute">Attribute to test</param>
		/// <param name="substring">Substring</param>
		public static LdapFilter ContainsSubstring(AttributeTypeDescription attribute, string substring)
		{
			ArgumentNullException.ThrowIfNull(attribute);
			ArgumentException.ThrowIfNullOrEmpty(substring);

			var bytes = attribute.Syntax.Encode(substring);

			return new LdapFilter(new Filter() { Substrings = new SubstringFilter(attribute.EncodedName, [new SubstringFilter_Substrings_Element() { Any = bytes }]) });
		}
		/// <summary>
		/// Constructs a search filter that matches entries with an attribute that begins with a substring.
		/// </summary>
		/// <param name="attribute">Attribute to test</param>
		/// <param name="substring">Substring</param>
		public static LdapFilter BeginsWithSubstring(AttributeTypeDescription attribute, string substring)
		{
			ArgumentNullException.ThrowIfNull(attribute);
			ArgumentException.ThrowIfNullOrEmpty(substring);

			var bytes = attribute.Syntax.Encode(substring);

			return new LdapFilter(new Filter() { Substrings = new SubstringFilter(attribute.EncodedName, [new SubstringFilter_Substrings_Element() { Initial = bytes }]) });
		}
		/// <summary>
		/// Constructs a search filter that matches entries with an attribute that ends with a substring.
		/// </summary>
		/// <param name="attribute">Attribute to test</param>
		/// <param name="substring">Substring</param>
		public static LdapFilter EndsWithSubstring(AttributeTypeDescription attribute, string substring)
		{
			ArgumentNullException.ThrowIfNull(attribute);
			ArgumentException.ThrowIfNullOrEmpty(substring);

			var bytes = attribute.Syntax.Encode(substring);

			return new LdapFilter(new Filter() { Substrings = new SubstringFilter(attribute.EncodedName, [new SubstringFilter_Substrings_Element() { Final = bytes }]) });
		}

		/// <summary>
		/// Constructs a search filter that matches entries that match at least one condition.
		/// </summary>
		/// <param name="filter">First condition</param>
		/// <param name="other">Other condition</param>
		public static LdapFilter Or(this LdapFilter filter, LdapFilter other)
		{
			ArgumentNullException.ThrowIfNull(filter);
			ArgumentNullException.ThrowIfNull(other);

			return new LdapFilter(new Filter
			{
				Or = [filter.struc, other.struc]
			});
		}

		/// <summary>
		/// Constructs a search filter that matches entries that match multiple conditions.
		/// </summary>
		/// <param name="filter">First condition</param>
		/// <param name="other">Other condition</param>
		public static LdapFilter And(this LdapFilter filter, LdapFilter other)
		{
			ArgumentNullException.ThrowIfNull(filter);
			ArgumentNullException.ThrowIfNull(other);

			return new LdapFilter(new Filter
			{
				And = [filter.struc, other.struc]
			});
		}

		internal const string LDAP_MATCHING_RULE_BIT_AND = "1.2.840.113556.1.4.803";
		internal const string LDAP_MATCHING_RULE_BIT_OR = "1.2.840.113556.1.4.804";
		internal const string LDAP_MATCHING_RULE_TRANSITIVE_EVAL = "1.2.840.113556.1.4.1941";
		internal const string LDAP_MATCHING_RULE_DN_WITH_DATA = "1.2.840.113556.1.4.2253";
	}

	public struct AttributeSpec
	{
		public AttributeSpec(string name)
		{
			ArgumentNullException.ThrowIfNull(name);
			this.rep = Encoding.UTF8.GetBytes(name);
		}
		public AttributeSpec(AttributeTypeDescription attribute)
		{
			ArgumentNullException.ThrowIfNull(attribute);
			this.rep = attribute.EncodedName;
		}

		internal byte[] rep;

		public override string ToString() => Encoding.UTF8.GetString(this.rep);

		public static implicit operator AttributeSpec(string name) => new AttributeSpec(name);
		public static implicit operator AttributeSpec(AttributeTypeDescription attribute) => new AttributeSpec(attribute);
	}


}
