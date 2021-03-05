using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class PduFieldAttribute : Attribute
	{
		public string? ReadMethod { get; set; }
		public string? WriteMethod { get; set; }
	}
}
