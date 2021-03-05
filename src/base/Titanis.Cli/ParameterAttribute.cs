using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Marks a property as a parameter.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ParameterAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new <see cref="ParameterAttribute"/>
		/// </summary>
		public ParameterAttribute()
		{

		}

		internal ParameterAttribute(ParameterFlags flags)
		{
			this.Flags = flags;
		}

		/// <summary>
		/// Initializes a new <see cref="ParameterAttribute"/>
		/// </summary>
		/// <remarks>Position of the parameter</remarks>
		public ParameterAttribute(int position)
		{
			this.Position = position;
		}

		internal ParameterFlags Flags { get; }

		/// <summary>
		/// The value of <see cref="Position"/> indicating a parameter
		/// is not positional.
		/// </summary>
		public const int NoPosition = int.MinValue;
		/// <summary>
		/// Gets the position of the parameter.
		/// </summary>
		/// <remarks>
		/// The value of this property is considered relative to other parameters and
		/// is not treated as an absolute position.
		/// </remarks>
		public int Position { get; } = NoPosition;
	}
}
