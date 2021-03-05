using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Marks a property as a parameter.
	/// </summary>
	/// <remarks>
	/// By default, a parameter group is only instantiated if at least one of the parameters is set.
	/// <see cref="ParameterGroupAttribute.ParameterGroupAttribute(ParameterGroupOptions)"/> to specify that a group should always be instantiated.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ParameterGroupAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="ParameterAttribute"/>
		/// </summary>
		public ParameterGroupAttribute()
		{

		}

		/// <summary>
		/// Initializes a new <see cref="ParameterAttribute"/>
		/// </summary>
		/// <param name="options"><see cref="ParameterGroupOptions"/> specifying the behavior of this group.</param>
		public ParameterGroupAttribute(ParameterGroupOptions options)
		{
			this.Options = options;
		}

		/// <summary>
		/// Gets a <see cref="ParameterGroupOptions"/> specifying the behavior of this group.
		/// </summary>
		public ParameterGroupOptions Options { get; }
	}
}
