namespace Titanis.DceRpc
{
	[System.CodeDom.Compiler.GeneratedCode("Animus IDL Compiler", "0.9")]
	public struct COMVERSION : IRpcFixedStruct
	{
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.MajorVersion);
			encoder.WriteValue(this.MinorVersion);
		}
		public void Decode(IRpcDecoder decoder)
		{
			this.MajorVersion = decoder.ReadUInt16();
			this.MinorVersion = decoder.ReadUInt16();
		}
		public ushort MajorVersion;
		public ushort MinorVersion;
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}
}
