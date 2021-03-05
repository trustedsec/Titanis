using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Specifies one or more aliases for a command or parameter property.
	/// </summary>
	/// <seealso cref="ParameterAttribute"/>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
	public sealed class AliasAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="AliasAttribute"/>.
		/// </summary>
		/// <param name="aliases">List of aliases for t</param>
		public AliasAttribute(params string[] aliases)
		{
			this.Aliases = aliases;
		}

		/// <summary>
		/// Gets a list of aliases.
		/// </summary>
		public string[] Aliases { get; }
	}
}
