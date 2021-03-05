using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Specifies the format string to use when displaying a value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DisplayFormatStringAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="DisplayFormatStringAttribute"/>.
		/// </summary>
		/// <param name="formatString">Format string to use for display</param>
		public DisplayFormatStringAttribute(string formatString)
		{
			this.FormatString = formatString;
		}

		/// <summary>
		/// Gets the format string to use for display
		/// </summary>
		public string FormatString { get; }

		public static string? GetDefaultFormatFor(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));

			if (type.IsEnum)
				return "G";

			var tc = Type.GetTypeCode(type);
			switch (tc)
			{
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return "N0";
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.DateTime:
				case TypeCode.DBNull:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Empty:
				case TypeCode.Object:
				case TypeCode.Single:
				case TypeCode.String:
				default:
					return null;
			}
		}
	}
}
