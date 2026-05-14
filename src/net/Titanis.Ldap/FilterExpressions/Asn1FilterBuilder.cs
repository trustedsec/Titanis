using Lightweight_Directory_Access_Protocol_V3;
using System.Text;

namespace Titanis.Ldap.FilterExpressions
{
	public interface IFilterVisitor<T>
	{
		T Visit(NotExpression expression);
		T Visit(AndExpression expression);
		T Visit(OrExpression expression);
		T Visit(PresentExpression expression);
		T Visit(EqualsExpression expression);
		T Visit(SubstringMatchExpression expression);
		T Visit(GreaterOrEqualExpression expression);
		T Visit(LessOrEqualExpression expression);
		T Visit(ApproxEqualExpression expression);
		T Visit(ExtensibleMatchExpression expression);
	}

	class Asn1FilterBuilder : IFilterVisitor<Filter>
	{
		internal Asn1FilterBuilder(FilterExpressionContext context)
		{
			this.context = context;
		}

		private readonly FilterExpressionContext context;

		public Filter Visit(NotExpression expression) => new Filter { Not = expression.Operand.Accept(this) };
		public Filter Visit(AndExpression expression) => new Filter { And = Array.ConvertAll(expression.Clauses, r => r.Accept(this)) };
		public Filter Visit(OrExpression expression) => new Filter { Or = Array.ConvertAll(expression.Clauses, r => r.Accept(this)) };
		public Filter Visit(PresentExpression expression) => new Filter() { Present = Encoding.UTF8.GetBytes(expression.AttributeDescription) };
		public Filter Visit(EqualsExpression expression) => new Filter() { EqualityMatch = expression.ToAssertion(context) };
		public Filter Visit(SubstringMatchExpression expression)
		{
			List<SubstringFilter_Substrings_Element> elems = new List<SubstringFilter_Substrings_Element>();
			if (!string.IsNullOrEmpty(expression.initial))
				elems.Add(new SubstringFilter_Substrings_Element() { Initial = Encoding.UTF8.GetBytes(expression.initial) });
			if (!expression.any.IsNullOrEmpty())
			{
				foreach (var any in expression.any)
				{
					elems.Add(new SubstringFilter_Substrings_Element() { Any = Encoding.UTF8.GetBytes(any) });
				}
			}

			if (!string.IsNullOrEmpty(expression.final))
				elems.Add(new SubstringFilter_Substrings_Element() { Final = Encoding.UTF8.GetBytes(expression.final) });

			return new Filter()
			{
				Substrings = new SubstringFilter(Encoding.UTF8.GetBytes(expression.attributeDescription), elems.ToArray())
			};
		}
		public Filter Visit(GreaterOrEqualExpression expression) => new Filter() { GreaterOrEqual = expression.ToAssertion(context) };
		public Filter Visit(LessOrEqualExpression expression) => new Filter() { LessOrEqual = expression.ToAssertion(context) };
		public Filter Visit(ApproxEqualExpression expression) => new Filter() { ApproxMatch = expression.ToAssertion(context) };
		public Filter Visit(ExtensibleMatchExpression expression) => new Filter() { ExtensibleMatch = new MatchingRuleAssertion(Encoding.UTF8.GetBytes(expression.AssertionValue.Resolve(context)), Encoding.UTF8.GetBytes(expression.Extension), Encoding.UTF8.GetBytes(expression.AttributeDescription)) };
	}
}
