using KerberosV5Spec2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.Asn1.Serialization;
using Titanis.IO;
using Titanis.PduStruct;

namespace Titanis.Security.Kerberos
{
	enum GssapiTokenId : ushort
	{
		APReq = 0x0100,
		APRep = 0x0200,
		Error = 0x0300,
		TgtReq = 0x0400,
		TgtRep = 0x0401,
	}

	// [RFC 4121] § 4.1.1 - Authenticator Checksum
	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial struct AuthChecksumToken
	{
		// [RFC 4121] § 4.1.1 - Authenticator Checksum
		public const int ChecksumType = 0x8003;

		internal uint bindLength;
		internal Guid channelBind;
		internal SecurityCapabilities capabilities;
		private bool HasDelegation => 0 != (this.capabilities & SecurityCapabilities.Delegation);

		[PduConditional(nameof(HasDelegation))]
		private DelegationToken? _delegationToken;

		public DelegationToken? DelegationToken
		{
			get => this._delegationToken;
			set
			{
				this._delegationToken = value;
				if (value != null)
					this.capabilities |= SecurityCapabilities.Delegation;
			}
		}

		public byte[] ToBytes()
		{
			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(this);
			return writer.GetData().ToArray();
		}
	}

	[PduStruct]
	[PduByteOrder(PduByteOrder.LittleEndian)]
	partial class DelegationToken
	{
		internal ushort option;

		private ushort Length;

		[PduIgnore]
		private byte[] _bytes;
		[PduField]
		[PduArraySize(nameof(Length))]
		public byte[] Bytes
		{
			get => this._bytes;
			set
			{
				this._bytes = value;
				this.Length = checked((ushort)(value?.Length ?? 0));
				this.option = (this.Length > 0) ? (ushort)1 : (ushort)0;
			}
		}
	}
}
