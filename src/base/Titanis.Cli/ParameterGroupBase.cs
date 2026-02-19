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
		/// Gets the service container supporting the group.
		/// </summary>
		protected IServiceContainer? Services { get; private set; }
		/// <inheritdoc/>
		void IParameterGroup.Initialize(IServiceContainer services)
		{
			this.Initialize(services);
		}
		/// <summary>
		/// Called when the parameter group is initialized.
		/// </summary>
		/// <param name="services">Services available to the group</param>
		protected virtual void Initialize(IServiceContainer services) { }

		protected IFileAccess RequireFileAccess() => this.Services?.RequireService<IFileAccess>();
		protected string ResolveFsPath(string path) => this.RequireFileAccess().ResolveFsPath(path);
		protected ILog? Log => this.Services?.GetService<ILog>();

		protected TCallback? GetCallback<TCallback>()
			where TCallback : class
			=> null;
	}
}
