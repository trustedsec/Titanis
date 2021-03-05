using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies the name of the placeholder for this parameter.
	/// </summary>
	/// <see cref="ParameterMetadata.Placeholder"/>
	public class PlaceholderAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="PlaceholderAttribute"/>.
		/// </summary>
		/// <param name="name">Placeholder name</param>
		public PlaceholderAttribute(string name)
		{
			this.Name = name;
		}

		/// <summary>
		/// Gets the name of the placeholder.
		/// </summary>
		public string Name { get; }
	}
}
