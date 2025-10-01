using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Titanis.Cli
{
	/// <summary>
	/// Base class for parameter group classes.
	/// </summary>
	public abstract class ParameterGroupBase : IParameterGroup
	{
		/// <summary>
		/// Gets the <see cref="Command"/> owning the parameter group.
		/// </summary>
		protected Command? Owner { get; private set; }
		protected IServiceContainer? Services { get; private set; }
		/// <inheritdoc/>
		void IParameterGroup.Initialize(Command command, IServiceContainer services)
		{
			this.Owner = command;
			this.Services = services;
			this.Initialize(command, services);
		}
		/// <summary>
		/// Called when the parameter group is initialized.
		/// </summary>
		/// <param name="owner"></param>
		protected virtual void Initialize(Command owner, IServiceContainer services) { }
	}
}
