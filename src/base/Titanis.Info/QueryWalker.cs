using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Titanis.Info.Schema;

namespace Titanis.Info
{
	internal abstract class InfoQuery : IQueryable
	{
		protected InfoQuery(
			Expression expression,
			InfoQueryProvider provider
			)
		{
			this.Expression = expression;
			this.Provider = provider;
		}

		public abstract Type ElementType { get; }

		public Expression Expression { get; }

		public IQueryProvider Provider { get; }

		public abstract IEnumerator GetUntypedEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetUntypedEnumerator();
	}


	class ItemTable<T> : IQueryable<T>
	{
		internal ItemTable(InfoQueryProvider provider)
		{
			this.Expression = Expression.Constant(this, typeof(IQueryable<T>));
			Provider = provider;
		}

		public Type ElementType => typeof(T);

		public Expression Expression { get; }

		public IQueryProvider Provider { get; }

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	internal class InfoQueryProvider : IQueryProvider
	{
		private readonly InfoBase infobase;

		internal InfoQueryProvider(InfoBase infobase)
		{
			this.infobase = infobase;
		}

		public IQueryable CreateQuery(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (expression is null) throw new ArgumentNullException(nameof(expression));
			return new InfoQuery<TElement>(expression, this);
		}

		public object Execute(Expression expression)
		{
			throw new NotImplementedException();
		}

		public TResult Execute<TResult>(Expression expression)
		{
			throw new NotImplementedException();
		}

		public IQueryable<ItemData> AllItems => new ItemTable<ItemData>(this);
	}

	internal class InfoQuery<T> : InfoQuery, IQueryable<T>
	{
		public InfoQuery(Expression expression, InfoQueryProvider provider) : base(expression, provider)
		{
		}

		public override Type ElementType => typeof(T);

		public sealed override IEnumerator GetUntypedEnumerator() => this.GetEnumerator();

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}

	public class QueryInfo
	{
		internal LambdaExpression? predicateLambda;
	}

	internal class QueryWalker : ExpressionVisitor
	{
		internal QueryWalker(ParameterExpression parameter)
		{
			this.parameter = parameter;
		}

		private readonly ParameterExpression parameter;

		static class SingletonParam<T>
		{
			internal static ParameterExpression Parameter = Expression.Parameter(typeof(T), "item");
		}

		public static Expression<Func<T, bool>> ExtractPredicate<T>(IQueryable<T> query)
		{
			if (query is null) throw new ArgumentNullException(nameof(query));

			var walker = new QueryWalker(SingletonParam<T>.Parameter);
			walker.Visit(query.Expression);
			return (Expression<Func<T, bool>>)walker._queryInfo.predicateLambda;
		}

		class LambdaFrame
		{
			internal LambdaExpression lambda;
		}

		private LambdaFrame? _lambda;
		private QueryInfo _queryInfo = new QueryInfo();

		protected override Expression VisitLambda<T>(Expression<T> node)
		{
			var prev = this._lambda;
			try
			{
				this._lambda = new LambdaFrame { lambda = node };
				return base.VisitLambda(node);
			}
			finally
			{
				this._lambda = prev;
			}
		}

		protected override Expression VisitParameter(ParameterExpression node)
		{
			if (node == this._lambda.lambda.Parameters[0])
				return this.parameter;
			else
				return base.VisitParameter(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			var method = node.Method;
			if (method.DeclaringType == typeof(Queryable) && method.Name == "Where")
			{
				var source = this.Visit(node.Arguments[0]);

				var lambda = (LambdaExpression)this.Visit(((UnaryExpression)node.Arguments[1]).Operand);
				this._queryInfo.predicateLambda =
					(this._queryInfo.predicateLambda == null) ? lambda
					: Expression.Lambda(Expression.AndAlso(this._queryInfo.predicateLambda.Body, lambda.Body), this.parameter);

				return Expression.Call(method, source, Expression.Quote(this._queryInfo.predicateLambda));
			}
			else
			{
				throw new NotImplementedException();
			}
		}
	}
}
