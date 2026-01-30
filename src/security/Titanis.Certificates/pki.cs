using PKIX1Explicit88;
using PKIX1Implicit88;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;
using Titanis.IO;

namespace PKIX1Explicit88
{
	class Extensions : Asn1SequenceOf<Extension>
	{
		public Extensions(Extension[] values) : base(values)
		{
		}
	}
}

namespace PKIX1Implicit88
{
	class GeneralNames : Asn1SequenceOf<GeneralName>
	{
		public GeneralNames(GeneralName[] values) : base(values)
		{
		}
	}

	public static partial class SubjectAltName
	{
		public const string SubjectAltNameOid = "1.3.6.1.4.1.311.20.2.3";

		public static string? TryReadFrom(ReadOnlyMemory<byte> bytes)
		{
			var names = Asn1DerDecoder.DecodeTlv<Asn1SequenceOf<GeneralName>>(bytes);
			if (
				names.Values.Length == 1
				&& names.Values[0].SelectedChoice == GeneralName.ChoiceIndex.OtherName
				// [MS-WCCE] § 2.2.2.7.5 - szOID_NT_PRINCIPAL_NAME
				&& names.Values[0].OtherName.type_id.ToString() == SubjectAltNameOid
				)
			{
				var decoded = Asn1DerDecoder.DecodeStringTlv<UTF8String>(names.Values[0].OtherName.value.TlvBytes);
				return decoded;
			}
			return null;
		}
	}
}
