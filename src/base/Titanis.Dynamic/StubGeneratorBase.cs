using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Titanis.Reflection;

namespace Titanis.Dynamic
{
	/// <summary>
	/// Base class for stub generator implementations.
	/// </summary>
	public abstract class StubGeneratorBase
	{
		/// <summary>
		/// Initializes a new <see cref="StubGeneratorBase"/>.
		/// </summary>
		/// <param name="stubAssemblyName">Name of assembly to generate.</param>
		/// <exception cref="ArgumentNullException"><paramref name="stubAssemblyName"/> is <c>null</c>.</exception>
		public StubGeneratorBase(AssemblyName stubAssemblyName)
		{
			if (stubAssemblyName is null) throw new ArgumentNullException(nameof(stubAssemblyName));

			AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(stubAssemblyName, AssemblyBuilderAccess.RunAndCollect);
			ModuleBuilder mb = ab.DefineDynamicModule(stubAssemblyName.Name);

			this.assembly = ab;
			this.module = mb;
		}

		protected readonly AssemblyBuilder assembly;
		protected readonly ModuleBuilder module;

		/// <summary>
		/// Generates a stub for a type.
		/// </summary>
		/// <param name="baseClass">Base class type</param>
		/// <param name="typeName">Name of stub type</param>
		/// <returns><see cref="TypeInfo"/> representing the generated stub type</returns>
		public TypeInfo GenerateTypeStub(Type baseClass, string typeName)
			=> this.GenerateTypeStub(baseClass, typeName, null);
		/// <summary>
		/// Generates a stub for a type.
		/// </summary>
		/// <param name="baseClass">Base class type</param>
		/// <param name="stubTypeName">Name of stub type</param>
		/// <param name="interfaceTypes">List of interface types to implement</param>
		/// <exception cref="ArgumentNullException"><paramref name="baseClass"/> is <c>null</c>.</exception>
		/// <returns><see cref="TypeInfo"/> representing the generated stub type</returns>
		/// <remarks>
		/// <paramref name="baseClass"/> must not be <c>null</c>.  If no specific
		/// base class is required, use <see cref="object"/>.</remarks>
		public TypeInfo GenerateTypeStub(Type baseClass, string stubTypeName, Type[]? interfaceTypes)
		{
			if (baseClass is null) throw new ArgumentNullException(nameof(baseClass));
			if (string.IsNullOrEmpty(stubTypeName)) throw new ArgumentNullException(nameof(stubTypeName));

			var baseClassInfo = baseClass.GetTypeInfo();
			if (!baseClassInfo.IsClass)
				// TODO: 
				throw new ArgumentException(string.Format(Messages.StubGen_BaseTypeNotAClass, baseClass.FullName), nameof(baseClass));

			return this.GenerateTypeStub(new StubGenerateParams
			{
				BaseClass = baseClassInfo,
				InterfaceTypes = interfaceTypes,
				StubTypeName = stubTypeName
			});
		}
		private TypeInfo GenerateTypeStub(in StubGenerateParams stubParams)
		{

			TypeAttributes baseTypeAttrs = stubParams.BaseClass.Attributes;
			baseTypeAttrs &= ~(TypeAttributes.Abstract | TypeAttributes.VisibilityMask);
			baseTypeAttrs |= TypeAttributes.Public;
			TypeBuilder tb = this.module.DefineType(stubParams.StubTypeName, baseTypeAttrs, stubParams.BaseClass.AsType());

			if (stubParams.InterfaceTypes != null && stubParams.InterfaceTypes.Length > 0)
			{
				foreach (var interfaceType in stubParams.InterfaceTypes)
				{
					tb.AddInterfaceImplementation(interfaceType);
				}
			}

			object userContext = OnGeneratingStub(tb, stubParams);

			StubGenerateContext ctx = new StubGenerateContext
			{
				implInterfaces = new HashSet<Type>(),
				implMethods = new HashSet<MethodInfo>(),
				userContext = userContext
			};

			DefineConstructors(stubParams.BaseClass, tb);
			this.DefineMethodOverrides(stubParams.BaseClass.AsType(), tb, userContext);
			if (stubParams.InterfaceTypes != null && stubParams.InterfaceTypes.Length > 0)
			{
				foreach (var interfaceType in stubParams.InterfaceTypes)
				{
					this.DefineInterface(interfaceType, tb, ctx);
				}
			}

			this.OnFinalizingStub(userContext);

			return tb.CreateTypeInfo();
		}

		protected virtual void OnFinalizingStub(object context)
		{
		}

		protected virtual object OnGeneratingStub(TypeBuilder tb, in StubGenerateParams stubParams)
		{
			return null;
		}

		protected void DefineMethodOverrides(Type baseType, TypeBuilder tb, object context)
		{
			var baseMethods = baseType.GetRuntimeMethods();
			foreach (var baseMethod in baseMethods)
			{
				var declaringType = baseMethod.DeclaringType;
				if (declaringType == typeof(object))
					continue;

				var attrs = baseMethod.Attributes;
				if (baseMethod.IsVirtual && !baseMethod.IsFinal
					&& (0 == (attrs & MethodAttributes.RTSpecialName)))
				{
					this.DefineMethodStub(tb, baseMethod, context);
				}
			}
		}

