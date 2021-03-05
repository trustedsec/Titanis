using System;
using System.Collections.Generic;
using System.Text;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{
	static class NtlmReader
	{
		internal static string? ReadStringUni(this ByteMemoryReader reader, int length)
		{
			if (length > 0)
			{
				var buf = reader.Consume(length);
				string str = Encoding.Unicode.GetString(buf);
				return str;
			}
			else
			{
				return null;
			}
		}

		internal static string? ReadStringUni(this ByteMemoryReader reader, int position, int length)
		{
			reader.Position = position;
			return reader.ReadStringUni(length);
		}

		internal unsafe static SingleHostData ReadSingleHostData(this ByteMemoryReader reader, int cb)
		{
			if (cb != SingleHostData.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidSingleHostData);

			fixed (byte* pBuf = reader.Consume(SingleHostData.StructSize))
			{
				return *(SingleHostData*)pBuf;
			}
		}

		internal unsafe static Guid ReadChannelBinding(this ByteMemoryReader reader, int cb)
		{
			if (cb != 0x10)
				throw new FormatException(Messages.Ntlm_InvalidSingleHostData);

			return reader.ReadGuid();
		}

		internal unsafe static NtlmChallengeHeader ReadChallengeHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(NtlmChallengeHeader.StructSize))
			{
				return *(NtlmChallengeHeader*)pBuf;
			}
		}

		internal unsafe static AvHeader ReadAvHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(AvHeader.StructSize))
			{
				return *(AvHeader*)pBuf;
			}
		}


		internal static NtlmNegotiateMessage ReadNegotiate(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NegotiateHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmNegotiateMessage msg = new NtlmNegotiateMessage
			{
				hdr = reader.ReadNegotiateHeader()
			};

			bool isValid =
				(msg.hdr.signature == NegotiateHeader.ValidSignature)
				&& (msg.hdr.messageType == NtlmMessageType.Negotiate)
				;
			if (!isValid)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			msg.workstationDomain = reader.ReadStringUni(pos + msg.hdr.domain.offset, msg.hdr.domain.len);
			msg.workstationName = reader.ReadStringUni(pos + msg.hdr.workstation.offset, msg.hdr.workstation.len);

			return msg;
		}

		internal static unsafe NegotiateHeader ReadNegotiateHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(NegotiateHeader.StructSize))
			{
				return *(NegotiateHeader*)pBuf;
			}
		}

		internal static NtlmChallenge ReadChallenge(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NtlmChallengeHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmChallenge challenge = new NtlmChallenge
			{
				hdr = reader.ReadChallengeHeader()
			};
			bool isValid =
				(challenge.hdr.signature == NegotiateHeader.ValidSignature)
				&& (challenge.hdr.messageType == NtlmMessageType.Challenge)
				;
			if (!isValid)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			if (0 != (challenge.hdr.negotiateFlags & NegotiateFlags.S_NegotiateTargetInfo))
			{
				int infoStartPos = pos + challenge.hdr.targetInfo.offset;
				reader.Position = infoStartPos;
				int infoEndPos = infoStartPos + challenge.hdr.targetInfo.len;

				challenge.targetInfo = reader.ReadAvInfo(infoEndPos);
			}
			return challenge;
		}

		private static NtlmAvInfo ReadAvInfo(this ByteMemoryReader reader, int infoEndPos)
		{
			NtlmAvInfo av = new NtlmAvInfo();

			bool eol = false;
			while (!eol || reader.Position < infoEndPos)
			{
				AvHeader avh = reader.ReadAvHeader();
				int avEndPos = reader.Position + avh.avLen;
				switch (avh.id)
				{
					case AvId.Eol:
						eol = true;
						break;
					case AvId.NbComputerName:
						av.NbComputerName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.NbDomainName:
						av.NbDomainName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.DnsComputerName:
						av.DnsComputerName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.DnsDomainName:
						av.DnsDomainName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.DnsTreeName:
						av.DnsTreeName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.Flags:
						av.flags = (NtlmAuthFlags)reader.ReadInt32LE();
						break;
					case AvId.Timestamp:
						av.timestamp = new DateTime(reader.ReadInt64LE());
						break;
					case AvId.SingleHost:
						av.singleHost = reader.ReadSingleHostData(avh.avLen);
						break;
					case AvId.TargetName:
						av.targetName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.ChannelBindings:
						av.channelBinding = reader.ReadChannelBinding(avh.avLen);
						break;
					default:
						break;
				}
				// TODO: Throw FormatException if ending doesn't match
				reader.Position = avEndPos;
			}

			return av;
		}

		internal static NtlmAuthenticate ReadAuthenticate(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NtlmAuthenticateHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmAuthenticate c = new NtlmAuthenticate
			{
				hdr = reader.ReadAuthenticateHeader()
			};
			bool isValid =
				(c.hdr.signature == NegotiateHeader.ValidSignature)
				&& (c.hdr.messageType == NtlmMessageType.Authenticate)
				&& (c.hdr.lmChallengeResponse.len == 0 || c.hdr.lmChallengeResponse.len == Buffer192.StructSize)
				&& (c.hdr.sessionKey.len == 0 || c.hdr.sessionKey.len == Buffer128.StructSize)
				&& (c.hdr.ntChallengeResponse.len == 0 || c.hdr.ntChallengeResponse.len >= Buffer192.StructSize)
				;
			if (!isValid)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			if (c.hdr.lmChallengeResponse.len > 0)
				c.lmResponse = reader.ReadBuffer192(pos + c.hdr.lmChallengeResponse.offset);
			if (c.hdr.ntChallengeResponse.len > 0)
			{
				bool isNtlmV2 = (c.hdr.ntChallengeResponse.len > Buffer192.StructSize);

				c.ntResponse = new Buffer192(reader.ReadBuffer128(pos + c.hdr.ntChallengeResponse.offset));
				if (isNtlmV2)
				{
					c.ntResponse = new Buffer192(reader.ReadBuffer128(pos + c.hdr.ntChallengeResponse.offset));
					c.clientChallenge = reader.ReadClientChallenge();
					c.avInfo = reader.ReadAvInfo(pos + c.hdr.ntChallengeResponse.len);
				}
				else
				{
					c.ntResponse = reader.ReadBuffer192(pos + c.hdr.ntChallengeResponse.offset);
				}
			}
			if (c.hdr.sessionKey.len > 0)
				c.sessionKey = reader.ReadBuffer128(pos + c.hdr.sessionKey.offset);
			c.domain = reader.ReadStringUni(pos + c.hdr.domain.offset, c.hdr.domain.len);
			c.userName = reader.ReadStringUni(pos + c.hdr.userName.offset, c.hdr.userName.len);
			c.workstation = reader.ReadStringUni(pos + c.hdr.workstation.offset, c.hdr.workstation.len);

			return c;
		}

		internal static unsafe NtlmClientChallenge ReadClientChallenge(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(NtlmClientChallenge.StructSize))
			{
				return *(NtlmClientChallenge*)pBuf;
			}
		}

		internal static unsafe Buffer128 ReadBuffer128(this ByteMemoryReader reader, int pos)
		{
			reader.Position = pos;
			fixed (byte* pBuf = reader.Consume(Buffer128.StructSize))
			{
				return *(Buffer128*)pBuf;
			}
		}

		internal static unsafe Buffer192 ReadBuffer192(this ByteMemoryReader reader, int pos)
		{
			reader.Position = pos;
			fixed (byte* pBuf = reader.Consume(Buffer192.StructSize))
			{
				return *(Buffer192*)pBuf;
			}
		}

		internal static unsafe NtlmAuthenticateHeader ReadAuthenticateHeader(this ByteMemoryReader reader)
		{
			fixed (byte* pBuf = reader.Consume(NtlmAuthenticateHeader.StructSize))
			{
				return *(NtlmAuthenticateHeader*)pBuf;
			}
		}
	}
}
