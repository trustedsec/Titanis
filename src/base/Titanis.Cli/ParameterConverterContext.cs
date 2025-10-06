using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Text;

namespace Titanis.Cli
{
	public class ParameterConverterContext : ITypeDescriptorContext
	{
		internal ParameterConverterContext(
			Command command
			)
		{
			this.Command = command;
			// IN actual use cases, Parameter is always set before instances are actually used
			this.Parameter = null!;
		}

		public Command Command { get; set; }
		public ParameterMetadata Parameter { get; internal set; }

		/// <inheritdoc/>
		public IContainer? Container => null;

		/// <inheritdoc/>
		public object Instance => this.Command;

		/// <inheritdoc/>
		public PropertyDescriptor PropertyDescriptor => this.Parameter.Property;

		/// <inheritdoc/>
		public object? GetService(Type serviceType) => this.Command.GetService(serviceType);

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
