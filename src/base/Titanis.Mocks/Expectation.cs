using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Titanis.Dynamic;
using Titanis.Reflection;

namespace Titanis.Mocks
{
	enum ExpectationType
	{
		Action = 0,
		MethodCall,
		AsyncAction,
		AsyncFunc,
		Setter,
	}

	[Flags]
	public enum ExpectationOptions
	{
		None = 0,
		MultipleCall = 1,
	}

	/// <summary>
	/// Describes a pattern to match a method call to an expectation.
	/// </summary>
	class ExpectPattern
	{
		public ExpectPattern(MethodInfo method, IList<Expression>? argExprs)
		{
			this._method = method;
			this._args = argExprs;

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
									var invoker = Expression.Invoke(comparerExpr, Expression.Convert(comparandArg, call.Method.ReturnType));
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

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"{this._method.DeclaringType.Name}.{this._method.Name}(");
			if (this._args != null)
			{
				foreach (var arg in this._args)
				{
					sb.Append($", {arg}");
				}
			}
			sb.Append(')');
			return sb.ToString();
		}

		private static Func<object, bool> CreateConstComparer(object comparand)
		{
			return x => EqualityComparer<object>.Default.Equals(comparand, x);
		}

		private static bool CompareArg(object comparand, object arg)
		{
			return EqualityComparer<object>.Default.Equals(comparand, arg);
		}

		internal readonly MethodInfo _method;
		private readonly IList<Expression>? _args;
		private readonly Func<object, bool>?[] comparers;
		private int ArgCount => (this._args == null) ? 0 : this._args.Count;

		internal bool Matches(MethodCallMessage message)
		{
			if (message.Method == this._method)
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
		private List<Action<MethodCallMessage>>? _callbacks;

		internal Expectation(ExpectPattern pattern, ExpectationFlags flags, ExpectationOptions options)
		{
			this._pattern = pattern;
			this._flags = flags;
			this._options = options;
		}

		private protected ExpectationFlags _flags;
		private readonly ExpectationOptions _options;
		private readonly ExpectPattern _pattern;

		public override string ToString() => this._pattern.ToString();

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

		public void AddCallback(Action<MethodCallMessage> callback)
		{
			if (callback is null) throw new ArgumentNullException(nameof(callback));
			(this._callbacks ??= new List<Action<MethodCallMessage>>()).Add(callback);
		}

		public IExpect Do(Action<object[]> callback)
		{
			this.AddCallback(r => callback(r.GetArguments()));
			return this;
		}

		public IExpect Do<TArg>(Action<TArg> callback)
		{
			this.AddCallback(r => callback((TArg)r.GetArgument(0)));
			return this;
		}

		internal bool Matches(MethodCallMessage message)
			=> this._pattern.Matches(message);

		internal void HandleCall(MethodCallMessage message)
		{
			this.calledCount++;
			this.MarkMet();

			this._callbacks?.ForEach(r => r.Invoke(message));

			if (this.HasException)
				throw this._exception!;
			else if (this.HasReturnValue)
				message.returnValue = this.GetReturnValue(message);
			else if (this.CallsBase)
				message.callBase = true;
			else if (this.MustReturnValue)
				throw new InvalidOperationException(string.Format(Messages.Expectation_NoResultSet, this._pattern._method.Name, this._pattern._method.DeclaringType.FullName));
		}

		public void CallBase()
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);
			if (this._pattern._method.IsAbstract)
				throw new InvalidOperationException(string.Format(Messages.Expectation_CannotCallBaseAbstractMethod, this._pattern._method.Name, this._pattern._method.DeclaringType.FullName));

			this._flags |= ExpectationFlags.CallBaseSet;
		}

		internal virtual object? GetReturnValue(MethodCallMessage message) => null;

		public bool HasBeenMet { get; private set; }

		internal void MarkMet()
		{
			this.HasBeenMet = true;
		}

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
		internal Expectation(ExpectPattern pattern, ExpectationFlags flags, ExpectationOptions options)
			: base(pattern, flags, options)
		{

		}

		private TReturn? _returnValue;
		private Func<object[], TReturn>? _returnValueFunc;

		internal override object? GetReturnValue(MethodCallMessage methodCall)
		{
			if (this._returnValueFunc != null)
				return this._returnValueFunc(methodCall.GetArguments());
			else
				return this._returnValue;
		}

		public void Return(TReturn value)
		{
			EnsureResultNotSet();

			this._returnValue = value;
			this._flags |= ExpectationFlags.ReturnValueSet;
		}

		private void EnsureResultNotSet()
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);
		}

		public void Return(Func<object[], TReturn> valueFunc)
		{
			if (this.HasResult)
				throw new InvalidOperationException(Messages.Expectation_ResultAlreadySet);

			this._returnValueFunc = valueFunc;
			this._flags |= ExpectationFlags.ReturnValueSet;
		}

		IExpect<TInstance> IExpect<TInstance>.Do(Action<object[]> callback) => this.Do(callback);
		IExpect<TInstance> IExpect<TInstance>.Do<TArg>(Action<TArg> callback) => this.Do(callback);

		public new IExpect<TInstance, TReturn> Do(Action<object[]> callback)
		{
			base.AddCallback(r => callback(r.GetArguments()));
			return this;
		}

		public new IExpect<TInstance, TReturn> Do<TArg>(Action<TArg> callback)
		{
			base.AddCallback(r => callback((TArg)r.GetArgument(0)));
			return this;
		}
	}

	internal class ExpectationAsync<TInstance> : Expectation<TInstance, Task>, IExpectAsync
	{
		internal ExpectationAsync(ExpectPattern pattern, ExpectationOptions options)
			: base(pattern, ExpectationFlags.None, options)
		{
		}

		public void ThrowAsync(Exception ex)
		{
			base.Return(Task.FromException(ex));
		}
	}

	internal class ExpectationAsync<TInstance, TReturn> : Expectation<TInstance, Task<TReturn>>, IExpectAsync<TInstance, TReturn>
	{
		internal ExpectationAsync(ExpectPattern pattern, ExpectationOptions options)
			: base(pattern, ExpectationFlags.None, options)
		{
		}

		public void ReturnAsync(TReturn value)
		{
			base.Return(Task.FromResult(value));
		}

		public void ReturnAsync(Func<object[], Task<TReturn>> valueFunc)
		{
			base.Return(valueFunc);
		}

		public void ThrowAsync(Exception ex)
		{
			base.Return(Task.FromException<TReturn>(ex));
		}

		IExpect<Task<TReturn>> IExpect<Task<TReturn>>.Do(Action<object[]> callback)
		{
			base.Do(callback);
			return this;
		}

		IExpect<Task<TReturn>> IExpect<Task<TReturn>>.Do<TArg>(Action<TArg> callback)
		{
			base.Do(callback);
			return this;
		}
	}
}