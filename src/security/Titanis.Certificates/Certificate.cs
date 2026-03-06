using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1.Serialization;

namespace Titanis.Certificates
{
	public class Certificate
	{
		public Certificate(byte[] bytes)
		{
			ArgumentNullException.ThrowIfNull(bytes);

			PKIX1Explicit88.Certificate cert;
			try
			{
				cert = Asn1DerDecoder.DecodeTlv<PKIX1Explicit88.Certificate>(bytes);
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"An error occurred while parsing the bytes: {ex.Message}", nameof(bytes), ex);
			}

			Bytes = bytes;
		}

		public byte[] Bytes { get; }
	}
}
