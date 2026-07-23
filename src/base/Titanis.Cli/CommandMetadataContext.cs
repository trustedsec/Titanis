using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Titanis.Cli
{
	public class CommandMetadataContext
	{
		public CommandMetadataContext(
			MetadataResolver resolver
			)
		{
			this.Resolver = resolver;

			this.ComponentCatalog = new ComponentCatalog(resolver);
		}

		public MetadataResolver Resolver { get; }
		public ComponentCatalog ComponentCatalog { get; }

		public PropertyDescriptorCollection GetProperties(Type recordType) => this.Resolver.GetDescriptor(recordType).GetProperties();
	}
}
