using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Titanis.Dynamic;
using Titanis.Reflection;

namespace Titanis.Mocks
{
	enum ExpectationType
	{
		Action = 0,
		Func,
		AsyncAction,
		AsyncFunc,
		Setter,
	}

	/// <summary>
	/// Describes a pattern to match a method call to an expectation.
	/// </summary>
	class ExpectPattern
	{
		public ExpectPattern(MethodInfo method, IList<Expression>? argExprs)
		{
			this.method = method;
			this.args = argExprs;

			if (argExprs != null && argExprs.Count > 0)
			{
				var parms = method.GetParameters();
				var comparers = this.comparers = new Func<object, bool>?[argExprs.Count];
				for (int i = 0; i < comparers.Length; i++)
				{
					var parm = parms[i];
					var argExpr = argExprs[i];

					var parmAttrs = parm.Attributes;
					bool isOut = 0 != (parmAttrs & ParameterAttributes.Out);
					bool isIn = !isOut || (0 != (parmAttrs & ParameterAttributes.In));

					if (isIn)
					{
						Func<object, bool>? comparer;
						if (argExpr.NodeType == ExpressionType.Constant && argExpr is ConstantExpression constExpr)
						{
							var comparand = constExpr.Value;
							comparer = CreateConstComparer(comparand);
						}
						else
						{
							if (
								argExpr.NodeType == ExpressionType.Call
								&& (argExpr is MethodCallExpression { Method: { DeclaringType: var type } calledMethod } call)
								&& (type == typeof(Arg))
								)
							{
								if (calledMethod.Name == nameof(Arg.Any))
									comparer = null;
								else if (calledMethod.Name == nameof(Arg.Matches))
								{
									var comparerExpr = call.Arguments[0];
									ParameterExpression comparandArg = Expression.Parameter(typeof(object));
									var invoker = Expression.Invoke(comparerExpr, Expression.Convert(comparandArg, parm.ParameterType));
									var invokerLambda = Expression.Lambda(invoker, comparandArg);

									comparer = (Func<object, bool>)invokerLambda.Compile();
								}
								else
									// Should never happen
									throw new NotSupportedException();
							}
							else
							{
								ParameterExpression paramExpr = Expression.Parameter(typeof(object));
								if (argExpr.Type.GetTypeInfo().IsValueType)
									argExpr = Expression.Convert(argExpr, typeof(object));

								LambdaExpression lambda = Expression.Lambda(Expression.Call(
									ReflectionHelper.MethodOf<object, object, bool>((x, y) => CompareArg(x, y)),
									paramExpr,
									argExpr
									), paramExpr);
								comparer = (Func<object, bool>)lambda.Compile();
							}
						}

						comparers[i] = comparer;
					}
				}
			}
		}

		private static Func<object, bool> CreateConstComparer(object comparand)
		{
			return x => EqualityComparer<object>.Default.Equals(comparand, x);
		}

		private static bool CompareArg(object comparand, object arg)
		{
			return EqualityComparer<object>.Default.Equals(comparand, arg);
		}

		internal readonly MethodInfo method;
		private readonly IList<Expression>? args;
		private readonly Func<object, bool>?[] comparers;
		private int ArgCount => (this.args == null) ? 0 : this.args.Count;

		internal bool Matches(MethodCallMessage message)
		{
			if (message.Method == this.method)
			{
				var argCount = this.ArgCount;
				if (message.ArgCount == argCount)
				{
					for (int i = 0; i < argCount; i++)
					{
						var arg = message.GetArgument(i);
						bool argMatches = Matches(arg, i);
						if (!argMatches)
							return false;
					}

					return true;
				}
			}

			return false;
		}

		private bool Matches(object arg, int argIndex)
		{
			var comparer = this.comparers[argIndex];
			return (comparer != null) ? comparer(arg) : true;
		}

		private static bool MatchesConstant(object arg, ConstantExpression argPattern)
			=> EqualityComparer<object>.Default.Equals(arg, argPattern.Value);
	}

	internal class Expectation : IExpect
	{
		internal Exception? _exception;
		internal int calledCount;

		internal Expectation(ExpectPattern pattern, ExpectationFlags flags)
		{
			this.pattern = pattern;
			this._flags = flags;
		}

		private protected ExpectationFlags _flags;
		private readonly ExpectPattern pattern;

		internal bool HasResult => (0 != (this._flags & ExpectationFlags.ResultMask));
		internal bool HasReturnValue => (0 != (this._flags & ExpectationFlags.ReturnValueSet));
		internal bool CallsBase => (0 != (this._flags & ExpectationFlags.CallBaseSet));
		internal bool HasException => (0 != (this._flags & ExpectationFlags.ExceptionSet));

		public void Throw(Exception? ex)
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);

			this._exception = ex;
			this._flags |= ExpectationFlags.ExceptionSet;
		}

		internal bool Matches(MethodCallMessage message)
			=> this.pattern.Matches(message);

		internal void HandleCall(MethodCallMessage message)
		{
			this.calledCount++;
			if (this.HasException)
				throw this._exception!;
			else if (this.HasReturnValue)
				message.returnValue = this.ReturnValue;
			else if (this.CallsBase)
				message.callBase = true;
			else if (this.MustReturnValue)
				throw new InvalidOperationException(string.Format(Messages.Expectation_NoResultSet, this.pattern.method.Name, this.pattern.method.DeclaringType.FullName));
		}

		public void CallBase()
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);
			if (this.pattern.method.IsAbstract)
				throw new InvalidOperationException(string.Format(Messages.Expectation_CannotCallBaseAbstractMethod, this.pattern.method.Name, this.pattern.method.DeclaringType.FullName));

			this._flags |= ExpectationFlags.CallBaseSet;
		}

		internal virtual object? ReturnValue => null;
		internal bool MustReturnValue => (0 == (this._flags & ExpectationFlags.NoReturnValue));
	}

	[Flags]
	enum ExpectationFlags
	{
		None = 0,

		ReturnValueSet = 1,
		ExceptionSet = 2,
		CallBaseSet = 4,
		NoReturnValue = 8,
		ResultMask = ReturnValueSet | ExceptionSet | CallBaseSet,
	}

	//internal class Expectation<TInstance> : Expectation, IExpect<TInstance, TReturn>
	//{

	//}

	internal class Expectation<TInstance, TReturn> : Expectation, IExpect<TInstance, TReturn>
	{
		internal Expectation(ExpectPattern pattern, ExpectationFlags flags)
			: base(pattern, flags)
		{

		}

		private TReturn? _returnValue;

		internal override object? ReturnValue => this._returnValue;

		public void Return(TReturn value)
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);

			this._returnValue = value;
			this._flags |= ExpectationFlags.ReturnValueSet;
		}
	}

	internal class ExpectationAsync<TInstance> : Expectation<TInstance, Task>, IExpectAsync
	{
		internal ExpectationAsync(ExpectPattern pattern)
			: base(pattern, ExpectationFlags.None)
		{
		}

		public void ThrowAsync(Exception ex)
		{
			base.Return(Task.FromException(ex));
		}
	}

	internal class ExpectationAsync<TInstance, TReturn> : Expectation<TInstance, Task<TReturn>>, IExpectAsync<TInstance, TReturn>
	{
		internal ExpectationAsync(ExpectPattern pattern)
			: base(pattern, ExpectationFlags.None)
		{
		}

		public void ReturnAsync(TReturn value)
		{
			base.Return(Task.FromResult(value));
		}

		public void ThrowAsync(Exception ex)
		{
			base.Return(Task.FromException<TReturn>(ex));
		}
	}
}