using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a field or property as a list.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class PduListAttribute : Attribute
	{
		public PduListAttribute()
		{
		}

		public string? PredicateMember { get; set; }
		public string? SizeMember { get; set; }
	}
}
