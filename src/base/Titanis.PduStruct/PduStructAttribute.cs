using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a type as being used in a PDU.
	/// </summary>
	/// <remarks>
	/// When a type is marked with this attribute, the source generator generates
	/// an implementation for IPduStruct.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class PduStructAttribute : Attribute
	{
	}
}
