namespace Titanis.Ldap
{
    public sealed class DnsRecordSyntax : Asn1EncodedSyntax<DnsRecordInfo>
	{
		internal DnsRecordSyntax()
		{

		}

		/// <inheritdoc/>
		public sealed override string? RfcName => "Binary";
		/// <inheritdoc/>
		public sealed override string RfcOid => "OctetString";
		/// <inheritdoc/>
		public sealed override string ActiveDirectoryName => "Object(Replica-Link)";
		/// <inheritdoc/>
		public sealed override string? ActiveDirectoryOid => "2.5.5.10";
		/// <inheritdoc/>
		public sealed override int OmSyntax => 127;
		/// <inheritdoc/>
		public sealed override string? OmObjectClass => "1.2.840.113556.1.1.1.6";

		protected sealed override DnsRecordInfo DecodeOctets(byte[] bytes)
		{
			return new DnsRecordInfo(bytes);
		}

		protected sealed override byte[] EncodeOctets(DnsRecordInfo value)
		{
			return value.Bytes;
		}

		public override object Parse(string text)
		{
			return new BinaryString(BinaryHelper.ParseHexString(text));
		}
	}

}
