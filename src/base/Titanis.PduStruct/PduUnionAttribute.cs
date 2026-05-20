using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.PduStruct
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class PduUnionAttribute : Attribute
	{
		public PduUnionAttribute()
		{
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class PduSwitchAttribute : Attribute
	{
		public PduSwitchAttribute()
		{
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public sealed class PduCaseAttribute : Attribute
	{
		public PduCaseAttribute(object value)
		{
			this.Value = value;
		}

		public object Value { get; }
	}
}
