using System;
using System.Diagnostics.SymbolStore;

namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1Int64RangeConstraint : Asn1ValueSetConstraint
	{
		public Asn1Int64RangeConstraint(Asn1Int64Range range)
		{
			this.Range = range;
		}

		/// <inheritdoc/>
		public sealed override string ToString() => this.Range.ToString();

		public Asn1Int64Range Range { get; }

		public sealed override Asn1Int64Range? TryGetInt64Range() => this.Range;
		public sealed override Asn1UInt64Range? TryGetUInt64Range()
		{
			var range = this.Range;
			bool canUnsign = (range.Min >= 0);
			return (canUnsign) ? new Asn1UInt64Range((ulong?)range.Min, range.IncludesMin, (ulong?)range.Max, range.IncludesMax) : default;
		}
	}
}