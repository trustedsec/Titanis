using System.IO;
using Titanis.IO;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.77 - Signature
	[PduStruct]
	partial struct EncodingUnitSignature
	{
		internal const uint ValidValue = 0x12345678;

		internal uint value;

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			this.value = ValidValue;
		}
		partial void OnAfterReadPdu(IByteSource source)
		{
			if (this.value != ValidValue)
				throw new InvalidDataException("The data is not a valid WMI EncodingUnit.");
		}
	}
}
