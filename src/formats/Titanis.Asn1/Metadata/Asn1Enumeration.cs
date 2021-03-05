using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1Enumeration
	{
		public Asn1NamedNumber[] Items { get; }

		public Asn1Enumeration(Asn1NamedNumber[] items)
		{
			if (items.IsNullOrEmpty())
				throw new ArgumentNullException(nameof(items));

			this.Items = items;
		}
	}
}
