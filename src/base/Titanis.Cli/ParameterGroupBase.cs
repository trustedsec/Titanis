using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
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
		/// <summary>
		/// Gets the object owning this group.
		/// </summary>
        protected object? Owner { get; private set; }

        /// <inheritdoc/>
        void IParameterGroup.Initialize(IServiceContainer services, object? owner)
		{
			this.Services = services;
			this.Owner = owner;
			this.Initialize(services);
		}
		/// <summary>
		/// Called when the parameter group is initialized.
		/// </summary>
		/// <param name="services">Services available to the group</param>
		protected virtual void Initialize(IServiceContainer services) { }

		protected IFileAccess RequireFileAccess() => this.Services?.RequireService<IFileAccess>();
		protected string ResolveFsPath(FileSpec path) => this.RequireFileAccess().ResolveFsPath(path);
		protected ILog? Log => this.Services?.GetService<ILog>();

		protected TCallback? GetCallback<TCallback>()
			where TCallback : class
			=> null;


		protected static byte[] LoadCertFile(IFileAccess fileAccess, FileSpec fileName, [CallerArgumentExpression(nameof(fileName))] string? argName = null)
		{
			byte[] certBytes = fileAccess.ReadAllBytesFrom(fileName);
			if (certBytes.Length == 0)
				throw new ArgumentException($"File {fileName} does not contain any data.", argName);

			return certBytes;
		}
	}
}

namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Parameter)]
	sealed class CallerArgumentExpressionAttribute : Attribute
	{
		public CallerArgumentExpressionAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; }
	}
}