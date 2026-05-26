using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a field or property as a list.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class PduOffsetAttribute : Attribute
	{
		public PduOffsetAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		public string MemberName { get; }
	}
}
