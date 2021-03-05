using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using Titanis.Reflection;

namespace Titanis.Dynamic
{
	public class StubGenerator : StubGeneratorBase
	{
		public StubGenerator(AssemblyName stubAssemblyName)
			: base(stubAssemblyName)
		{
		}

		class StubInfo
		{
			internal FieldBuilder handlerField;
		}

		protected override object OnGeneratingStub(TypeBuilder tb, in StubGenerateParams stubParams)
		{
			tb.AddInterfaceImplementation(typeof(IStub));
			var handlerField = tb.DefineField("_handler", typeof(IStubHandler), FieldAttributes.Private);

			MethodBuilder mbSetHandler = tb.DefineMethod("IStub.SetHandler", MethodAttributes.Private | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.HideBySig | MethodAttributes.Final, typeof(void), new Type[] { typeof(IStubHandler) });
			var pbHandler = mbSetHandler.DefineParameter(0, ParameterAttributes.None, "handler");
			ILGenerator ilgen = mbSetHandler.GetILGenerator();
			ilgen
				.Ldarg_0()
				.Ldarg_1()
				.Stfld(handlerField)
				.Ret()
				;
			tb.DefineMethodOverride(mbSetHandler, ReflectionHelper.MethodOf<IStub>(r => r.SetHandler(null)));
			return new StubInfo { handlerField = handlerField };
		}

		private static readonly MethodInfo handleMethod = ReflectionHelper.MethodOf<IStubHandler>(r => r.HandleCall(null));

		protected override void ImplementMethod(
			MethodInfo baseMethod,
			ParameterInfo[] parms,
			MethodBuilder methodb,
			object context)
		{
			StubInfo stubInfo = (StubInfo)context;
			var ilgen = methodb.GetILGenerator();

			Label lblMain = ilgen.DefineLabel();
			ilgen
				.Ldarg(0).Ldfld(stubInfo.handlerField)
				.Brtrue_S(lblMain)
				.Ldstr(Messages.Stub_NoHandler)
				.Newobj(ReflectionHelper.ConstructorOf(() => new InvalidOperationException(null)))
				.Throw()
				.MarkLabel(lblMain)
				;

			// Create param object
			LocalBuilder locMessage = ilgen.DeclareLocal(typeof(MethodCallMessage));
			//LocalBuilder locParamArray = ilgen.DeclareLocal(typeof(object[]));

			ConstructorInfo ctor = ReflectionHelper.ConstructorOf(() => new MethodCallMessage(new RuntimeMethodHandle(), 0));
			ilgen
				.Ldtoken(baseMethod).Ldc_I4(parms.Length)
				.Newobj(ctor).Stloc(locMessage);

			var methSet = ReflectionHelper.MethodOf<MethodCallMessage>(r => r.SetParam("", 0, null));

			bool hasOutParams = false;
			for (short i = 0; i < parms.Length; i++)
			{
				var parm = parms[i];
				var pb = methodb.DefineParameter(i, parm.Attributes, parm.Name);
				var parmType = parm.ParameterType;

				var parmAttrs = parm.Attributes;
				bool isOut = 0 != (parmAttrs & ParameterAttributes.Out);
				bool isIn = !isOut || (0 != (parmAttrs & ParameterAttributes.In));

				if (isOut)
					hasOutParams = true;

				if (isIn)
				{
					ilgen
						.Ldloc(locMessage)
						.Ldstr(parm.Name)
						.Ldc_I4(i)
						.Ldarg((short)(i + 1))
						;
					if (parmType.IsByRef)
					{
						parmType = parmType.GetElementType();
						ilgen.Ldind(parmType);
					}

					if (parmType.GetTypeInfo().IsValueType)
						ilgen.Box(parmType);

					ilgen.Call(methSet);
				}
			}

			Label lblSkipDefault = ilgen.DefineLabel();
			ilgen.Ldarg(0).Ldfld(stubInfo.handlerField)
				.Ldloc(locMessage)
				.Callvirt(handleMethod)
				;

			if (hasOutParams)
			{
				MethodInfo methGetParam = ReflectionHelper.MethodOf<MethodCallMessage>(r => r.GetParam(0));
				for (short i = 0; i < parms.Length; i++)
				{
					var parm = parms[i];
					var parmType = parm.ParameterType;

					var parmAttrs = parm.Attributes;
					bool isOut = 0 != (parmAttrs & ParameterAttributes.Out);

					if (isOut)
					{
						Debug.Assert(parmType.IsByRef);
						parmType = parmType.GetTypeInfo().GetElementType();

						ilgen
							.Ldarg((short)(i + 1))
							.Ldloc(locMessage)
							.Ldc_I4(i)
							.Call(methGetParam)
							;

						bool isValueType = parmType.GetTypeInfo().IsValueType;
						if (isValueType)
						{
							Label lblNullValue = ilgen.DefineLabel();
							Label lblEndArg = ilgen.DefineLabel();

							ilgen
								.Dup()
								.Brfalse(lblNullValue)
								.Unbox_Any(parmType)
								.Stind(parmType)
								.Br(lblEndArg)
								.MarkLabel(lblNullValue)
							;

							ilgen
								.Pop()
								.Pop()
								.MarkLabel(lblEndArg)
								;
						}
						else
						{
							ilgen
								.Stind(parmType)
								;
						}
					}
				}
			}

			bool canCallBase = !baseMethod.IsAbstract;
			if (canCallBase)
			{
				ilgen
					.Ldloc(locMessage).Ldfld(ReflectionHelper.FieldOf<MethodCallMessage>(r => r.callBase))
					.Brfalse(lblSkipDefault)
					;

				// Call default
				// Includes 'this' pointer
				for (short i = 0; i <= parms.Length; i++)
				{
					ilgen.Ldarg(i);
				}
				ilgen.Call(baseMethod).Ret();

				ilgen.MarkLabel(lblSkipDefault);
			}

			var returnType = baseMethod.ReturnType;
			if (returnType != typeof(void))
			{
				var retTypeInfo = returnType.GetTypeInfo();
				if (retTypeInfo.IsValueType)
				{
					Label lblNondefault = ilgen.DefineLabel();
					LocalBuilder locDefault = ilgen.DeclareLocal(returnType);
					ilgen
						.Ldloc(locMessage).Ldfld(ReflectionHelper.FieldOf<MethodCallMessage>(r => r.returnValue))
						.Dup()
						.Brtrue(lblNondefault)
						.Pop()
						.Ldloc(locDefault)
						.Ret()
						;

					ilgen.MarkLabel(lblNondefault);
					ilgen

						.Unbox_Any(returnType)
						.Ret()
						;
				}
				else
				{
					ilgen
						.Ldloc(locMessage).Ldfld(ReflectionHelper.FieldOf<MethodCallMessage>(r => r.returnValue))
						.Castclass(returnType)
						.Ret();
				}
			}
			else
			{
				ilgen.Ret();
			}
		}
	}
}
