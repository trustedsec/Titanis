using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

		internal static T ReadBlittableStruct<T>(this ByteMemoryReader reader, int cb)
			where T : struct
		{
			if (cb < Unsafe.SizeOf<T>())
				throw new InvalidCastException($"The buffer size is too small.  The caller passed size {cb}, but the size required for {typeof(T).Name} is {Unsafe.SizeOf<T>()}.");

			return MemoryMarshal.Read<T>(reader.Consume(cb));
		}

		internal static T ReadBlittableStruct<T>(this ByteMemoryReader reader)
			where T : struct
			{
			return MemoryMarshal.Read<T>(reader.Consume(Unsafe.SizeOf<T>()));
			}

		internal static T ReadBlittableStructAt<T>(this ByteMemoryReader reader, int position)
			where T : struct
		{
			reader.Position = position;
			return reader.ReadBlittableStruct<T>();
		}

		internal static Guid ReadChannelBinding(this ByteMemoryReader reader, int cb)
		{
			if (cb != 0x10)
				throw new FormatException(Messages.Ntlm_InvalidSingleHostData);

			return reader.ReadGuid();
		}

		internal static NtlmNegotiateMessage ReadNegotiate(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NegotiateHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmNegotiateMessage msg = new NtlmNegotiateMessage
			{
				hdr = reader.ReadBlittableStruct<NegotiateHeader>()
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

		internal static NtlmChallenge ReadChallenge(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NtlmChallengeHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmChallenge challenge = new NtlmChallenge
			{
				hdr = reader.ReadBlittableStruct<NtlmChallengeHeader>()
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
			while (!eol && reader.Position < infoEndPos)
			{
				AvHeader avh = reader.ReadBlittableStruct<AvHeader>();
				int avEndPos = reader.Position + avh.avLen;
				switch (avh.id)
				{
					case AvId.Eol:
						eol = true;
						break;
					case AvId.NbComputerName:
						// Preserve empty-valued pairs (e.g. Samba sends MsvAvDnsDomainName with len 0);
						// they must be echoed back or servers such as Samba reject the session.
						av.NbComputerName = reader.ReadStringUni(avh.avLen) ?? string.Empty;
						break;
					case AvId.NbDomainName:
						av.NbDomainName = reader.ReadStringUni(avh.avLen) ?? string.Empty;
						break;
					case AvId.DnsComputerName:
						av.DnsComputerName = reader.ReadStringUni(avh.avLen) ?? string.Empty;
						break;
					case AvId.DnsDomainName:
						av.DnsDomainName = reader.ReadStringUni(avh.avLen) ?? string.Empty;
						break;
					case AvId.DnsTreeName:
						av.DnsTreeName = reader.ReadStringUni(avh.avLen) ?? string.Empty;
						break;
					case AvId.Flags:
						av.flags = (NtlmAuthFlags)reader.ReadInt32LE();
						break;
					case AvId.Timestamp:
						// FILETIME is relative to 1601-01-01 UTC
						av.timestamp = DateTime.FromFileTimeUtc(reader.ReadInt64LE());
						break;
					case AvId.SingleHost:
						av.singleHost = reader.ReadBlittableStruct<SingleHostData>(avh.avLen);
						break;
					case AvId.TargetName:
						av.targetName = reader.ReadStringUni(avh.avLen);
						break;
					case AvId.ChannelBindings:
						av.channelBindingHashed = reader.ReadChannelBinding(avh.avLen);
						break;
					default:
						break;
				}
				// TODO: Throw FormatException if ending doesn't match
				reader.Position = avEndPos;
			}

			// TODO: Alert if !eol ?

			return av;
		}

		internal static NtlmAuthenticate ReadAuthenticate(this ByteMemoryReader reader)
		{
			if (reader.Remaining.Length < NtlmAuthenticateHeader.StructSize)
				throw new FormatException(Messages.Ntlm_InvalidMessage);

			int pos = reader.Position;

			NtlmAuthenticate c = new NtlmAuthenticate
			{
				hdr = reader.ReadBlittableStruct<NtlmAuthenticateHeader>()
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
				c.lmResponse = reader.ReadBlittableStructAt<Buffer192>(pos + c.hdr.lmChallengeResponse.offset);
			if (c.hdr.ntChallengeResponse.len > 0)
			{
				bool isNtlmV2 = (c.hdr.ntChallengeResponse.len > Buffer192.StructSize);

				c.ntResponse = new Buffer192(reader.ReadBlittableStructAt<Buffer128>(pos + c.hdr.ntChallengeResponse.offset));
				if (isNtlmV2)
				{
					c.ntResponse = new Buffer192(reader.ReadBlittableStructAt<Buffer128>(pos + c.hdr.ntChallengeResponse.offset));
					c.clientChallenge = reader.ReadBlittableStruct<NtlmClientChallenge>();
					c.avInfo = reader.ReadAvInfo(pos + c.hdr.ntChallengeResponse.len);
				}
				else
				{
					c.ntResponse = reader.ReadBlittableStructAt<Buffer192>(pos + c.hdr.ntChallengeResponse.offset);
				}
			}
			if (c.hdr.sessionKey.len > 0)
				c.sessionKey = reader.ReadBlittableStructAt<Buffer128>(pos + c.hdr.sessionKey.offset);
			c.domain = reader.ReadStringUni(pos + c.hdr.domain.offset, c.hdr.domain.len);
			c.userName = reader.ReadStringUni(pos + c.hdr.userName.offset, c.hdr.userName.len);
			c.workstation = reader.ReadStringUni(pos + c.hdr.workstation.offset, c.hdr.workstation.len);

			return c;
		}
			}
		}
