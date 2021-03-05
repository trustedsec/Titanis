using System;
using System.Linq;
using System.Text;

namespace Titanis.Msrpc.Mswmi
{
	static class MofExtensions
	{
		public static StringBuilder AppendMofValue(this StringBuilder sb, object? value)
		{
			if (value == null)
			{
				sb.Append("null");
				return sb;
			}

			var tc = Convert.GetTypeCode(value);
			switch (tc)
			{
				case TypeCode.Empty:
					sb.Append("null");
					break;
				case TypeCode.Boolean:
					sb.Append(((bool)value) ? "true" : "false");
					break;
				case TypeCode.Char:
					{
						var c = (char)value;
						if (c == '\'')
							sb.Append("'\\''");
						else
							sb.Append('\'').Append(c).Append('\'');
					}
					break;
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.Object:
				case TypeCode.DBNull:
				default:
					sb.Append(value);
					break;
				case TypeCode.DateTime:
					sb.Append('"')
						// TODO: Look up actual date format
						.Append(value)
						.Append('"');
					break;
				case TypeCode.String:
					sb.Append('"')
						// TODO: Real escapes
						.Append(((string)value).Replace("\"", "\\\""))
						.Append('"');
					break;
			}

			return sb;
		}
		public static StringBuilder AppendCimType(this StringBuilder sb, CimType cimType)
		{
			var isArray = 0 != (cimType & CimType.Array);
			cimType &= ~CimType.Array;
			var typeStr = cimType switch
			{
				CimType.SInt8 => "sint8",
				CimType.UInt8 => "uint8",
				CimType.SInt16 => "sint16",
				CimType.UInt16 => "uint16",
				CimType.SInt32 => "sint32",
				CimType.UInt32 => "uint32",
				CimType.SInt64 => "sint64",
				CimType.UInt64 => "uint64",
				CimType.Real32 => "real32",
				CimType.Real64 => "real64",
				CimType.Boolean => "boolean",
				CimType.String => "string",
				CimType.DateTime => "datetime",
				CimType.Char16 => "char16",
				// TODO: Not sure how to represent these yet
				CimType.Reference => "reference",
				CimType.Object => "object",
			};
			sb.Append(typeStr);
			if (isArray)
				sb.Append("[]");

			return sb;
		}
	}
}