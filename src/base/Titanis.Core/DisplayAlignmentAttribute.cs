using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Titanis
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class DisplayAlignmentAttribute : Attribute
	{
		public DisplayAlignmentAttribute(DisplayAlignment alignment)
		{
			this.Alignment = alignment;
		}

		public DisplayAlignment Alignment { get; }

		public static DisplayAlignment GetDefaultAlignmentFor(Type type)
		{
			if (type is null) throw new ArgumentNullException(nameof(type));

			var tc = Type.GetTypeCode(type);
			switch (tc)
			{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Byte:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return DisplayAlignment.Right;
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.DateTime:
				case TypeCode.DBNull:
				case TypeCode.Empty:
				case TypeCode.Object:
				case TypeCode.SByte:
				case TypeCode.Single:
				case TypeCode.String:
				default:
					return DisplayAlignment.Left;
			}
		}
	}
}
