using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class PduAlignmentAttribute : Attribute
	{
		public PduAlignmentAttribute(int alignment)
		{
			this.Alignment = alignment;
		}

		public int Alignment { get; }
	}
}
