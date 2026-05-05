using System;
using System.Linq;
using System.Text;

namespace Titanis.Msrpc.Mswmi
{
	public static class MofExtensions
	{
		public static StringBuilder AppendMofClass(this StringBuilder sb, WmiClassObject obj)
		{
			ArgumentNullException.ThrowIfNull(sb);
			ArgumentNullException.ThrowIfNull(obj);

			foreach (var qual in obj.Qualifiers)
			{
				sb.AppendMofQualifier(qual);
				sb.AppendLine();
			}

			sb.Append("class ").Append(obj.Name ?? "<anonymous>");
			string? baseClassName = obj.BaseClass?.Name;
			if (baseClassName != null)
				sb.Append(" : ").Append(baseClassName);

			sb.AppendLine(" {");

			foreach (var prop in obj.Properties)
			{
				prop.ToMof(sb, "\t");
				sb.AppendLine();
			}

			sb.AppendLine("}");

			return sb;
		}
		public static StringBuilder AppendMofQualifier(this StringBuilder sb, WmiQualifier qualifier)
		{
			ArgumentNullException.ThrowIfNull(sb);
			ArgumentNullException.ThrowIfNull(qualifier);

			sb.Append('[')
				.Append(qualifier.Name)
				;
			if (qualifier.Value != null)
			{
				sb.Append('(');
				// TODO: Proper MOF encode value
				if (qualifier.Value is object[] values)
				{
					bool first = true;
					foreach (var value in values)
					{
						if (first)
							first = false;
						else
							sb.Append(", ");

						sb.AppendMofValue(value);
					}
				}
				else
					sb.AppendMofValue(qualifier.Value);
				sb.Append(')');
			}
			sb.Append(']');

			return sb;
		}
		public static StringBuilder AppendMofProperty(this StringBuilder sb, WmiProperty property, string? indent = null)
		{
			ArgumentNullException.ThrowIfNull(sb);
			ArgumentNullException.ThrowIfNull(property);

			foreach (var qual in property.Qualifiers)
			{
				sb.Append(indent);
				sb.AppendMofQualifier(qual);
				sb.AppendLine();
			}

			sb
				.Append(indent)
				.AppendCimType(property.PropertyType)
				.Append(' ')
				.Append(property.Name)
				.Append(';')
				.AppendLine();

			return sb;
		}
		public static StringBuilder AppendMofInstance(this StringBuilder sb, WmiInstanceObject obj)
		{
			ArgumentNullException.ThrowIfNull(sb);
			ArgumentNullException.ThrowIfNull(obj);

			sb.AppendLine($"instance of {obj.WmiClass.Name} {{");
			foreach (var prop in obj.Properties)
			{
				sb.Append($"\t{prop.ClassProperty.Name} = ")
					.AppendMofValue(prop.Value)
					.AppendLine(";");
			}
			sb.AppendLine("}");

			return sb;
		}
		public static StringBuilder AppendMofValue(this StringBuilder sb, object? value)
		{
			ArgumentNullException.ThrowIfNull(sb);

			if (value == null)
			{
				sb.Append("null");
				return sb;
			}

			if (value is Array arr)
			{
				sb.Append("{ ");
				for (int i = 0; i < arr.Length; i++)
				{
					if (i > 0)
						sb.Append(", ");

					object? elem = arr.GetValue(i);
					sb.AppendMofValue(elem);
				}
				sb.Append(" }");
			}
			else
			{
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
					case TypeCode.DBNull:
						sb.Append(value);
						break;
					case TypeCode.DateTime:
						sb.Append('"')
							// TODO: Look up actual date format
							.Append(value)
							.Append('"');
						break;
					default:
					case TypeCode.Object:
					case TypeCode.String:
						sb.Append('"');
						// TODO: Real escapes
						{
							int startIndex = sb.Length;
							sb.Append(value);
							sb.Replace(@"\", @"\\", startIndex, sb.Length - startIndex);
							sb.Replace("\"", @"\""", startIndex, sb.Length - startIndex);
							sb.Append('"');
						}
						break;
				}
			}

			return sb;
		}
		public static StringBuilder AppendCimType(this StringBuilder sb, CimType cimType)
		{
			var isArray = cimType.IsArray();
			cimType = cimType.ElementType();
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