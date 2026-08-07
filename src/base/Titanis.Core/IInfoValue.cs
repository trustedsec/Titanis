using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class InfoValueAttribute : Attribute
	{
		public InfoValueAttribute(Type dataType)
		{
			DataType = dataType;
		}

		public Type DataType { get; }
	}

	public interface IInfoValue
	{
		object? GetValue();
	}
}
