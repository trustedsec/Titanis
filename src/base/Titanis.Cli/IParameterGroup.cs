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
		void Initialize(Command command);
	}
}
