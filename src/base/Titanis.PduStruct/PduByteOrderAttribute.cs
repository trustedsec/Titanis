using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	[AttributeUsage(
		AttributeTargets.Struct
		| AttributeTargets.Class
		| AttributeTargets.Field
		| AttributeTargets.Property
		| AttributeTargets.Assembly,
		AllowMultiple = false, Inherited = true)]
	public sealed class PduByteOrderAttribute : Attribute
	{
		public PduByteOrderAttribute(PduByteOrder byteOrder)
		{
			this.ByteOrder = byteOrder;
		}

		public PduByteOrderAttribute(string byteOrderMemberName)
		{
			this.ByteOrderMemberName = byteOrderMemberName;
		}

		public PduByteOrder ByteOrder { get; }
		public string? ByteOrderMemberName { get; }

		public sealed override bool Match(object obj) => (obj is PduByteOrderAttribute);
	}
}
