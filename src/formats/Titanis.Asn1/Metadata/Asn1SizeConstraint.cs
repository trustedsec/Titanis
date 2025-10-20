namespace Titanis.Asn1.Metadata
{
	public sealed class Asn1SizeConstraint : Asn1Constraint
	{
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

		public long? MinSize { get; }
		public long? MaxSize { get; }

		public sealed override string ToString()
			=> (this.MinSize.HasValue && this.MinSize == this.MaxSize) ? $"SIZE({this.MinSize.ToString()})"
			: $"SIZE({new Asn1Int64Range(this.MinSize, true, this.MaxSize, true)})";
	}
}