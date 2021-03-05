using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis
{
	/// <summary>
	/// Represents a named object.
	/// </summary>
	public interface INamedObject
	{
		/// <summary>
		/// Gets the name of the object
		/// </summary>
		string Name { get; }
	}
}
