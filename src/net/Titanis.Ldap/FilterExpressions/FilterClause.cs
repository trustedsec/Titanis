using Lightweight_Directory_Access_Protocol_V3;
using System.Text;

namespace Titanis.Ldap.FilterExpressions
{

	public abstract partial class FilterClause
	{
		public virtual FilterClause Not() => new NotExpression(this);

		public abstract T Accept<T>(IFilterVisitor<T> visitor);
	}

	public sealed class NotExpression : FilterClause
	{
		internal NotExpression(FilterClause operand)
		{
			Operand = operand;
		}

		public FilterClause Operand { get; }

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}

	public sealed class AndExpression : FilterClause
	{
		internal AndExpression(FilterClause[] clauses)
		{
			Clauses = clauses;
		}
		public FilterClause[] Clauses { get; }

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}

	public sealed class OrExpression : FilterClause
	{
		internal OrExpression(FilterClause[] clauses)
		{
			Clauses = clauses;
		}
		public FilterClause[] Clauses { get; }

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}

	public sealed class PresentExpression : FilterClause
	{
		internal PresentExpression(string attributeDescription)
		{
			AttributeDescription = attributeDescription;
		}

		public string AttributeDescription { get; }

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}

	public abstract class AssertionExpression : FilterClause
	{
		internal AssertionExpression(string attributeDescription, AssertionValue assertionValue)
		{
			AttributeDescription = attributeDescription;
			AssertionValue = assertionValue;
		}

		public string AttributeDescription { get; }
		public AssertionValue AssertionValue { get; }

		internal AttributeValueAssertion ToAssertion(Asn1FilterBuilder b, FilterExpressionContext context)
		{
			string value = this.AssertionValue.Resolve(context);
			var attrSyntax = LdapAttributeTypes.TryGetByNameOrOid(this.AttributeDescription)?.Syntax;
			byte[] encodedValue;
			if (attrSyntax != null)
			{
				encodedValue = attrSyntax.ParseAndEncode(value);
			}
			else
			{
				encodedValue = Encoding.UTF8.GetBytes(value);
			}
			return new AttributeValueAssertion(b.GetAttrBytes(this.AttributeDescription), encodedValue);
		}
	}
	public sealed class EqualsExpression : AssertionExpression
	{
		internal EqualsExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
	public sealed class SubstringMatchExpression : FilterClause
	{
		internal SubstringMatchExpression(string attributeDescription, string? initial, string[]? any, string? final)
		{
			this.attributeDescription = attributeDescription;
			this.initial = initial;
			this.any = any;
			this.final = final;
		}

		internal readonly string attributeDescription;
		internal readonly string? initial;
		internal readonly string[]? any;
		internal readonly string? final;

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
	public sealed class GreaterOrEqualExpression : AssertionExpression
	{
		internal GreaterOrEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
	public sealed class LessOrEqualExpression : AssertionExpression
	{
		internal LessOrEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
	public sealed class ApproxEqualExpression : AssertionExpression
	{
		internal ApproxEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
	public sealed class ExtensibleMatchExpression : AssertionExpression
	{
		internal ExtensibleMatchExpression(string attributeDescription, string extension, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
			Extension = extension;
		}

		public string Extension { get; }

		public sealed override T Accept<T>(IFilterVisitor<T> visitor) => visitor.Visit(this);
	}
}
