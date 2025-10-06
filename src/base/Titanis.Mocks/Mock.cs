using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Titanis.Dynamic;
using Titanis.Reflection;

namespace Titanis.Mocks
{
	public enum MockBehavior
	{
		Loose = 0,
		Struct,
		Ordered
	}

	public class Mock<T> : IStubHandler
		where T : class
	{
		private T _stubObj;

		internal Mock(T stubObj, MockBehavior behavior)
		{
			this._stubObj = stubObj;
			this.Behavior = behavior;
			IStub stub = (IStub)stubObj;
			stub.SetHandler(this);
		}

		public T Object => this._stubObj;

		public MockBehavior Behavior { get; }

		void IStubHandler.HandleCall(MethodCallMessage message)
		{
			message.callBase = true;

			Expectation exp = this.FindExpectationFor(message);
			// TODO: Throw exception
			exp.HandleCall(message);
		}

		private Expectation? FindExpectationFor(MethodCallMessage message)
		{
			foreach (var exp in this._expectations)
			{
				if (exp.Matches(message))
					return exp;
			}

			return null;
		}

		private List<Expectation> _expectations = new List<Expectation>();

		private Expectation<T, TReturn> ExpectInternal<TReturn>(
			LambdaExpression lambda,
			ExpectationType type)
		{
			var pattern = CheckLambda(lambda, type);
			ExpectationFlags flags = (type == ExpectationType.Action) ? ExpectationFlags.NoReturnValue : ExpectationFlags.None;
			Expectation<T, TReturn> expect = new Expectation<T, TReturn>(pattern, flags);
			this._expectations.Add(expect);

			return expect;
		}

		private static PropertyInfo? CheckPropertySetter(LambdaExpression lambda)
		{
			var body = lambda.Body;
			if (body.NodeType == ExpressionType.MemberAccess
				&& body is MemberExpression { Member: var member })
			{
				if (member is PropertyInfo propertyInfo)
				{
					return propertyInfo;
				}
			}
			return null;
		}

		private Expectation ExpectSetInternal<TValue>(
			Expression<Func<T, TValue>> lambda,
			Expression<Func<TValue, bool>> comparerExpr)
		{
			var property = CheckPropertySetter(lambda);
			if (property == null)
				throw new ArgumentException(Messages.Mock_InvalidPropertyExpectation, nameof(lambda));

			var pattern = CheckLambda(lambda, ExpectationType.Setter);
			Expectation expect = new Expectation(new ExpectPattern(property.SetMethod, new Expression[] {
				Expression.Call(ReflectionHelper.MethodOf<Func<TValue,bool>,TValue>(x=>Arg.Matches(x)), comparerExpr)
			}), ExpectationFlags.NoReturnValue);
			this._expectations.Add(expect);

			return expect;
		}

		private ExpectationAsync<T> ExpectAsyncInternal(LambdaExpression lambda, ExpectationType type)
		{
			var pattern = CheckLambda(lambda, type);
			ExpectationAsync<T> expect = new ExpectationAsync<T>(pattern);
			this._expectations.Add(expect);

			return expect;
		}

		private ExpectationAsync<T, TReturn> ExpectAsyncInternal<TReturn>(LambdaExpression lambda, ExpectationType type)
		{
			var pattern = CheckLambda(lambda, type);
			ExpectationAsync<T, TReturn> expect = new ExpectationAsync<T, TReturn>(pattern);
			this._expectations.Add(expect);

			return expect;
		}

		private static ExpectPattern CheckLambda(LambdaExpression lambda, ExpectationType type)
		{
			if (lambda is null) throw new ArgumentNullException(nameof(lambda));

			var body = lambda.Body;
			if (body.NodeType == ExpressionType.Call
				&& (body is MethodCallExpression call))
			{
				var method = call.Method;
				if (!method.IsVirtual)
					throw new ArgumentException(string.Format(method.Name, method.DeclaringType.FullName), nameof(lambda));
				return new ExpectPattern(method, call.Arguments);
			}
			else if (body.NodeType == ExpressionType.MemberAccess
				&& body is MemberExpression { Member: var member })
			{
				if (member is PropertyInfo propertyInfo)
				{
					if (type == ExpectationType.Setter)
						return new ExpectPattern(propertyInfo.SetMethod, null);
					else if (type is ExpectationType.MethodCall)
						return new ExpectPattern(propertyInfo.GetMethod, null);
				}
			}
			throw new ArgumentException(Messages.Mock_InvalidExpectionExpression, nameof(lambda));
		}

		public IExpect ExpectSet<TValue>(Expression<Func<T, TValue>> properteyGetter, TValue expectedValue)
			=> this.ExpectSetInternal(properteyGetter, x => EqualityComparer<TValue>.Default.Equals(x, expectedValue));
		public IExpect ExpectSet<TValue>(Expression<Func<T, TValue>> properteyGetter, Expression<Func<TValue, bool>> expectedValueComparer)
			=> this.ExpectSetInternal(properteyGetter, expectedValueComparer);

		public IExpect Expect(Expression<Action<T>> lambda)
			=> this.ExpectInternal<int>(lambda, ExpectationType.Action);

		public IExpect<T, TReturn> Expect<TReturn>(Expression<Func<T, TReturn>> lambda)
			=> this.ExpectInternal<TReturn>(lambda, ExpectationType.MethodCall);

		public IExpectAsync Expect(Expression<Func<T, Task>> lambda)
			=> this.ExpectAsyncInternal(lambda, ExpectationType.AsyncAction);

		public IExpectAsync<T, TReturn> Expect<TReturn>(Expression<Func<T, Task<TReturn>>> lambda)
			=> this.ExpectAsyncInternal<TReturn>(lambda, ExpectationType.AsyncFunc);
	}
}