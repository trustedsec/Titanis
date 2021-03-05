using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a field or property as an array.
	/// </summary>
	/// <remarks>
	/// If the size of the array is constant, use <see cref="PduArraySizeAttribute(int)"/>.
	/// Otherwise, use <see cref="PduArraySizeAttribute(string)"/>, passing the name of a property or method
	/// that returns the array length at runtime.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class PduArraySizeAttribute : Attribute
	{
		public PduArraySizeAttribute(int constSize)
		{
			this.ConstSize = constSize;
		}

		public PduArraySizeAttribute(string methodName)
		{
			this.MethodName = methodName;
		}

		public int ConstSize { get; }
		public string? MethodName { get; }
	}
}
