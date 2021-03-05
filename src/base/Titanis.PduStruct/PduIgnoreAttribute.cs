using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class PduIgnoreAttribute : Attribute
	{
	}
}
