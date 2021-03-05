using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	public enum NtlmMessageType : int
	{
		Negotiate = 1,
		Challenge = 2,
		Authenticate = 3,
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NtlmStringInfo
	{
		public ushort len;
		public ushort maxLen;
		public int offset;

		public NtlmStringInfo(ushort len, int offset)
		{
			this.len = len;
			this.maxLen = len;
			this.offset = offset;
		}
		public NtlmStringInfo(ushort len, ushort maxLen, int offset)
		{
			this.len = len;
			this.maxLen = maxLen;
			this.offset = offset;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NtlmVersion
	{
		public byte majorVersion;
		public byte minorVersion;
		public ushort build;
		private ushort reserved1;
		private byte reserved2;
		public byte revision;

		public override string ToString() => $"{this.majorVersion}.{this.minorVersion}.{this.build}.{this.revision}";
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NegotiateHeader
	{
		public static unsafe int StructSize => sizeof(NegotiateHeader);

		public const ulong ValidSignature = 0x005053534D4C544E;
		// NTLMSSP\0 = 4E 54 4C 4D 53 53 50 0

		internal ulong signature;
		internal NtlmMessageType messageType;
		internal NegotiateFlags negotiatedFlags;
		internal NtlmStringInfo domain;
		internal NtlmStringInfo workstation;

		internal NtlmVersion version;
	}

	enum RespType : byte
	{
		V1 = 1
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct ClientChallengeHeader
	{
		internal RespType respType;
		internal RespType hiRespType;
		internal ushort reserved1;
		internal uint reserved2;
		internal DateTime timestamp;
		internal ulong challengeFromClient;
		internal uint reserved3;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NtlmChallengeHeader
	{
		public static unsafe int StructSize => sizeof(NtlmChallengeHeader);

		public ulong signature;
		public NtlmMessageType messageType;
		public NtlmStringInfo targetName;
		public NegotiateFlags negotiateFlags;
		public ulong serverChallenge;
		public ulong reserved;
		public NtlmStringInfo targetInfo;
		public NtlmVersion version;
	}

	enum AvId : ushort
	{
		Eol = 0,
		NbComputerName = 1,
		NbDomainName = 2,
		DnsComputerName = 3,
		DnsDomainName = 4,
		DnsTreeName = 5,
		Flags = 6,
		Timestamp = 7,
		SingleHost = 8,
		TargetName = 9,
		ChannelBindings = 10
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct AvHeader
	{
		public static unsafe int StructSize => sizeof(AvHeader);

		internal AvId id;
		internal ushort avLen;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct NtlmAuthenticateHeader
	{
		public static unsafe int StructSize => sizeof(NtlmAuthenticateHeader);

		public ulong signature;
		public NtlmMessageType messageType;
		public NtlmStringInfo lmChallengeResponse;
		public NtlmStringInfo ntChallengeResponse;
		public NtlmStringInfo domain;
		public NtlmStringInfo userName;
		public NtlmStringInfo workstation;
		public NtlmStringInfo sessionKey;
		public NegotiateFlags negotiatedFlags;
		public NtlmVersion version;
		//internal Buffer128 mic;
	}

	public class NtlmAuthenticate
	{
		public NtlmAuthenticateHeader hdr;

		public Buffer192 lmResponse;
		public Buffer192 ntResponse;
		public string? domain;
		public string? userName;
		public string? workstation;
		public Buffer128 sessionKey;
		public NtlmAvInfo? avInfo;
		public NtlmClientChallenge clientChallenge;

		public bool IsNtlmv2 => this.avInfo != null;

		public static NtlmAuthenticate Parse(
			ReadOnlySpan<byte> bytes,
			out ReadOnlySpan<byte> targetInfoBytes)
		{
			var msg = Parse(bytes);
			targetInfoBytes = bytes.Slice(
				msg.hdr.ntChallengeResponse.offset + 16 + NtlmClientChallenge.StructSize,
				msg.hdr.ntChallengeResponse.len - 16 - NtlmClientChallenge.StructSize - 4 /* Z4 */
				);
			return msg;
		}
		public static NtlmAuthenticate Parse(
			ReadOnlySpan<byte> bytes
			)
		{
			ByteMemoryReader reader = new ByteMemoryReader(bytes.ToArray());
			NtlmAuthenticate msg = reader.ReadAuthenticate();
			return msg;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SingleHostData
	{
		public unsafe static int StructSize = sizeof(SingleHostData);

		public uint size;
		public uint z4;
		public ulong CustomData;
		public Guid machineId1;
		public Guid machineId2;
	}
}
