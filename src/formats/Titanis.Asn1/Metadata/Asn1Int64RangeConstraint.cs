namespace Titanis.Asn1.Metadata
{
	public class Asn1Int64RangeConstraint : Asn1Constraint
	{
		private Asn1Int64Range? Range;

		public Asn1Int64RangeConstraint(Asn1Int64Range range)
		{
			this.Range = range;
		}
	}
}