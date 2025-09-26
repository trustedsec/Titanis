using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Allows a parameter group to receive notifications during command execution.
	/// </summary>
	public interface IParameterGroup
	{
		/// <summary>
		/// Called by the command line parser when creating the parameter group.
		/// </summary>
		/// <param name="owner">Owning <see cref="Command"/></param>
		void Initialize(Command owner);
	}
}
