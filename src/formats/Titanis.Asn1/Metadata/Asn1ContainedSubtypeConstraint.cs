using System;
using System.Collections.Generic;
using System.Text;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1ContainedSubtypeConstraint : Asn1Constraint
	{
		public Asn1ContainedSubtypeConstraint(Asn1Type baseType)
		{
			if (baseType is null)
				throw new ArgumentNullException(nameof(baseType));

			this.Subtype = baseType;
		}

		public Asn1Type Subtype { get; }

		/// <inheritdoc/>
		public sealed override string ToString() => this.Subtype.ToString();
	}
}
