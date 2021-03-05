using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class PduPositionAttribute : Attribute
	{
	}
}