		protected struct StubGenerateContext
		{
			internal HashSet<Type> implInterfaces;
			internal HashSet<MethodInfo> implMethods;
			public object userContext;
		}

		protected void DefineInterface(
			Type interfaceType,
			TypeBuilder tb,
			in StubGenerateContext context)
		{
			if (context.implInterfaces.Add(interfaceType))
			{
				// Get all methods, including inherited methods
				var baseMethods = interfaceType.GetRuntimeMethods();
				foreach (var baseMethod in baseMethods)
				{
					if (context.implMethods.Add(baseMethod))
						this.DefineMethodStub(tb, baseMethod, context.userContext);
				}

				var itfInfo = interfaceType.GetTypeInfo();
				foreach (var baseItf in itfInfo.ImplementedInterfaces)
				{
					this.DefineInterface(baseItf, tb, context);
				}
			}
		}

		protected virtual void DefineMethodStub(TypeBuilder tb, MethodInfo baseMethod, object context)
		{
			var parms = baseMethod.GetParameters();
			var declInfo = GetMethodDeclInfo(parms);

			MethodAttributes attrs = baseMethod.Attributes;
			attrs &= ~MethodAttributes.Abstract;

			Type declaringType = baseMethod.DeclaringType;
			bool isInterface = (declaringType.GetTypeInfo().IsInterface);
			string methodName = baseMethod.Name;
			if (isInterface)
			{
				attrs &= ~MethodAttributes.MemberAccessMask;
				attrs |= MethodAttributes.NewSlot | MethodAttributes.Final | MethodAttributes.SpecialName | MethodAttributes.Private;
				methodName = $"{declaringType.FullName}.{methodName}";
				// MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Final
			}
			else
			{
				attrs &= ~MethodAttributes.NewSlot;
			}

			var methodb = tb.DefineMethod(
				methodName,
				attrs,
				baseMethod.CallingConvention,
				baseMethod.ReturnType,
				Type.EmptyTypes, Type.EmptyTypes,
				declInfo.paramTypes,
				declInfo.reqmods, declInfo.optmods);

			if (isInterface)
				tb.DefineMethodOverride(methodb, baseMethod);

			this.ImplementMethod(baseMethod, parms, methodb, context);
		}

		protected abstract void ImplementMethod(
			MethodInfo baseMethod,
			ParameterInfo[] parms,
			MethodBuilder methodb,
			object context);

		private static void DefineConstructors(TypeInfo baseType, TypeBuilder tb)
		{
			var baseCtors = baseType.DeclaredConstructors;
			bool hasCtor = false;
			foreach (var baseCtor in baseCtors)
			{
				var baseAttrs = baseCtor.Attributes;
				switch (baseAttrs & MethodAttributes.MemberAccessMask)
				{
					case MethodAttributes.Private:
						continue;
				}

				hasCtor = true;

				DefineConstructor(tb, baseCtor);
			}

			if (!hasCtor)
				throw new ArgumentException(string.Format(Messages.StubGen_NoAccessibleConstructor, baseType.FullName), nameof(baseType));
		}

		private static void DefineConstructor(TypeBuilder tb, ConstructorInfo baseCtor)
		{
			ParameterInfo[] parms = baseCtor.GetParameters();
			MethodDeclInfo declInfo = GetMethodDeclInfo(parms);

			MethodAttributes methodAttrs = MethodAttributes.Public;
			var chainedCtor = tb.DefineConstructor(methodAttrs, baseCtor.CallingConvention, declInfo.paramTypes, declInfo.reqmods, declInfo.optmods);
			var ilgen = chainedCtor.GetILGenerator();

			ilgen.Ldarg_0();
			for (short i = 0; i < parms.Length - 1; i++)
			{
				var parm = parms[i];
				var pb = chainedCtor.DefineParameter(i, parm.Attributes, parm.Name);

				// TODO: Custom attributes

				// TODO: Default value?
				ilgen.Ldarg(i);
			}
			ilgen.Call(baseCtor).Ret();
		}

		internal static MethodDeclInfo GetMethodDeclInfo(ParameterInfo[] parms)
		{
			Type[] paramTypes = new Type[parms.Length];
			Type[][] reqmods = new Type[paramTypes.Length][];
			Type[][] optmods = new Type[paramTypes.Length][];
			for (int i = 0; i < parms.Length; i++)
			{
				var parm = parms[i];
				paramTypes[i] = parm.ParameterType;
				// TODO: How are modifiers discovered?
				reqmods[i] = Type.EmptyTypes;
				optmods[i] = Type.EmptyTypes;
			}
			MethodDeclInfo declInfo = new MethodDeclInfo(
				paramTypes,
				reqmods,
				optmods
			);
			return declInfo;
		}
	}
}
