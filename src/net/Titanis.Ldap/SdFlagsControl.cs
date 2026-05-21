using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.Winterop.Security;

namespace Titanis.Ldap
{
	// [MS-ADTS] § 3.1.1.3.4.1.11 LDAP_SERVER_SD_FLAGS_OID
	class SdFlagsControl : IAsn1DerEncodableValue, IAsn1DerEncodableTlv
	{
		public SdFlagsControl(SecurityInfo sections)
		{
			this.SecurityInfo = sections;
		}

		public Asn1Tag Tag => new Asn1Tag(Asn1PredefTag.Sequence, Asn1TagFlags.Constructed);

		internal SecurityInfo SecurityInfo { get; set; }

		public void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeValueTlv(this, this.Tag);
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeInt32Tlv((int)this.SecurityInfo);
		}
	}
}
