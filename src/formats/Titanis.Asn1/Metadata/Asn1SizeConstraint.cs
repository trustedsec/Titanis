namespace Titanis.Asn1.Metadata
{
	public class Asn1SizeConstraint : Asn1Constraint
	{
		public long? MinSize { get; }
		public long? MaxSize { get; }

		public Asn1SizeConstraint(long size)
		{
			this.MinSize = size;
			this.MaxSize = size;
		}
		public Asn1SizeConstraint(long? min, long? max)
		{
			this.MinSize = min;
			this.MaxSize = max;
		}
	}
}