namespace Titanis.Asn1
{
	public enum Asn1PredefTag
	{
		Unspecified = 0,

		// § 18.2
		Boolean = 1,
		// § 19.8
		Integer = 2,

		// § 20.7
		Enumerated = 0xA,
		// § 21.2
		Real = 9,
		// § 22.8
		BitString = 3,
		// § 23.2
		OctetString = 4,
		// § 24.2
		Null = 5,
		// § 25.17, 26.2
		Sequence = 0x10,
		// § 27.5
		Set = 0x11,
		// § 32.2
		ObjectIdentifier = 6,
		// § 33.2
		RelativeOid = 0xD,
		// § 34.2
		Iri = 0x23,
		// § 35.2
		RelativeIri = 0x24,
		// § 38.2
		Time = 0xE,
		// § 38.4.1
		Date = 0x1F,
		// § 38.4.2
		TimeOfDay = 0x20,
		// § 38.4.3
		DateTime = 0x21,
		// § 38.4.4
		Duration = 0x22,

		UTF8String = 0xC,
		NumericString = 0x12,
		PrintableString = 0x13,
		TeletexString = 0x14,
		T61String = TeletexString,
		VideotexString = 0x15,
		IA5String = 0x16,
		GraphicString = 0x19,
		Iso646String = 0x1A,
		VisibleString = Iso646String,
		GeneralString = 0x1B,
		UniversalString = 0x1C,
		CharacterString = 0x1D,
		BMPString = 0x1E,

		// § 46.3
		GeneralizedTime = 0x18,

		// § 47.3
		UtcTime = 0x17,
	}
}
