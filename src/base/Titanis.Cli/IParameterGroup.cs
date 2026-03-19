using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
		/// <param name="services">Service container to add services to</param>
		/// <param name="owner">The object owning the parameter group</param>
		/// <remarks>
		/// The owner is usually the root command object.
		/// </remarks>
		void Initialize(IServiceContainer services, object? owner);
	}
}
