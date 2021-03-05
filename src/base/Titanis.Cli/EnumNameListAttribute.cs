using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// When used on a parameter, provides a list of values containing the names of the members of one or more <see langword="enum"/> types.
	/// </summary>
	/// <remarks>
	/// Multiple types are supported.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
	public sealed class EnumNameListAttribute : Attribute
	{
		public EnumNameListAttribute(params Type[] enumTypes)
		{
			EnumTypes = enumTypes;
		}

		/// <summary>
		/// Gets the <see langword="enum"/> types.
		/// </summary>
		public Type[] EnumTypes { get; }
	}
}
