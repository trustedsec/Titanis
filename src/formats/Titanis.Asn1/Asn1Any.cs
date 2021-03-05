using System;
using System.Collections.Generic;
using System.Text;
using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace Titanis.Asn1
{
	public class Asn1Any : IAsn1Tag, IAsn1DerEncodableTlv
	{
		private byte[] _tlvData;
		private Memory<byte> _innerData;
		public Asn1Tag Tag { get; private set; }

		public Asn1Any()
		{

		}

		public static Asn1Any CreateFrom<T>(T obj)
			where T : IAsn1DerEncodableTlv
		{
			var any = new Asn1Any
			{
				Tag = obj.Tag
			};
			any.SetTlvData(Asn1DerEncoder.EncodeTlv(obj).ToArray());
			return any;
		}

		private void SetTlvData(byte[] tlvData)
		{
			this._tlvData = tlvData;
			byte lengthByte = tlvData[1];
			int offData;
			if (lengthByte < 0x80)
			{
				offData = 2;
			}
			else
			{
				offData = 2 + (lengthByte - 0x80);
			}

			this._innerData = new Memory<byte>(tlvData, offData, tlvData.Length - offData);
		}

		public T DecodeAs<T>()
			where T : IAsn1DerEncodableTlv, new()
		{
			T obj = new T();

			Asn1DerDecoder decoder = new Asn1DerDecoder(new ByteMemoryReader(this._tlvData));
			obj.DecodeTlv(decoder);
			return obj;
		}

		public void DecodeTlv(Asn1DerDecoder decoder)
		{
			var tlvData = decoder.DecodeOctetString();
			if (tlvData.Length > 0)
			{
				// TODO: Do this without instantiating a reader and decoder
				var subreader = new ByteMemoryReader(tlvData);
				var subdec = new Asn1DerDecoder(subreader);
				this.Tag = subdec.PeekTag();
			}
			else
			{
				this.Tag = Asn1PredefTag.Unspecified;
			}

			this.SetTlvData(tlvData);
		}

		public void DecodeValue(Asn1DerDecoder decoder)
		{
			this._innerData = decoder.DecodeOctetString();
		}

		public bool TryDecodeTlv(Asn1DerDecoder decoder)
		{
			this.DecodeTlv(decoder);
			return true;
		}

		public void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeOctetString(this._innerData.Span);
		}
	}
}
