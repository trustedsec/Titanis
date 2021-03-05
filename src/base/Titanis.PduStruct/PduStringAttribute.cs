using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class PduStringAttribute : Attribute
	{
		public PduStringAttribute(CharSet charSet, string lengthMemberName)
		{
			this.CharSet = charSet;
			this.LengthMemberName = lengthMemberName;
		}

		public CharSet CharSet { get; }
		public string LengthMemberName { get; }
	}
}
