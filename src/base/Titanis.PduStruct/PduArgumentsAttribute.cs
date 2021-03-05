using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class PduArgumentsAttribute : Attribute
	{
		public PduArgumentsAttribute(params string[] memberNames)
		{
			this.MemberNames = memberNames;
		}

		public string[] MemberNames { get; }
	}
}
