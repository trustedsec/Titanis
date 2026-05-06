namespace Titanis.Winterop.SamServer
{
	/// <summary>
	/// Describes a Kerberos key
	/// </summary>
	public sealed class KerberosKeyInfo
	{
		internal KerberosKeyInfo(int? kvno, uint keyType, byte[] bytes)
		{
			this.Kvno = kvno;
			this.KeyType = keyType;
			this.Bytes = bytes;
		}

		public KerberosKeyInfo(int? kvno, uint keyType, byte[] bytes, int iterationCount) : this(kvno, keyType, bytes)
		{
			IterationCount = iterationCount;
		}

		public sealed override string ToString() => $"{this.KeyType}: {this.Bytes.ToHexString()}";

		/// <summary>
		/// Gets the key version number.
		/// </summary>
		public int? Kvno { get; }
		/// <summary>
		/// Gets the key encryption type.
		/// </summary>
		public uint KeyType { get; }
		/// <summary>
		/// Gets the bytes constituting the key.
		/// </summary>
		public byte[] Bytes { get; }
		/// <summary>
		/// Gets the number of iterations used to calculate the key.
		/// </summary>
		public int IterationCount { get; }
	}
}