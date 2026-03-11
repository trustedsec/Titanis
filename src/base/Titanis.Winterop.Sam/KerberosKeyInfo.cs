namespace Titanis.Winterop.SamServer
{
	public sealed class KerberosKeyInfo
	{
		internal KerberosKeyInfo(uint KeyType, byte[] bytes)
		{
			this.KeyType = KeyType;
			this.Bytes = bytes;
		}

		public KerberosKeyInfo(uint KeyType, byte[] bytes, int iterationCount) : this(KeyType, bytes)
		{
			IterationCount = iterationCount;
		}

		public sealed override string ToString() => $"{this.KeyType}: {this.Bytes.ToHexString()}";

		public uint KeyType { get; }
		public byte[] Bytes { get; }
		public int IterationCount { get; }
	}
}