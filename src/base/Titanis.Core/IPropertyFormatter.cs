using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Provides formatting for a property value.
	/// </summary>
	public interface IPropertyFormatter
	{
		string? Format(string format, object? value, PropertyDescriptor property);
	}
}
