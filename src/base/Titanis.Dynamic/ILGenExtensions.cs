using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Titanis.Dynamic
{
	/// <summary>
	/// Implements convenience methods for IL generation.
	/// </summary>
	/// <remarks>
	/// This class implements extension methods for the MSIL opcodes, with an
	/// overload for every valid parameter type.  This ensures that required
	/// arguments are specified and of the correct type.
	/// Each method returns a reference to the <see cref="ILGenerator"/> so
	/// that calls may be chained.
	/// </remarks>
	public static class ILGenExtensions
	{
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode)
		{
			ilgen.Emit(opcode);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, short n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, int n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, float n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, double n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, Type n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, FieldInfo n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, MethodInfo n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, ConstructorInfo n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, string n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, Label n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}
		internal static ILGenerator Operand(this ILGenerator ilgen, OpCode opcode, Label[] n)
		{
			ilgen.Emit(opcode, n);
			return ilgen;
		}

		public static ILGenerator Add(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Add);
		public static ILGenerator Ldloc_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Ldloc_S, index);
		public static ILGenerator Ldloca(this ILGenerator ilgen, short index) => ilgen.Operand(OpCodes.Ldloca, index);
		public static ILGenerator Ldloca(this ILGenerator ilgen, LocalBuilder local) => ilgen.Operand(OpCodes.Ldloca, local.LocalIndex);
		public static ILGenerator Ldnull(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldnull);
		public static ILGenerator Ldobj(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Ldobj, type);
		public static ILGenerator Ldsfld(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Ldsfld, field);
		public static ILGenerator Ldsflda(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Ldsflda, field);
		public static ILGenerator Ldstr(this ILGenerator ilgen, string str) => ilgen.Operand(OpCodes.Ldstr, str);
		public static ILGenerator Ldtoken(this ILGenerator ilgen, MethodInfo method) => ilgen.Operand(OpCodes.Ldtoken, method);
		public static ILGenerator Ldtoken(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Ldtoken, field);
		public static ILGenerator Ldtoken(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Ldtoken, type);

		public static ILGenerator Ldvirtftn(this ILGenerator ilgen, MethodInfo method) => ilgen.Operand(OpCodes.Ldvirtftn, method);
		public static ILGenerator Leave(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Leave, target);
		public static ILGenerator Ldloc_3(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldloc_3);
		public static ILGenerator Leave_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Leave_S, target);
		public static ILGenerator Mkrefany(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Mkrefany, type);
		public static ILGenerator Mul(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Mul);
		public static ILGenerator Mul_Ovf(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Mul_Ovf);
		public static ILGenerator Mul_Ovf_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Mul_Ovf_Un);
		public static ILGenerator Neg(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Neg);
		public static ILGenerator Newarr(this ILGenerator ilgen, Type elementType) => ilgen.Operand(OpCodes.Newarr, elementType);
		public static ILGenerator Newobj(this ILGenerator ilgen, ConstructorInfo constructor) => ilgen.Operand(OpCodes.Newobj, constructor);
		public static ILGenerator Nop(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Nop);
		public static ILGenerator Not(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Not);
		public static ILGenerator Or(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Or);
		public static ILGenerator Pop(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Pop);
		public static ILGenerator Localloc(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Localloc);
		public static ILGenerator Ldloc_2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldloc_2);
		public static ILGenerator Ldloc_1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldloc_1);
		public static ILGenerator Ldloc_0(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldloc_0);
		public static ILGenerator Ldelem_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_I4);
		public static ILGenerator Ldelem_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_I8);
		public static ILGenerator Ldelem_R4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_R4);
		public static ILGenerator Ldelem_R8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_R8);
		public static ILGenerator Ldelem_Ref(this ILGenerator ilgen, object arg) => ilgen.Operand(OpCodes.Ldelem_Ref);
		public static ILGenerator Ldelem_U1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_U1);
		public static ILGenerator Ldelem_U2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_U2);
		public static ILGenerator Ldelem_U4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_U4);
		public static ILGenerator Ldelema(this ILGenerator ilgen, Type elementType) => ilgen.Operand(OpCodes.Ldelema, elementType);
		public static ILGenerator Ldfld(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Ldfld, field);
		public static ILGenerator Ldflda(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Ldflda, field);
		public static ILGenerator Ldftn(this ILGenerator ilgen, MethodInfo method) => ilgen.Operand(OpCodes.Ldftn, method);
		public static ILGenerator Ldind_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_I);
		public static ILGenerator Ldind_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_I1);
		public static ILGenerator Ldind_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_I2);
		public static ILGenerator Ldind_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_I4);
		public static ILGenerator Ldind_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_I8);
		public static ILGenerator Ldind_R4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_R4);
		public static ILGenerator Ldind_R8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_R8);
		public static ILGenerator Ldind_Ref(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_Ref);
		public static ILGenerator Ldind_U1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_U1);
		public static ILGenerator Ldind_U2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_U2);
		public static ILGenerator Ldind_U4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldind_U4);
		public static ILGenerator Ldlen(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldlen);
		public static ILGenerator Ldloc(this ILGenerator ilgen, short index) => ilgen.Operand(OpCodes.Ldloc, index);
		public static ILGenerator Ldloc(this ILGenerator ilgen, LocalBuilder loc) => ilgen.Operand(OpCodes.Ldloc, loc.LocalIndex);
		public static ILGenerator Ldelem_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_I2);
		public static ILGenerator Stind_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_I2);
		public static ILGenerator Stind_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_I4);
		public static ILGenerator Stind_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_I8);
		public static ILGenerator Stind_R4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_R4);
		public static ILGenerator Stind_R8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_R8);
		public static ILGenerator Stind_Ref(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_Ref);
		public static ILGenerator Stloc(this ILGenerator ilgen, LocalBuilder loc) => ilgen.Operand(OpCodes.Stloc, loc.LocalIndex);
		public static ILGenerator Stloc(this ILGenerator ilgen, short index) => ilgen.Operand(OpCodes.Stloc, index);
		public static ILGenerator Stloc_0(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stloc_0);
		public static ILGenerator Stloc_1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stloc_1);
		public static ILGenerator Stloc_2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stloc_2);
		public static ILGenerator Stloc_3(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stloc_3);
		public static ILGenerator Stind_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_I1);
		public static ILGenerator Stloc_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Stloc_S, index);
		public static ILGenerator Stsfld(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Stsfld, field);
		public static ILGenerator Sub(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Sub);
		public static ILGenerator Sub_Ovf(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Sub_Ovf);
		public static ILGenerator Sub_Ovf_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Sub_Ovf_Un);
		public static ILGenerator Switch(this ILGenerator ilgen, Label[] targets) => ilgen.Operand(OpCodes.Switch, targets);
		public static ILGenerator Tailcall(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Tailcall);
		public static ILGenerator Throw(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Throw);
		public static ILGenerator Unaligned(this ILGenerator ilgen, byte alignment) => ilgen.Operand(OpCodes.Unaligned, alignment);
		public static ILGenerator Unbox(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Unbox, type);
		public static ILGenerator Unbox_Any(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Unbox_Any, type);
		public static ILGenerator Volatile(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Volatile);
		public static ILGenerator Stobj(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Stobj, type);
		public static ILGenerator Stind_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stind_I);
		public static ILGenerator Stfld(this ILGenerator ilgen, FieldInfo field) => ilgen.Operand(OpCodes.Stfld, field);
		public static ILGenerator Stelem_Ref(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_Ref);
		public static ILGenerator Readonly(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Readonly);
		public static ILGenerator Refanytype(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Refanytype);
		public static ILGenerator Refanyval(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Refanyval);
		public static ILGenerator Rem(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Rem);
		public static ILGenerator Rem_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Rem_Un);
		public static ILGenerator Ret(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ret);
		public static ILGenerator Rethrow(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Rethrow);
		public static ILGenerator Shl(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Shl);
		public static ILGenerator Shr(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Shr);
		public static ILGenerator Shr_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Shr_Un);
		public static ILGenerator Sizeof(this ILGenerator ilgen, Type valueType) => ilgen.Operand(OpCodes.Sizeof, valueType);
		public static ILGenerator Starg(this ILGenerator ilgen, short index) => ilgen.Operand(OpCodes.Starg, index);
		public static ILGenerator Starg_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Starg_S, index);
		public static ILGenerator Stelem(this ILGenerator ilgen, Type elementType) => ilgen.Operand(OpCodes.Stelem, elementType);
		public static ILGenerator Stelem_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_I);
		public static ILGenerator Stelem_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_I1);
		public static ILGenerator Stelem_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_I2);
		public static ILGenerator Stelem_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_I4);
		public static ILGenerator Stelem_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_I8);
		public static ILGenerator Stelem_R4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_R4);
		public static ILGenerator Stelem_R8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Stelem_R8);
		public static ILGenerator Xor(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Xor);
		public static ILGenerator Ldelem_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_I1);
		public static ILGenerator Ldelem(this ILGenerator ilgen, Type elementType) => ilgen.Operand(OpCodes.Ldelem, elementType);
		public static ILGenerator Brfalse_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Brfalse_S, target);
		public static ILGenerator Brtrue(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Brtrue, target);
		public static ILGenerator Brtrue_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Brtrue_S, target);
		public static ILGenerator Call(this ILGenerator ilgen, MethodInfo method) => ilgen.Operand(OpCodes.Call, method);
		public static ILGenerator Call(this ILGenerator ilgen, ConstructorInfo constructor) => ilgen.Operand(OpCodes.Call, constructor);
		public static ILGenerator Callvirt(this ILGenerator ilgen, MethodInfo method) => ilgen.Operand(OpCodes.Callvirt, method);
		public static ILGenerator Castclass(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Castclass, type);
		public static ILGenerator Ceq(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ceq);
		public static ILGenerator Cgt(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Cgt);
		public static ILGenerator Cgt_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Cgt_Un);
		public static ILGenerator Ckfinite(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ckfinite);
		public static ILGenerator Brfalse(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Brfalse, target);
		public static ILGenerator Clt(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Clt);
		public static ILGenerator Constrained(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Constrained, type);
		public static ILGenerator Conv_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_I);
		public static ILGenerator Conv_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_I1);
		public static ILGenerator Conv_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_I2);
		public static ILGenerator Conv_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_I4);
		public static ILGenerator Conv_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_I8);
		public static ILGenerator Conv_Ovf_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I);
		public static ILGenerator Conv_Ovf_I_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I_Un);
		public static ILGenerator Conv_Ovf_I1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I1);
		public static ILGenerator Conv_Ovf_I1_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I1_Un);
		public static ILGenerator Conv_Ovf_I2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I2);
		public static ILGenerator Clt_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Clt_Un);
		public static ILGenerator Break(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Break);
		public static ILGenerator Br_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Br_S, target);
		public static ILGenerator Br(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Br, target);
		public static ILGenerator Add_Ovf(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Add_Ovf);
		public static ILGenerator Add_Ovf_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Add_Ovf_Un);
		public static ILGenerator And(this ILGenerator ilgen) => ilgen.Operand(OpCodes.And);
		public static ILGenerator Arglist(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Arglist);
		public static ILGenerator Beq(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Beq, target);
		public static ILGenerator Beq_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Beq_S, target);
		public static ILGenerator Bge(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bge, target);
		public static ILGenerator Bge_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bge_S, target);
		public static ILGenerator Bge_Un(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bge_Un, target);
		public static ILGenerator Bge_Un_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bge_Un_S, target);
		public static ILGenerator Bgt(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bgt, target);
		public static ILGenerator Bgt_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bgt_S, target);
		public static ILGenerator Bgt_Un(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bgt_Un, target);
		public static ILGenerator Bgt_Un_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bgt_Un_S, target);
		public static ILGenerator Ble(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Ble, target);
		public static ILGenerator Ble_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Ble_S, target);
		public static ILGenerator Ble_Un(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Ble_Un, target);
		public static ILGenerator Ble_Un_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Ble_Un_S, target);
		public static ILGenerator Blt(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Blt, target);
		public static ILGenerator Blt_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Blt_S, target);
		public static ILGenerator Blt_Un(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Blt_Un, target);
		public static ILGenerator Blt_Un_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Blt_Un_S, target);
		public static ILGenerator Bne_Un(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bne_Un, target);
		public static ILGenerator Bne_Un_S(this ILGenerator ilgen, Label target) => ilgen.Operand(OpCodes.Bne_Un_S, target);
		public static ILGenerator Box(this ILGenerator ilgen, Type valueType) => ilgen.Operand(OpCodes.Box, valueType);
		public static ILGenerator Conv_Ovf_I2_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I2_Un);
		public static ILGenerator Ldelem_I(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldelem_I);
		public static ILGenerator Conv_Ovf_I4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I4);
		public static ILGenerator Conv_Ovf_I8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I8);
		public static ILGenerator Jmp(this ILGenerator ilgen, MethodInfo target) => ilgen.Operand(OpCodes.Jmp, target);
		public static ILGenerator Ldarg(this ILGenerator ilgen, short arg) => ilgen.Operand(OpCodes.Ldarg, arg);
		public static ILGenerator Ldarg_0(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldarg_0);
		public static ILGenerator Ldarg_1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldarg_1);
		public static ILGenerator Ldarg_2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldarg_2);
		public static ILGenerator Ldarg_3(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldarg_3);
		public static ILGenerator Ldarg_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Ldarg_S, index);
		public static ILGenerator Ldarga(this ILGenerator ilgen, short index) => ilgen.Operand(OpCodes.Ldarga, index);
		public static ILGenerator Ldarga_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Ldarga_S, index);
		public static ILGenerator Ldc_I4(this ILGenerator ilgen, int n) => ilgen.Operand(OpCodes.Ldc_I4, n);
		public static ILGenerator Ldc_I4_0(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_0);
		public static ILGenerator Isinst(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Isinst, type);
		public static ILGenerator Ldc_I4_1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_1);
		public static ILGenerator Ldc_I4_3(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_3);
		public static ILGenerator Ldc_I4_4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_4);
		public static ILGenerator Ldc_I4_5(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_5);
		public static ILGenerator Ldc_I4_6(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_6);
		public static ILGenerator Ldc_I4_7(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_7);
		public static ILGenerator Ldc_I4_8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_8);
		public static ILGenerator Ldc_I4_M1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_M1);
		public static ILGenerator Ldc_I4_S(this ILGenerator ilgen, byte index) => ilgen.Operand(OpCodes.Ldc_I4_S, index);
		public static ILGenerator Ldc_I8(this ILGenerator ilgen, long arg) => ilgen.Operand(OpCodes.Ldc_I8, arg);
		public static ILGenerator Ldc_R4(this ILGenerator ilgen, float arg) => ilgen.Operand(OpCodes.Ldc_R4, arg);
		public static ILGenerator Ldc_R8(this ILGenerator ilgen, double arg) => ilgen.Operand(OpCodes.Ldc_R8, arg);
		public static ILGenerator Ldc_I4_2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Ldc_I4_2);
		public static ILGenerator Initobj(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Initobj, type);
		public static ILGenerator Initblk(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Initblk);
		public static ILGenerator Endfinally(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Endfinally);
		public static ILGenerator Conv_Ovf_I8_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I8_Un);
		public static ILGenerator Conv_Ovf_U(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U);
		public static ILGenerator Conv_Ovf_U_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U_Un);
		public static ILGenerator Conv_Ovf_U1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U1);
		public static ILGenerator Conv_Ovf_U1_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U1_Un);
		public static ILGenerator Conv_Ovf_U2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U2);
		public static ILGenerator Conv_Ovf_U2_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U2_Un);
		public static ILGenerator Conv_Ovf_U4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U4);
		public static ILGenerator Conv_Ovf_U4_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U4_Un);
		public static ILGenerator Conv_Ovf_U8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U8);
		public static ILGenerator Conv_Ovf_U8_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_U8_Un);
		public static ILGenerator Conv_R_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_R_Un);
		public static ILGenerator Conv_R4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_R4);
		public static ILGenerator Conv_R8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_R8);
		public static ILGenerator Conv_U(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_U);
		public static ILGenerator Conv_U1(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_U1);
		public static ILGenerator Conv_U2(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_U2);
		public static ILGenerator Conv_U4(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_U4);
		public static ILGenerator Conv_U8(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_U8);
		public static ILGenerator Cpblk(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Cpblk);
		public static ILGenerator Cpobj(this ILGenerator ilgen, Type type) => ilgen.Operand(OpCodes.Cpobj, type);
		public static ILGenerator Div(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Div);
		public static ILGenerator Div_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Div_Un);
		public static ILGenerator Dup(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Dup);
		public static ILGenerator Endfilter(this ILGenerator ilgen, object arg) => ilgen.Operand(OpCodes.Endfilter);
		public static ILGenerator Conv_Ovf_I4_Un(this ILGenerator ilgen) => ilgen.Operand(OpCodes.Conv_Ovf_I4_Un);

		public static ILGenerator Ldind(this ILGenerator ilgen, Type type)
		{
			var opcode = GetLdindOpcodeFor(type);
			if (opcode == OpCodes.Ldobj)
				return ilgen.Ldobj(type);
			else
				return ilgen.Operand(opcode);
		}

		private static OpCode GetLdindOpcodeFor(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));

			var typeInfo = type.GetTypeInfo();
			if (typeInfo.IsValueType)
			{
				return type switch
				{
					var x when x == typeof(IntPtr) => OpCodes.Ldind_I,
					var x when x == typeof(UIntPtr) => OpCodes.Ldind_I,
					var x when x == typeof(byte) => OpCodes.Ldind_I1,
					var x when x == typeof(sbyte) => OpCodes.Ldind_U1,
					var x when x == typeof(short) => OpCodes.Ldind_I2,
					var x when x == typeof(ushort) => OpCodes.Ldind_U2,
					var x when x == typeof(int) => OpCodes.Ldind_I4,
					var x when x == typeof(uint) => OpCodes.Ldind_U4,
					var x when x == typeof(long) => OpCodes.Ldind_I8,
					// TODO: Is this correct?
					var x when x == typeof(ulong) => OpCodes.Ldind_I8,
					var x when x == typeof(float) => OpCodes.Ldind_R4,
					var x when x == typeof(double) => OpCodes.Ldind_R8,
					_ => OpCodes.Ldobj
				};
			}
			else
			{
				return OpCodes.Ldind_Ref;
			}
		}

		public static ILGenerator Stind(this ILGenerator ilgen, Type type)
		{
			var opcode = GetStindOpcodeFor(type);
			if (opcode == OpCodes.Ldobj)
				return ilgen.Stobj(type);
			else
				return ilgen.Operand(opcode);
		}

		private static OpCode GetStindOpcodeFor(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));

			var typeInfo = type.GetTypeInfo();
			if (typeInfo.IsValueType)
			{
				return type switch
				{
					var x when x == typeof(IntPtr) => OpCodes.Stind_I,
					var x when x == typeof(UIntPtr) => OpCodes.Stind_I,
					var x when x == typeof(byte) => OpCodes.Stind_I1,
					var x when x == typeof(sbyte) => OpCodes.Stind_I1,
					var x when x == typeof(short) => OpCodes.Stind_I2,
					var x when x == typeof(ushort) => OpCodes.Stind_I2,
					var x when x == typeof(int) => OpCodes.Stind_I4,
					var x when x == typeof(uint) => OpCodes.Stind_I4,
					var x when x == typeof(long) => OpCodes.Stind_I8,
					// TODO: Is this correct?
					var x when x == typeof(ulong) => OpCodes.Stind_I8,
					var x when x == typeof(float) => OpCodes.Stind_R4,
					var x when x == typeof(double) => OpCodes.Stind_R8,
					_ => OpCodes.Ldobj
				};
			}
			else
			{
				return OpCodes.Ldind_Ref;
			}
		}
		public static ILGenerator Calli(this ILGenerator ilgen, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			ilgen.EmitCalli(OpCodes.Calli, callingConvention, returnType, parameterTypes, optionalParameterTypes);
			return ilgen;
		}
	}
}
