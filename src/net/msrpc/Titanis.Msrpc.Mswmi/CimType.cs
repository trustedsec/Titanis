using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Msrpc.Mswmi
{
	// [MS-WMIO] § 2.2.82 - CimType
	[Flags]
	public enum CimType : int
	{
		None = 0,

		Array = 0x2000,
		Inherited = 0x4000,

		BaseTypeMask = 0xFF,

		SInt8 = 16,
		UInt8 = 17,
		SInt16 = 2,
		UInt16 = 18,
		SInt32 = 3,
		UInt32 = 19,
		SInt64 = 20,
		UInt64 = 21,
		Real32 = 4,
		Real64 = 5,
		Boolean = 11,
		String = 8,
		DateTime = 101,
		Reference = 102,
		Char16 = 103,
		Object = 13,
	}

	// MUST match the value of the [SUBTYPE] qualifier
	public enum CimSubtype
	{
		Unspecified = 0,
		Interval
	}

	public static class CimTypeExtensions
	{
		public static string AsCSharpType(this CimType type)
		{
			var baseType = (type & CimType.BaseTypeMask);

			string typeName = baseType switch
			{
				CimType.None => "void",
				CimType.SInt8 => "sbyte",
				CimType.UInt8 => "byte",
				CimType.SInt16 => "short",
				CimType.UInt16 => "ushort",
				CimType.SInt32 => "int",
				CimType.UInt32 => "uint",
				CimType.SInt64 => "long",
				CimType.UInt64 => "ulong",
				CimType.Real32 => "float",
				CimType.Real64 => "double",
				CimType.Boolean => "bool",
				CimType.String => "string",
				CimType.DateTime => "DateTime",
				CimType.Reference => "object",
				CimType.Char16 => "char",
				CimType.Object => "object",
				_ => "object"
			};
			if (0 != (type & CimType.Array))
				typeName += "[]";
			return typeName;
		}
	}
}
