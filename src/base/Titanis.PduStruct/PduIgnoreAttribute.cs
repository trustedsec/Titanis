using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a field to be ignored when reading or writing a type marked with <see cref="PduStructAttribute"/>.
	/// </summary>
	/// <remarks>
	/// This attribute cannot be applied to properties which are ignored by default.  The backing field is serialized.  The user may opt out by applying this attribute to the backing field.  Since this attribute cannot be applied to properties, the compiler will catch errors where the user applies this attribute to a property, forgetting to specify <c>field:</c> to the attribute application.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class PduIgnoreAttribute : Attribute
	{
	}
}
