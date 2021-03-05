using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class PduParameterAttribute : Attribute
	{
	}
}
