using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;

namespace Titanis.Cli
{
	[Flags]
	enum ParameterConverterContextOptions
	{
		None = 0,
		ForDefault = 1,
	}
	/// <summary>
	/// Provides context information when converting a value for a parameter.
	/// </summary>
	public class ParameterConverterContext : ITypeDescriptorContext
	{
		private readonly ParameterConverterContextOptions _options;

		internal ParameterConverterContext(
			Command? command,
			ParameterMetadata parameter,
			ParameterConverterContextOptions options
			)
		{
			this.Command = command;
			// IN actual use cases, Parameter is always set before instances are actually used
			this.Parameter = parameter;
			this._options = options;
		}

		public Command? Command { get; set; }
		public ParameterMetadata Parameter { get; internal set; }
		public bool IsForDefault => 0 != (this._options & ParameterConverterContextOptions.ForDefault);

		/// <inheritdoc/>
		public IContainer? Container => null;

		/// <inheritdoc/>
		public object? Instance => this.Command;

		/// <inheritdoc/>
		public PropertyDescriptor PropertyDescriptor => this.Parameter.Property;

		/// <inheritdoc/>
		public object? GetService(Type serviceType) => this.Command?.GetService(serviceType);

		/// <inheritdoc/>
		public void OnComponentChanged()
		{
		}

		/// <inheritdoc/>
		public bool OnComponentChanging()
		{
			return true;
		}
	}
}
