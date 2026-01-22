using Lightweight_Directory_Access_Protocol_V3;
using System.Text;

namespace Titanis.Ldap.FilterExpressions
{
	public abstract partial class FilterClause
	{
		internal abstract Filter ToFilterAsn1(FilterExpressionContext context);

		public virtual FilterClause Not() => new NotExpression(this);
	}

	public sealed class NotExpression : FilterClause
	{
		internal NotExpression(FilterClause operand)
		{
			Operand = operand;
		}

		public FilterClause Operand { get; }

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter { Not = this.Operand.ToFilterAsn1(context) };
	}

	public sealed class AndExpression : FilterClause
	{
		internal AndExpression(FilterClause[] clauses)
		{
			Clauses = clauses;
		}
		public FilterClause[] Clauses { get; }

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter { And = Array.ConvertAll(this.Clauses, r => r.ToFilterAsn1(context)) };
	}

	public sealed class OrExpression : FilterClause
	{
		internal OrExpression(FilterClause[] clauses)
		{
			Clauses = clauses;
		}
		public FilterClause[] Clauses { get; }

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter { Or = Array.ConvertAll(this.Clauses, r => r.ToFilterAsn1(context)) };
	}

	public sealed class PresentExpression : FilterClause
	{
		internal PresentExpression(string attributeDescription)
		{
			AttributeDescription = attributeDescription;
		}

		public string AttributeDescription { get; }

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { Present = Encoding.UTF8.GetBytes(this.AttributeDescription) };
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

		private protected AttributeValueAssertion ToAssertion(FilterExpressionContext context)
		{
			string value = this.AssertionValue.Resolve(context);
			return new AttributeValueAssertion(Encoding.UTF8.GetBytes(this.AttributeDescription), Encoding.UTF8.GetBytes(value));
		}
	}
	public sealed class EqualsExpression : AssertionExpression
	{
		internal EqualsExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { EqualityMatch = this.ToAssertion(context) };
	}
	public sealed class SubstringMatchExpression : FilterClause
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

		internal override Filter ToFilterAsn1(FilterExpressionContext context)
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
		internal GreaterOrEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { GreaterOrEqual = this.ToAssertion(context) };
	}
	public sealed class LessOrEqualExpression : AssertionExpression
	{
		internal LessOrEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { LessOrEqual = this.ToAssertion(context) };
	}
	public sealed class ApproxEqualExpression : AssertionExpression
	{
		internal ApproxEqualExpression(string attributeDescription, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
		}

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { ApproxMatch = this.ToAssertion(context) };
	}
	public sealed class ExtensibleMatchExpression : AssertionExpression
	{
		internal ExtensibleMatchExpression(string attributeDescription, string extension, AssertionValue assertionValue)
			: base(attributeDescription, assertionValue)
		{
			Extension = extension;
		}

		public string Extension { get; }

		internal override Filter ToFilterAsn1(FilterExpressionContext context) => new Filter() { ExtensibleMatch = new MatchingRuleAssertion(Encoding.UTF8.GetBytes(this.AssertionValue.Resolve(context)), Encoding.UTF8.GetBytes(this.Extension), Encoding.UTF8.GetBytes(this.AttributeDescription)) };
	}
}
