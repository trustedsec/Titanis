using System;
using System.Collections.Generic;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	static class NtlmWriter
	{
		internal static void WriteAvInfo(this ByteWriter writer, NtlmAvInfo av)
		{
			if (av.NbDomainName != null)
				writer.WriteAv(AvId.NbDomainName, av.NbDomainName);
			if (av.NbComputerName != null)
				writer.WriteAv(AvId.NbComputerName, av.NbComputerName);
			if (av.DnsDomainName != null)
				writer.WriteAv(AvId.DnsDomainName, av.DnsDomainName);
			if (av.DnsComputerName != null)
				writer.WriteAv(AvId.DnsComputerName, av.DnsComputerName);
			if (av.DnsTreeName != null)
				writer.WriteAv(AvId.DnsComputerName, av.DnsTreeName);
			if (av.timestamp.HasValue)
				writer.WriteAv(AvId.Timestamp, av.timestamp.Value);
			if (av.flags != 0)
				writer.WriteAv(AvId.Flags, av.flags);
			if (av.singleHost.HasValue)
				writer.WriteAv(AvId.SingleHost, av.singleHost.Value);
			if (av.channelBinding.HasValue)
				writer.WriteAv(AvId.ChannelBindings, av.channelBinding.Value);
			if (av.targetName != null)
				writer.WriteAv(AvId.TargetName, av.targetName);

			writer.WriteUInt16LE((ushort)AvId.Eol);
			writer.WriteUInt16LE(0);
		}

		internal static void WriteAv(this ByteWriter writer, AvId avid, string str)
		{
			writer.WriteUInt16LE((ushort)avid);
			if (!string.IsNullOrEmpty(str))
			{
				int cb = Encoding.Unicode.GetByteCount(str);
				writer.WriteUInt16LE((ushort)cb);
				writer.WriteStringUni(str);
			}
			else
			{
				writer.WriteUInt16LE(0);
			}
		}

		internal static void WriteAv(this ByteWriter writer, AvId avid, DateTime timestamp)
		{
			writer.WriteUInt16LE((ushort)avid);
			writer.WriteUInt16LE(8);
			writer.WriteInt64LE(timestamp.Ticks);
		}

		internal static void WriteAv(this ByteWriter writer, AvId avid, NtlmAuthFlags flags)
		{
			writer.WriteUInt16LE((ushort)avid);
			writer.WriteUInt16LE(4);
			writer.WriteUInt32LE((uint)flags);
		}

		internal unsafe static void WriteAv(this ByteWriter writer, AvId avid, SingleHostData data)
		{
			writer.WriteUInt16LE((ushort)avid);
			writer.WriteUInt16LE((ushort)SingleHostData.StructSize);
			fixed (byte* pBuf = writer.Consume(SingleHostData.StructSize))
			{
				*(SingleHostData*)pBuf = data;
			}
		}

		internal static void WriteAv(this ByteWriter writer, AvId avid, Guid value)
		{
			writer.WriteUInt16LE((ushort)avid);
			writer.WriteUInt16LE(16);
			writer.WriteGuid(value);
		}

		internal static void WriteChallenge(this ByteWriter writer, NtlmChallenge challenge)
		{
			writer.WriteChallengeHeader(challenge.hdr);
			if (!string.IsNullOrEmpty(challenge.serverName))
				writer.WriteStringUni(challenge.serverName);
			if (challenge.targetInfo != null)
				writer.WriteAvInfo(challenge.targetInfo);
		}


		internal unsafe static void WriteChallengeHeader(this ByteWriter writer, in NtlmChallengeHeader hdr)
		{
			fixed (byte* pBuf = writer.Consume(NtlmChallengeHeader.StructSize))
			{
				*(NtlmChallengeHeader*)pBuf = hdr;
			}
		}
	}
}
