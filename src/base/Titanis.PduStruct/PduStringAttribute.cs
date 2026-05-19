using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class PduStringAttribute : Attribute
	{
		public PduStringAttribute(CharSet charSet, int constLength)
		{
			this.CharSet = charSet;
			this.ConstLength = constLength;
		}
		public PduStringAttribute(CharSet charSet, string lengthMemberName)
		{
			this.CharSet = charSet;
			this.ConstLength = -1;
			this.LengthMemberName = lengthMemberName;
		}

		public CharSet CharSet { get; }
		public int ConstLength { get; }
		public string? LengthMemberName { get; }
	}
}
