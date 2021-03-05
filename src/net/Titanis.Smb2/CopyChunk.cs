namespace Titanis.Smb2
{
	public partial class Smb2OpenFileObjectBase
	{
		public struct CopyChunk
		{
			public const int StructSize = 24;

			public CopyChunk(
				long sourceOffset,
				long targetOffset,
				int length
				)
			{
				this.sourceOffset = sourceOffset;
				this.targetOffset = targetOffset;
				this.length = length;
				this.reserved = 0;
			}

			public long sourceOffset;
			public long targetOffset;
			public int length;
			private readonly int reserved;
		}

	}
}