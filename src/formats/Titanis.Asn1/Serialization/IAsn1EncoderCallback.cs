namespace Titanis.Asn1.Serialization
{
	public interface IAsn1EncoderCallback
	{
		void OnCloseTlv(Asn1DerEncoder encoder, Asn1Tag tag);
	}
}