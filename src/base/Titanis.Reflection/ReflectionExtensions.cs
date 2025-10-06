using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Titanis.Reflection
{
	public static class ReflectionExtensions
	{
		public static object Instantiate(this TypeInfo type)
		{
			if (type is null)
				throw new ArgumentNullException(nameof(type));

			var ctor = type.DeclaredConstructors.SingleOrDefault(ctor => ctor.GetParameters().Length == 0);
			if (ctor == null)
				throw new ArgumentException(Messages.Reflection_NoDefaultConstructor, nameof(type));

			var obj = ctor.Invoke(Type.EmptyTypes);
			return obj;
		}
		public static object Instantiate<T1>(this TypeInfo type, T1 arg1)
		{
			if (type is null)
				throw new ArgumentNullException(nameof(type));

			var ctor = type.DeclaredConstructors.SingleOrDefault(ctor =>
			{
				var parms = ctor.GetParameters();
				var matches =
					(parms.Length == 1)
					&& (parms[0].ParameterType.GetTypeInfo().IsAssignableFrom(typeof(T1).GetTypeInfo()))
					;
				return matches;
			});
			if (ctor == null)
				throw new ArgumentException(Messages.Reflection_NoDefaultConstructor, nameof(type));

			var obj = ctor.Invoke([arg1]);
			return obj;
		}

		public static bool IsFlagsEnum(this Type enumType)
		{
			return enumType.GetTypeInfo().IsDefined(typeof(FlagsAttribute), true);
		}

		public static MethodInfo? TryResolveInterfaceMethod(this Type implementingType, MethodInfo interfaceMethod)
		{
			if (interfaceMethod is null)
				throw new ArgumentNullException(nameof(interfaceMethod));

			var itfType = interfaceMethod.DeclaringType;
			if ((itfType == null) || !itfType.GetTypeInfo().IsInterface)
				throw new ArgumentException(Messages.Reflection_MethodMustBeInterfaceMethod, nameof(interfaceMethod));

			var interfaceMap = implementingType.GetTypeInfo().GetRuntimeInterfaceMap(itfType);
			for (int i = 0; i < interfaceMap.InterfaceMethods.Length; i++)
			{
				var itfMethod = interfaceMap.InterfaceMethods[i];
				if (itfMethod == interfaceMethod)
					return interfaceMap.TargetMethods[i];
			}

			return null;
		}

		public static byte[] LoadResourceData(this Assembly assembly, string resourceName)
		{
			if (string.IsNullOrEmpty(resourceName))
				throw new ArgumentNullException(nameof(resourceName));

			var resourceStream = assembly.GetManifestResourceStream(resourceName);
			if (resourceName == null)
				throw new FileNotFoundException();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
			byte[] data = new byte[resourceStream.Length];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			resourceStream.Read(data, 0, data.Length);
			return data;
		}
	}
}
