using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		}

		public Command Command { get; set; }
		public ParameterMetadata Parameter { get; internal set; }

		/// <inheritdoc/>
		public IContainer Container => null;

		/// <inheritdoc/>
		public object Instance => this.Command;

		/// <inheritdoc/>
		public PropertyDescriptor PropertyDescriptor => null;

		/// <inheritdoc/>
		public object GetService(Type serviceType)
		{
			return null;
		}

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
