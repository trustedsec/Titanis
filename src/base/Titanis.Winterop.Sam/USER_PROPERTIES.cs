using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.PduStruct;

namespace Titanis.Winterop.Sam
{
	// [MS-SAMR] § 2.2.10 Supplemental Credentials Structures
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct USER_PROPERTIES
	{
		private uint reserved1;
		private uint length;
		private ushort reserved2;
		private ushort reserved3;
		private ulong reserved4_0;
		private ulong reserved4_1;
		private ulong reserved4_2;
		private ulong reserved4_3;
		private ulong reserved4_4;
		private ulong reserved4_5;
		private ulong reserved4_6;
		private ulong reserved4_7;
		private ulong reserved4_8;
		private ulong reserved4_9;
		private ulong reserved4_10;
		private ulong reserved4_11;

		public ushort propertySignature;
		public ushort propertyCount;

		[PduArraySize(nameof(propertyCount))]
		public USER_PROPERTY[] properties;
	}

	// [MS-SAMR] § 2.2.10 Supplemental Credentials Structures
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct USER_PROPERTY
	{
		private ushort nameLength;
		private ushort valueLength;
		private ushort reserved;

		[PduString(System.Runtime.InteropServices.CharSet.Unicode, nameof(nameLength))]
		public string name;
		[PduString(System.Runtime.InteropServices.CharSet.Ansi, nameof(valueLength))]
		public string valueBytes;
	}

	// [MS-SAMR] § 2.2.10.3 Primary:WDigest - WDIGEST_CREDENTIALS
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct WDIGEST_CREDENTIALS
	{
		private ushort reserved1_2;
		private byte version;
		private byte numberOfHashes;
		[PduArraySize(nameof(numberOfHashes))]
		public WDigestHash[] hashes;
	}
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct WDigestHash
	{
		[PduArraySize(16)]
		public byte[] bytes;
	}

	// [MS-SAMR] § 2.2.10.4 Primary:Kerberos - KERB_STORED_CREDENTIAL
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct KERB_STORED_CREDENTIAL
	{
		[PduPosition]
		private long pduOffset;

		private ushort revision;
		private ushort flags;
		private ushort credCount;
		private ushort oldCredCount;
		internal ushort defaultSaltLength;
		internal ushort defaultSaltMaxLength;
		internal int defaultSaltOffset;

		[PduArraySize(nameof(credCount))]
		internal KERB_KEY_DATA[] credentials;
		[PduArraySize(nameof(oldCredCount))]
		internal KERB_KEY_DATA[] oldCredentials;
	}

	// [MS-SAMR] § 2.2.10.5 KERB_KEY_DATA
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct KERB_KEY_DATA
	{
		private ushort reserved1;
		private ushort reserved2;
		private uint reserved3;
		internal uint keyType;
		internal int keyLength;
		internal int keyOffset;
	}

	// [MS-SAMR] § 2.2.10.6 Primary:Kerberos-Newer-Keys - KERB_STORED_CREDENTIAL_NEW
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct KERB_STORED_CREDENTIAL_NEW
	{
		[PduPosition]
		private long pduOffset;

		private ushort revision;
		private ushort flags;
		private ushort credCount;
		private ushort serviceCredCount;
		private ushort oldCredCount;
		private ushort olderCredCount;
		internal ushort defaultSaltLength;
		internal ushort defaultSaltMaxLength;
		internal int defaultSaltOffset;
		internal int defaultiterationCount;

		[PduArraySize(nameof(credCount))]
		internal KERB_KEY_DATA_NEW[] credentials;
		[PduArraySize(nameof(serviceCredCount))]
		internal KERB_KEY_DATA_NEW[] serviceCredentials;
		[PduArraySize(nameof(oldCredCount))]
		internal KERB_KEY_DATA_NEW[] oldCredentials;
		[PduArraySize(nameof(olderCredCount))]
		internal KERB_KEY_DATA_NEW[] olderCredentials;
	}

	// [MS-SAMR] § 2.2.10.7 KERB_KEY_DATA_NEW
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	internal partial struct KERB_KEY_DATA_NEW
	{
		private ushort reserved1;
		private ushort reserved2;
		private uint reserved3;
		internal int iterationCount;
		internal uint keyType;
		internal int keyLength;
		internal int keyOffset;
	}
}
