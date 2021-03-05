using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Marks a property or class as a file size.
	/// </summary>
	/// <remarks>
	/// Consumers may use this as a hint to format the value for display.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class FileSizeAttribute : Attribute
	{
	}
}
