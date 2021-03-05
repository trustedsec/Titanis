using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace Titanis.Reflection.Emit
{
	public static class EmitExtensions
	{
		public static ILGenerator Ldstr(this ILGenerator ilgen, string str)
		{
			if (str is null)
				throw new ArgumentNullException(nameof(str));

			ilgen.Emit(OpCodes.Ldstr, str);
			return ilgen;
		}

		#region Flow
		public static ILGenerator Ret(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ret);
			return ilgen;
		}

		#endregion

		#region Constants
		public static ILGenerator Ldnull(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ldnull);
			return ilgen;
		}
		public static ILGenerator Ldc_I4_0(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ldc_I4_0);
			return ilgen;
		}
		public static ILGenerator Ldc_I4_1(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ldc_I4_1);
			return ilgen;
		}
		public static ILGenerator Ldc_I4(this ILGenerator ilgen, int value)
		{
			ilgen.Emit(OpCodes.Ldc_I4, value);
			return ilgen;
		}
		public static ILGenerator Ldc_I8(this ILGenerator ilgen, long value)
		{
			ilgen.Emit(OpCodes.Ldc_I8, value);
			return ilgen;
		}
		public static ILGenerator Ldc_I8(this ILGenerator ilgen, ulong value)
		{
			ilgen.Emit(OpCodes.Ldc_I8, value);
			return ilgen;
		}
		#endregion

		public static ILGenerator Dup(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Dup);
			return ilgen;
		}

		public static ILGenerator Ldarg(this ILGenerator ilgen, short argIndex)
		{
			ilgen.Emit(OpCodes.Ldarg, argIndex);
			return ilgen;
		}
		public static ILGenerator RefanyType(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Refanytype);
			return ilgen;
		}
		public static ILGenerator RefanyValue(this ILGenerator ilgen, Type refType)
		{
			if (refType is null)
				throw new ArgumentNullException(nameof(refType));

			ilgen.Emit(OpCodes.Refanyval, refType);
			return ilgen;
		}

		public static ILGenerator Ldfld(this ILGenerator ilgen, FieldInfo field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));

			ilgen.Emit(OpCodes.Ldfld, field);
			return ilgen;
		}

		public static ILGenerator Ldflda(this ILGenerator ilgen, FieldInfo field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));

			ilgen.Emit(OpCodes.Ldflda, field);
			return ilgen;
		}

		public static ILGenerator Call(this ILGenerator ilgen, MethodInfo method)
		{
			if (method is null)
				throw new ArgumentNullException(nameof(method));

			Debug.Assert(!method.IsVirtual);

			ilgen.Emit(OpCodes.Call, method);
			return ilgen;
		}

		public static ILGenerator Call(this ILGenerator ilgen, ConstructorInfo constructor)
		{
			if (constructor is null)
				throw new ArgumentNullException(nameof(constructor));

			ilgen.Emit(OpCodes.Call, constructor);
			return ilgen;
		}

		public static ILGenerator Callvirt(this ILGenerator ilgen, MethodInfo method)
		{
			if (method is null)
				throw new ArgumentNullException(nameof(method));

			//Debug.Assert(method.IsVirtual);
			ilgen.Emit(OpCodes.Callvirt, method);
			return ilgen;
		}

		#region Locals
		public static ILGenerator Stloc(this ILGenerator ilgen, LocalBuilder loc)
		{
			if (loc is null)
				throw new ArgumentNullException(nameof(loc));

			ilgen.Emit(OpCodes.Stloc, loc);
			return ilgen;
		}

		public static ILGenerator Ldloc(this ILGenerator ilgen, LocalBuilder loc)
		{
			if (loc is null)
				throw new ArgumentNullException(nameof(loc));

			ilgen.Emit(OpCodes.Ldloc, loc);
			return ilgen;
		}

		public static ILGenerator Ldloca(this ILGenerator ilgen, LocalBuilder loc)
		{
			if (loc is null)
				throw new ArgumentNullException(nameof(loc));

			ilgen.Emit(OpCodes.Ldloca, loc);
			return ilgen;
		}
		#endregion

		public static ILGenerator Pop(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Pop);
			return ilgen;
		}

		#region Branching
		public static ILGenerator Switch(this ILGenerator ilgen, Label[] labels)
		{
			ilgen.Emit(OpCodes.Switch, labels);
			return ilgen;
		}
		public static ILGenerator Br(this ILGenerator ilgen, Label label)
		{
			ilgen.Emit(OpCodes.Br, label);
			return ilgen;
		}
		public static ILGenerator Brfalse(this ILGenerator ilgen, Label label)
		{
			ilgen.Emit(OpCodes.Brfalse, label);
			return ilgen;
		}
		public static ILGenerator Brtrue(this ILGenerator ilgen, Label label)
		{
			ilgen.Emit(OpCodes.Brtrue, label);
			return ilgen;
		}
		public static ILGenerator Beq(this ILGenerator ilgen, Label label)
		{
			ilgen.Emit(OpCodes.Beq, label);
			return ilgen;
		}
		public static ILGenerator Bne(this ILGenerator ilgen, Label label)
		{
			ilgen.Emit(OpCodes.Bne_Un, label);
			return ilgen;
		}
		#endregion

		#region Bitwise
		public static ILGenerator And(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.And);
			return ilgen;
		}
		public static ILGenerator Xor(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Xor);
			return ilgen;
		}
		public static ILGenerator Ceq(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ceq);
			return ilgen;
		}
		public static ILGenerator Clt(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Clt);
			return ilgen;
		}
		public static ILGenerator Cgt(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Cgt);
			return ilgen;
		}

		public static ILGenerator Shl(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Shl);
			return ilgen;
		}
		public static ILGenerator Shr_Un(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Shr_Un);
			return ilgen;
		}
		#endregion

		#region Arithmetic
		public static ILGenerator Add(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Add);
			return ilgen;
		}
		public static ILGenerator Sub(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Sub);
			return ilgen;
		}

		#endregion

		#region Conversion
		public static ILGenerator Conv_U4(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Conv_U4);
			return ilgen;
		}
		public static ILGenerator Conv_U8(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Conv_U8);
			return ilgen;
		}
		#endregion

		#region Indirection
		public static ILGenerator Ldind(this ILGenerator ilgen, Type valueType)
		{
			if (valueType is null)
				throw new ArgumentNullException(nameof(valueType));

			//if (valueType.IsEnum)
			//{
			//	ilgen.Emit(OpCodes.Ldobj, valueType);
			//}
			//else
			{
				TypeCode code = Type.GetTypeCode(valueType);
				switch (code)
				{
					case TypeCode.Object:
					case TypeCode.String:
					case TypeCode.DBNull:
					case TypeCode.Decimal:
					case TypeCode.DateTime:
					default:
						if (valueType.IsValueType)
							ilgen.Emit(OpCodes.Ldobj, valueType);
						else
							ilgen.Emit(OpCodes.Ldind_Ref);
						break;
					case TypeCode.SByte:
						ilgen.Emit(OpCodes.Ldind_I1);
						break;
					case TypeCode.Byte:
					case TypeCode.Boolean:
						ilgen.Emit(OpCodes.Ldind_U1);
						break;
					case TypeCode.Int16:
						ilgen.Emit(OpCodes.Ldind_I2);
						break;
					case TypeCode.Char:
					case TypeCode.UInt16:
						ilgen.Emit(OpCodes.Ldind_U2);
						break;
					case TypeCode.Int32:
						ilgen.Emit(OpCodes.Ldind_I4);
						break;
					case TypeCode.UInt32:
						ilgen.Emit(OpCodes.Ldind_U4);
						break;
					case TypeCode.Int64:
					case TypeCode.UInt64:
						ilgen.Emit(OpCodes.Ldind_I8);
						break;
					case TypeCode.Single:
						ilgen.Emit(OpCodes.Ldind_R4);
						break;
					case TypeCode.Double:
						ilgen.Emit(OpCodes.Ldind_R8);
						break;
				}
			}

			return ilgen;
		}
		public static ILGenerator Stind(this ILGenerator ilgen, Type valueType)
		{
			if (valueType is null)
				throw new ArgumentNullException(nameof(valueType));

			//if (valueType.IsEnum)
			//{
			//	ilgen.Emit(OpCodes.Stobj, valueType);
			//}
			//else
			{
				TypeCode code = Type.GetTypeCode(valueType);
				switch (code)
				{
					case TypeCode.Object:
					case TypeCode.String:
					case TypeCode.DBNull:
					case TypeCode.Decimal:
					case TypeCode.DateTime:
					default:
						if (valueType.IsValueType)
							ilgen.Emit(OpCodes.Stobj, valueType);
						else
							ilgen.Emit(OpCodes.Stind_Ref);
						break;
					case TypeCode.SByte:
					case TypeCode.Byte:
					case TypeCode.Boolean:
						ilgen.Emit(OpCodes.Stind_I1);
						break;
					case TypeCode.Int16:
					case TypeCode.Char:
					case TypeCode.UInt16:
						ilgen.Emit(OpCodes.Stind_I2);
						break;
					case TypeCode.Int32:
					case TypeCode.UInt32:
						ilgen.Emit(OpCodes.Stind_I4);
						break;
					case TypeCode.Int64:
					case TypeCode.UInt64:
						ilgen.Emit(OpCodes.Stind_I8);
						break;
					case TypeCode.Single:
						ilgen.Emit(OpCodes.Stind_R4);
						break;
					case TypeCode.Double:
						ilgen.Emit(OpCodes.Stind_R8);
						break;
				}
			}

			return ilgen;
		}
		#endregion

		public static ILGenerator Initobj(this ILGenerator ilgen, Type type)
		{
			if (type is null)
				throw new ArgumentNullException(nameof(type));

			ilgen.Emit(OpCodes.Initobj, type);
			return ilgen;
		}

		public static ILGenerator Newobj(this ILGenerator ilgen, ConstructorInfo ctor)
		{
			if (ctor is null)
				throw new ArgumentNullException(nameof(ctor));

			ilgen.Emit(OpCodes.Newobj, ctor);
			return ilgen;
		}
		public static ILGenerator Conv(this ILGenerator ilgen, TypeCode typeCode)
		{
			var opcode = typeCode switch
			{
				TypeCode.SByte => OpCodes.Conv_I1,
				TypeCode.Byte => OpCodes.Conv_I1,
				TypeCode.Int16 => OpCodes.Conv_I2,
				TypeCode.UInt16 => OpCodes.Conv_I2,
				TypeCode.Int32 => OpCodes.Conv_I4,
				TypeCode.UInt32 => OpCodes.Conv_I4,
				TypeCode.Int64 => OpCodes.Conv_I8,
				TypeCode.UInt64 => OpCodes.Conv_I8,
				_ => throw new ArgumentException(Messages.Emit_InvalidConvTypeCode, nameof(TypeCode))
			};
			ilgen.Emit(opcode);
			return ilgen;
		}
		#region Arrays
		public static ILGenerator Ldlen(this ILGenerator ilgen)
		{
			ilgen.Emit(OpCodes.Ldlen);
			return ilgen;
		}
		public static ILGenerator Ldelem(this ILGenerator ilgen, Type valueType)
		{
			if (valueType is null)
				throw new ArgumentNullException(nameof(valueType));

			TypeCode code = Type.GetTypeCode(valueType);
			switch (code)
			{
				case TypeCode.Object:
				case TypeCode.String:
				case TypeCode.DBNull:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
				default:
					if (valueType.IsValueType)
					{
						ilgen.Emit(OpCodes.Ldelema, valueType);
						ilgen.Emit(OpCodes.Ldobj, valueType);
					}
					else
						ilgen.Emit(OpCodes.Ldelem_Ref);
					break;
				case TypeCode.SByte:
					ilgen.Emit(OpCodes.Ldelem_I1);
					break;
				case TypeCode.Byte:
				case TypeCode.Boolean:
					ilgen.Emit(OpCodes.Ldelem_U1);
					break;
				case TypeCode.Int16:
					ilgen.Emit(OpCodes.Ldelem_I2);
					break;
				case TypeCode.Char:
				case TypeCode.UInt16:
					ilgen.Emit(OpCodes.Ldelem_U2);
					break;
				case TypeCode.Int32:
					ilgen.Emit(OpCodes.Ldelem_I4);
					break;
				case TypeCode.UInt32:
					ilgen.Emit(OpCodes.Ldelem_U4);
					break;
				case TypeCode.Int64:
				case TypeCode.UInt64:
					ilgen.Emit(OpCodes.Ldelem_I8);
					break;
				case TypeCode.Single:
					ilgen.Emit(OpCodes.Ldelem_R4);
					break;
				case TypeCode.Double:
					ilgen.Emit(OpCodes.Ldelem_R8);
					break;
			}

			return ilgen;
		}
		#endregion

		#region Metadata
		public static ILGenerator Ldtoken(this ILGenerator ilgen, MethodInfo method)
		{
			if (method is null)
				throw new ArgumentNullException(nameof(method));

			ilgen.Emit(OpCodes.Ldtoken, method);
			return ilgen;
		}
		public static ILGenerator Ldftn(this ILGenerator ilgen, MethodInfo method)
		{
			if (method is null)
				throw new ArgumentNullException(nameof(method));

			ilgen.Emit(OpCodes.Ldftn, method);
			return ilgen;
		}
		#endregion
	}
}
