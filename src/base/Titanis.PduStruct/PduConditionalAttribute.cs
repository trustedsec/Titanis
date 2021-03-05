using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.PduStruct
{
	/// <summary>
	/// Marks a field as being optional.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class PduConditionalAttribute : Attribute
	{
		public PduConditionalAttribute(string conditionFieldName)
		{
			this.ConditionFieldName = conditionFieldName;
		}

		public string ConditionFieldName { get; }
	}
}
