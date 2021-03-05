using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Titanis.Security.Kerberos
{
	// [RFC 4121] § 4.2.6.1. MIC Tokens
	enum WrapTokenType : ushort
	{
		// Reversed (big-endian)
		Wrap = 0x0504,
		Sign = 0x0404,
	}

	// [RFC 4121] § 4.2.6.1. MIC Tokens
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct SignToken
	{
		internal static unsafe int StructSize => sizeof(SignToken);

		internal WrapTokenType tokID;
		internal WrapFlags flags;
		internal byte filler_FF;
		internal uint filler2_FF;
		internal ulong seqNbr;
	}

	// [RFC 4121] 4.2.2.  Flags Field
	[Flags]
	enum WrapFlags : byte
	{
		None = 0,

		SentByAcceptor = (1 << 0),
		Sealed = (1 << 1),
		AcceptorSubkey = (1 << 2),
	}

	// [RFC 4121] § 4.2.6.2.  Wrap Tokens
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	record struct WrapToken
	{
		internal static unsafe int StructSize => sizeof(WrapToken);

		private UInt16NE tokID;
		internal WrapTokenType TokID
		{
			get => (WrapTokenType)tokID.Value;
			set => tokID.Value = (ushort)value;
		}
		internal WrapFlags flags;
		internal byte filler_FF;
		private UInt16NE extraCount;
		internal ushort ExtraCount
		{
			get => this.extraCount.Value;
			set => this.extraCount.Value = value;
		}
		private UInt16NE rrc;
		public ushort Rrc
		{
			get => this.rrc.Value;
			set => this.rrc.Value = value;
		}
		private UInt64NE seqNbr;
		internal ulong SeqNbr
		{
			get => this.seqNbr.Value;
			set => this.seqNbr.Value = value;
		}
	}
}
