using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1NamedNumber
	{
		public string Name { get; }
		public long Value { get; }

		public Asn1NamedNumber(string name, long value)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			this.Name = name;
			this.Value = value;
		}
	}
}
