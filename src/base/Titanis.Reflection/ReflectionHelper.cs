using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Reflection
{
	public static class ReflectionHelper
	{
		/// <summary>
		/// Gets the element type of a type implementing <see cref="IList{T}"/>.
		/// </summary>
		/// <param name="listType">Type implementing <see cref="IList{T}"/></param>
		/// <returns>Type argument to <see cref="IList{T}"/>, if implemented by <paramref name="listType"/>; otherwise, <c>null</c>.</returns>
		public static Type? GetListElementType(this Type listType)
		{
			if (listType.IsArray)
			{
				return listType.GetElementType();
			}
			else
			{
				if (listType is null) throw new ArgumentNullException(nameof(listType));

				var itfs = listType.FindInterfaces((r, o) => GetGenericArgumentFor(r, typeof(IList<>)) != null, null);
				return itfs.Length > 0 ? GetGenericArgumentFor(itfs[0], typeof(IList<>)) : null;
			}
		}
		/// <summary>
		/// Gets the argument to a generic type.
		/// </summary>
		/// <param name="type">Closed type</param>
		/// <param name="genericDef">Generic definition</param>
		/// <returns>The argument to <paramref name="genericDef"/>; otherwise, <c>null</c>.</returns>
		internal static Type? GetGenericArgumentFor(Type type, Type genericDef)
		{
			if (type.IsGenericType && !type.IsGenericTypeDefinition
				&& type.GetGenericTypeDefinition() == genericDef)
				return type.GetGenericArguments()[0];
			else
				return null;
		}

		public static Type? TryGetTaskResultType(Type taskType)
			=> GetGenericArgumentFor(taskType, typeof(Task<>));

		public static bool Matches(MethodBase method, params Type[] argTypes)
		{
			var parms = method.GetParameters();
			if (parms.Length != argTypes.Length)
				return false;

			for (int i = 0; i < parms.Length; i++)
			{
				var argType = parms[i].ParameterType;
				var parmType = argTypes[i];
				if (argType != parmType)
				{
					return false;
				}
			}

			return true;
		}

		public static MethodBase TryFindMethod(IEnumerable<MethodBase> methods, params Type[] argTypes)
		{
			foreach (var method in methods)
			{
				if (Matches(method, argTypes))
					return method;
			}
			return null;
		}

		public static ConstructorInfo TryFindConstructor(this Type type, params Type[] argTypes)
		{
			return (ConstructorInfo)TryFindMethod(type.GetTypeInfo().DeclaredConstructors, argTypes);
		}

		public static MethodInfo TryFindMethod(this Type type, string methodName, params Type[] argTypes)
		{
			return (MethodInfo)TryFindMethod(type.GetTypeInfo().GetDeclaredMethods(methodName), argTypes);
		}


		#region ConstructorOf
		public static ConstructorInfo ConstructorOf(Expression<Action> selector)
		{
			return ConstructorOf_(selector);
		}
		public static ConstructorInfo ConstructorOf<T>(Expression<Action<T>> selector)
		{
			return ConstructorOf_(selector);
		}

		private static ConstructorInfo ConstructorOf_(LambdaExpression selector)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			NewExpression? call = selector.Body as NewExpression;
			if (call == null)
				throw new ArgumentException(Messages.ConstructorCallExpressionRequiredMessage, nameof(selector));

			return call.Constructor;
		}
		#endregion

		#region MethodOf
		public static MethodInfo MethodOf<T>(Expression<Action<T>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T, TReturn>(Expression<Func<T, TReturn>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T1, T2, TReturn>(Expression<Func<T1, T2, TReturn>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T1, T2>(Expression<Action<T1, T2>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T1, T2, T3>(Expression<Action<T1, T2, T3>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T1, T2, T3, TReturn>(Expression<Func<T1, T2, T3, TReturn>> selector)
		{
			return MethodOf_(selector);
		}
		public static MethodInfo MethodOf<T1, T2, T3, T4>(Expression<Action<T1, T2, T3, T4>> selector)
		{
			return MethodOf_(selector);
		}

		private static MethodInfo MethodOf_(LambdaExpression selector)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			MethodCallExpression? call = selector.Body as MethodCallExpression;
			if (call == null)
				throw new ArgumentException(Messages.MethodCallExpressionRequiredMessage, nameof(selector));

			return call.Method;
		}

		//public static MethodInfo MethodOf(Action action)
		//{
		//	if (action == null)
		//		throw new ArgumentNullException(nameof(action));

		//	return action.GetMethodInfo();
		//}

		// UNDONE: This creates ambiguity with Expression<Action<T>>
		//public static MethodInfo MethodOf<T>(Action<T> action)
		//{
		//	if (action == null)
		//		throw new ArgumentNullException(nameof(action));

		//	return action.GetMethodInfo();
		//}

		//public static MethodInfo MethodOf<T>(Func<T> func)
		//{
		//	if (func == null)
		//		throw new ArgumentNullException(nameof(func));

		//	return func.GetMethodInfo();
		//}
		//public static MethodInfo MethodOf<T, TRet>(Func<T, TRet> func)
		//{
		//	if (func == null)
		//		throw new ArgumentNullException(nameof(func));

		//	return func.GetMethodInfo();
		//}
		//public static MethodInfo MethodOf<T1, T2>(Action<T1, T2> action)
		//{
		//	if (action == null)
		//		throw new ArgumentNullException(nameof(action));

		//	return action.GetMethodInfo();
		//}
		//public static MethodInfo MethodOf<T1, T2, TRet>(Func<T1, T2, TRet> func)
		//{
		//	if (func == null)
		//		throw new ArgumentNullException(nameof(func));

		//	return func.GetMethodInfo();
		//}
		#endregion

		#region FieldOf
		public static FieldInfo FieldOf<T>(Expression<Func<T, object>> selector)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			var body = selector.Body;
			if (body.NodeType == ExpressionType.Convert)
				body = ((UnaryExpression)body).Operand;

			MemberExpression? access = body as MemberExpression;
			var field = access?.Member as FieldInfo;
			if (field is null)
				throw new ArgumentException(Messages.FieldExpressionRequiredMessage, nameof(selector));

			return field;
		}
		#endregion
		#region PropertyOf
		public static PropertyInfo PropertyOf<T>(Expression<Func<T, object>> selector)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			var body = selector.Body;
			if (body.NodeType == ExpressionType.Convert)
				body = ((UnaryExpression)body).Operand;

			MemberExpression? access = body as MemberExpression;
			var property = access?.Member as PropertyInfo;
			if (property is null)
				throw new ArgumentException(Messages.PropertyExpressionRequiredMessage, nameof(selector));

			return property;
		}
		#endregion

	}
}
