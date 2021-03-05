using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Titanis.Crypto;
using Titanis.IO;

namespace Titanis.Security.Ntlm
{

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Buffer128 : IEquatable<Buffer128>
	{
		public static unsafe int StructSize => sizeof(Buffer128);

		public Buffer128(ReadOnlySpan<byte> bytes)
			: this()
		{
			bytes.CopyTo(this.AsSpan());
		}
		public Buffer128(in Buffer192 source)
		{
			this.k1 = source.k1;
			this.k2 = source.k2;
		}

		internal ulong k1;
		internal ulong k2;

		public bool IsEmpty => (this.k1 == 0 && this.k2 == 0);

		public readonly unsafe ReadOnlySpan<byte> AsReadOnlySpan()
		{
			fixed (ulong* pBuf = &this.k1)
			{
				return new ReadOnlySpan<byte>((byte*)pBuf, StructSize);
			}
		}
		public unsafe Span<byte> AsSpan()
		{
			fixed (ulong* pBuf = &this.k1)
			{
				return new Span<byte>((byte*)pBuf, StructSize);
			}
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is Buffer128 buffer && this.Equals(buffer);
		}

		public readonly bool Equals(Buffer128 other)
		{
			return this.k1 == other.k1 &&
				   this.k2 == other.k2;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			int hashCode = -2014564367;
			hashCode = hashCode * -1521134295 + this.k1.GetHashCode();
			hashCode = hashCode * -1521134295 + this.k2.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(Buffer128 left, Buffer128 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Buffer128 left, Buffer128 right)
		{
			return !(left == right);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct Buffer192 : IEquatable<Buffer192>
	{
		internal static unsafe int StructSize => sizeof(Buffer192);

		internal static Buffer192 EmptyLMResponse => new Buffer192();

		public Buffer192(ReadOnlySpan<byte> bytes)
			: this()
		{
			bytes.CopyTo(this.AsSpan());
		}
		public Buffer192(Buffer128 buf)
		{
			this.k1 = buf.k1;
			this.k2 = buf.k2;
			this.k3 = 0;
		}

		internal ulong k1;
		internal ulong k2;
		internal ulong k3;

		internal bool IsEmpty => (this.k1 == 0 && this.k2 == 0 && this.k3 == 0);

		public int Length => (this.IsEmpty) ? 0 : StructSize;

		internal unsafe Span<byte> AsSpan()
		{
			fixed (ulong* pBuf = &this.k1)
			{
				return new Span<byte>((byte*)pBuf, StructSize);
			}
		}

		public override bool Equals(object obj)
		{
			return obj is Buffer192 buffer && this.Equals(buffer);
		}

		public bool Equals(Buffer192 other)
		{
			return this.k1 == other.k1 &&
				   this.k2 == other.k2 &&
				   this.k3 == other.k3
				   ;
		}

		public override int GetHashCode()
		{
			int hashCode = 186409623;
			hashCode = hashCode * -1521134295 + this.k1.GetHashCode();
			hashCode = hashCode * -1521134295 + this.k2.GetHashCode();
			hashCode = hashCode * -1521134295 + this.k3.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(Buffer192 left, Buffer192 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Buffer192 left, Buffer192 right)
		{
			return !(left == right);
		}
	}

	//[StructLayout(LayoutKind.Sequential, Pack = 1)]
	//struct DeslResult
	//{
	//	internal static unsafe int StructSize => sizeof(DeslResult);

	//	internal ulong c1;
	//	internal ulong c2;
	//	internal ulong c3;

	//	internal unsafe Span<byte> AsSpan()
	//	{
	//		fixed (ulong* pBuf = &this.c1)
	//		{
	//			return new Span<byte>((byte*)pBuf, StructSize);
	//		}
	//	}
	//}

	static class Ntlm
	{
		#region Validation

		internal static Exception MakeBadMacSizeException(string paramName)
		{
			return new ArgumentException("The buffer provided for the message authentication code does not match the size required by the security provider.", paramName);
		}
		#endregion

		#region ComputeMd4
		internal static Buffer128 ComputeMd4(in Buffer128 message)
		{
			return ComputeMd4(message.AsSpan());
		}

		internal static Buffer128 ComputeMd4(ReadOnlySpan<byte> message)
		{
			Buffer128 digestBuffer = new Buffer128();
			SlimHashAlgorithm.ComputeHash<Md4Context>(
				message,
				digestBuffer.AsSpan());
			return digestBuffer;
		}
		#endregion

		#region ComputeMd5
		internal static Buffer128 ComputeMd5(in Buffer128 message)
		{
			Buffer128 digestBuffer = new Buffer128();
			SlimHashAlgorithm.ComputeHash<Md5Context>(
				message.AsSpan(),
				digestBuffer.AsSpan());
			return digestBuffer;
		}
		internal static void ComputeMd5(ReadOnlySpan<byte> message, Span<byte> digestBuffer)
		{
			SlimHashAlgorithm.ComputeHash<Md5Context>(message, digestBuffer);
		}
		#endregion


		#region ComputeHmacMd5
		/// <remarks>
		/// Simple overload for computing HMAC MD5 on a message.
		/// </remarks>
		internal static Buffer128 ComputeHmacMd5(
			in Buffer128 key,
			ReadOnlySpan<byte> message)
		{
			Buffer128 digestBuffer = new Buffer128();
			SlimHashAlgorithm.ComputeHmac<Md5Context>(key.AsSpan(), message, digestBuffer.AsSpan());
			return digestBuffer;
		}
		/// <remarks>
		/// Used for computing the NTLM MIC during authentication.
		/// </remarks>
		internal static unsafe Buffer128 ComputeHmacMd5(
			in Buffer128 key,
			in SecBufferList buffers
			)
		{
			HmacContext<Md5Context> ctx = new HmacContext<Md5Context>(key.AsSpan());
			ctx.Initialize();

			for (int i = 0; i < buffers.BufferCount; i++)
			{
				ctx.HashData(buffers.GetBuffer(i).ReadOnlySpan);
			}

			Buffer128 digestBuffer = new Buffer128();
			ctx.HashFinal(digestBuffer.AsSpan());
			return digestBuffer;
		}
		internal static Buffer128 ComputeHmacMd5(
			in Buffer128 key,
			uint seqnbr,
			in SecBufferList bufferList
			)
		{
			HmacContext<Md5Context> ctx = new HmacContext<Md5Context>(key.AsSpan());
			ctx.Initialize();

			Span<byte> seqNbrBytes = stackalloc byte[4];
			BinaryPrimitives.WriteUInt32LittleEndian(seqNbrBytes, seqnbr);
			ctx.HashData(seqNbrBytes);
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				ctx.HashData(bufferList.GetBuffer(i).ReadOnlySpan);
			}

			Buffer128 digestBuffer = new Buffer128();
			ctx.HashFinal(digestBuffer.AsSpan());
			return digestBuffer;
		}
		internal static void ComputeHmacMd5(
			in Buffer128 key,
			ReadOnlySpan<byte> message,
			Span<byte> digestBuffer)
		{
			SlimHashAlgorithm.ComputeHmac<Md5Context>(key.AsSpan(), message, digestBuffer);
		}
		#endregion

		#region NTLMv1
		internal static Buffer128 NtowfV1(
			string password
			)
		{
			return ComputeMd4(Encoding.Unicode.GetBytes(password));
		}

		public static Buffer128 LmowfV1(string password)
		{
			var key = DeriveLMKey(password);

			//const string Plaintext = "KGS!@#$%";
			const ulong Plaintext = 0x252423402153474B;
			Buffer128 lmowf = new Buffer128
			{
				k1 = Des.Encrypt(key.k1, Plaintext),
				k2 = Des.Encrypt(key.k2, Plaintext)
			};
			return lmowf;
		}

		const ulong Mask56 = 0x00FFFFFFFFFFFFFF;
		public static unsafe Buffer128 DeriveLMKey(string password)
		{
			int cch = Math.Min(14, password.Length);

			Buffer128 key = new Buffer128();
			Span<byte> keyBytes = key.AsSpan();

			password = password.ToUpper();
			Encoding.UTF8.GetBytes(password.AsSpan(0, cch), keyBytes);
			key.k2 <<= 8;
			key.k2 |= (key.k1 >> 56);
			key.k1 &= Mask56;

			key.k1 = Des.ExpandKey(key.k1);
			key.k2 = Des.ExpandKey(key.k2);

			return key;
		}

		internal static Buffer192 DeslEncrypt(in Buffer128 key, ulong data)
		{
			/*
			ConcatenationOf(
				DES(K[0..6], D),
				DES(K[7..13], D),
				DES(ConcatenationOf(K[14..15], Z(5)), D)
			);
			 */

			ulong k1 = Des.ExpandKey(key.k1);
			ulong k2 = Des.ExpandKey((key.k2 << 8) | (key.k1 >> 56));
			ulong k3 = Des.ExpandKey((key.k2 >> 48));

			return new Buffer192
			{
				k1 = Des.Encrypt(k1, data),
				k2 = Des.Encrypt(k2, data),
				k3 = Des.Encrypt(k3, data),
			};
		}

		private static void HashLMKey(ref Md5Context ctx, in Buffer128 lmkey)
		{
			ctx.HashData(lmkey.AsReadOnlySpan());
		}

		internal static NtlmResponse ComputeResponseV1(
			NegotiateFlags negFlags,
			NtlmCredential credential,
			ulong clientChallenge,
			ulong serverChallenge
			)
		{
			bool extendedSecurity = (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity));

			var responseKeys = new NtlmResponseKeys
			{
				ResponseKeyLM = (credential.CanProvideResponseKeyLM
					? credential.GetResponseKeyLMv1()
					: new Buffer128()
					),
				ResponseKeyNT = (credential.CanProvideResponseKeyNT
					? credential.GetResponseKeyNTv1()
					: new Buffer128()
					)
			};
			var sessionBaseKey = credential.CanProvideResponseKeyNT
				? ComputeMd4(responseKeys.ResponseKeyNT)
				: new Buffer128()
				;

			if (extendedSecurity)
			{
				Debug.Assert(credential.CanProvideResponseKeyNT);

				var ntChallengeResponse = DeslEncrypt(
					responseKeys.ResponseKeyNT,
					ComputeMd5(
						new Buffer128
						{
							k1 = serverChallenge,
							k2 = clientChallenge
						}).k1
					);
				Buffer192 lmChallengeResponse = new Buffer192();
				(new Buffer128 { k1 = clientChallenge }).AsSpan().CopyTo(lmChallengeResponse.AsSpan());

				return new NtlmResponse
				{
					keys = responseKeys,
					LmChallengeResponse = lmChallengeResponse,
					NtChallengeResponse = ntChallengeResponse.AsSpan().ToArray(),
					SessionBaseKey = sessionBaseKey
				};
			}
			else
			{
				var resp = new NtlmResponse
				{
					keys = responseKeys,
					LmChallengeResponse = (credential.CanProvideResponseKeyLM
						? DeslEncrypt(
							responseKeys.ResponseKeyLM,
							serverChallenge
							)
						: new Buffer192()
						),
					NtChallengeResponse = (credential.CanProvideResponseKeyNT
						? DeslEncrypt(
							responseKeys.ResponseKeyNT,
							serverChallenge
							).AsSpan().ToArray()
						: null
						),
					SessionBaseKey = sessionBaseKey
				};

				return resp;
			}
			/*
			If (NTLMSSP_NEGOTIATE_EXTENDED_SESSIONSECURITY flag is set in NegFlg) 

			 Set NtChallengeResponse to DESL(
				ResponseKeyNT, 
				MD5(
					ConcatenationOf(
						CHALLENGE_MESSAGE.ServerChallenge, 
						ClientChallenge
						)
					)[0..7]
				)
			 Set LmChallengeResponse to ConcatenationOf{
				ClientChallenge, 
				Z(16)
				}

			 Else 

			 Set NtChallengeResponse to DESL(ResponseKeyNT, 
			 CHALLENGE_MESSAGE.ServerChallenge)
			
			If (NoLMResponseNTLMv1 is TRUE)

			 Set LmChallengeResponse to NtChallengeResponse

			 Else 

			 Set LmChallengeResponse to DESL(ResponseKeyLM, 
			 CHALLENGE_MESSAGE.ServerChallenge)

			 EndIf

			 EndIf

			EndIf

			Set SessionBaseKey to MD4(NTOWF)
			 */
		}
		#endregion

		internal static Buffer128 NtowfV2(
			string password,
			string userName,
			string? domain
			)
		{
			var ntlmHash = ComputeMd4(Encoding.Unicode.GetBytes(password));
			return NtowfV2(userName, domain, ntlmHash);
		}

		internal static Buffer128 NtowfV2(string userName, string? domain, in Buffer128 ntlmHash)
		{
			return ComputeHmacMd5(
				ntlmHash,
				Encoding.Unicode.GetBytes(userName.ToUpper() + domain)
				);
		}

		internal static Buffer128 ComputeSessionBaseKeyV2(
			in Buffer128 responseKeyNT,
			in Buffer128 ntProofStr
			)
		{
			return ComputeHmacMd5(responseKeyNT, ntProofStr.AsSpan());
		}

		internal static unsafe void BuildTempV2(
			ByteWriter tempWriter,
			DateTime time,
			ulong clientChallenge,
			ReadOnlySpan<byte> targetInfo
			)
		{
			const int Responserversion = 1;
			const int HiResponserversion = 1;

			fixed (byte* pTemp = tempWriter.Consume(NtlmClientChallenge.StructSize))
			{
				NtlmClientChallenge* pTempBuf = (NtlmClientChallenge*)pTemp;
				*(NtlmClientChallenge*)pTemp = new NtlmClientChallenge
				{
					Responserversion = Responserversion,
					HiResponserversion = HiResponserversion,
					time = time.Ticks,
					clientChallenge = clientChallenge,
				};
			}

			tempWriter.WriteBytes(targetInfo);
			tempWriter.Advance(4);    // Add Z(4)
		}

		internal static unsafe NtlmResponse ComputeResponseV2(
			ref NtlmAuthContextState state,
			in Buffer128 responseKeyNT,
			ReadOnlySpan<byte> targetInfo
			)
		{
			const int Md5HashSize = 128 / 8;
			int PrehashStartIndex = Md5HashSize - 8;

			var tempWriter = new ByteWriter(Md5HashSize + (NtlmClientChallenge.StructSize + 4) + 0x100);
			tempWriter.Advance(PrehashStartIndex);
			tempWriter.WriteUInt64LE(state.serverChallenge);

			Ntlm.BuildTempV2(
				tempWriter,
				state.clientTime,
				state.challengeFromClient,
				targetInfo);

			byte[] tempBuf = tempWriter.GetBuffer();
			fixed (byte* pTemp = tempBuf)
			{
				ref Buffer128 ntProofStr = ref *(Buffer128*)pTemp;
				ntProofStr = ComputeHmacMd5(
					responseKeyNT,
					tempBuf.SliceReadOnly(PrehashStartIndex, tempWriter.Length - PrehashStartIndex)
					);

				return new NtlmResponse
				{
					NTProofStr = ntProofStr,
					NtChallengeResponse = tempWriter.GetData(),
					// responseKeyLM == responseKeyNT
					LmChallengeResponse = Ntlm.ComputeLMChallengeResponseV2(
						responseKeyNT,
						state.serverChallenge,
						state.challengeFromClient),
					SessionBaseKey = Ntlm.ComputeSessionBaseKeyV2(responseKeyNT, ntProofStr)
				};
			}
		}

		internal static unsafe Buffer192 ComputeLMChallengeResponseV2(
			in Buffer128 responseKeyLM,
			ulong serverChallenge,
			ulong clientChallenge)
		{
			var input = new Buffer128 { k1 = serverChallenge, k2 = clientChallenge };
			Buffer128 digest = ComputeHmacMd5(
				responseKeyLM,
				input.AsSpan()
				);
			Buffer192 buf = new Buffer192()
			{
				k1 = digest.k1,
				k2 = digest.k2,
				k3 = clientChallenge
			};
			return buf;
		}

		internal static Buffer128 KxkeyV1(
			NegotiateFlags negFlags,
			in NtlmResponse response,
			ulong serverChallenge
			)
		{
			/*
Define KXKEY(SessionBaseKey, LmChallengeResponse, ServerChallenge) as
If ( NTLMSSP_NEGOTIATE_LMKEY flag is set in NegFlg) 
 Set KeyExchangeKey to ConcatenationOf(DES(LMOWF[0..6],
 LmChallengeResponse[0..7]), 
 DES(ConcatenationOf(LMOWF[7], 0xBDBDBDBDBDBD), 
 LmChallengeResponse[0..7])) 
Else
 If ( NTLMSSP_REQUEST_NON_NT_SESSION_KEY flag is set in NegFlg) 
 Set KeyExchangeKey to ConcatenationOf(LMOWF[0..7], Z(8)), 
 Else
 Set KeyExchangeKey to SessionBaseKey
 Endif
Endif
EndDefine
			 */

			ref readonly Buffer128 sessionBaseKey = ref response.SessionBaseKey;

			if (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity))
			{
				Debug.Assert(!sessionBaseKey.IsEmpty);

				/*
Set KeyExchangeKey to HMAC_MD5(SessionBaseKey, ConcatenationOf(ServerChallenge, 
LmChallengeResponse [0..7]))
				 */
				return ComputeHmacMd5(
					sessionBaseKey,
					new Buffer128
					{
						k1 = serverChallenge,
						k2 = response.LmChallengeResponse.k1
					}.AsSpan());
			}
			else if (0 != (negFlags & NegotiateFlags.G_NegotiateLMKey))
			{
				ulong k1 = Des.ExpandKey(response.keys.ResponseKeyLM.k1);
				ulong p1 = response.LmChallengeResponse.k1;
				ulong k2 = Des.ExpandKey((response.keys.ResponseKeyLM.k1 >> 56) | (0xBDBDBDBDBDBD << 8));
				Buffer128 kxkey = new Buffer128
				{
					k1 = Des.Encrypt(k1, p1),
					k2 = Des.Encrypt(k2, p1)
				};
				return kxkey;
			}
			else if (0 != (negFlags & NegotiateFlags.R_RequestNonNTSessionKey))
			{
				return new Buffer128
				{
					k1 = response.keys.ResponseKeyLM.k1,
					k2 = 0
				};
			}
			else
			{
				return sessionBaseKey;
			}
		}

		internal static Buffer128 KxkeyV2(Buffer128 sessionBaseKey)
		{
			return sessionBaseKey;
		}

		#region Crc32
		internal static uint Crc32(
			in SecBufferList buffers
			)
		{
			uint r = 0xFFFFFFFF;
			for (int i = 0; i < buffers.BufferCount; i++)
			{
				var buffer = buffers.GetBuffer(i);
				if (buffer.ShouldSign)
					r = Crc32(buffers.GetBuffer(i).ReadOnlySpan, r);
			}

			return ~r;
		}

		const uint Poly = 0xEDB88320;
		private static uint Crc32(ReadOnlySpan<byte> message, uint r)
		{
			for (int i = 0; i < message.Length; i++)
			{
				r ^= message[i];
				for (int j = 0; j < 8; j++)
				{
					uint t = ~((r & 1) - 1);
					r = (r >> 1) ^ (Poly & t);
				}
			}

			return r;
		}
		#endregion

		public const int SessionKeySize = 16;
		private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

		internal static void GetRandomData(Span<byte> buffer)
		{
			rng.GetBytes(buffer);
		}

		internal static Buffer128 GetExportedSessionKey(
			NegotiateFlags negotiateFlags,
			in Buffer128 kxkey,
			in Buffer128 randomKey)
		{
			Buffer128 exportedSessionKey;
			if (0 != (negotiateFlags & NegotiateFlags.V_NegotiateKeyExchange))
			{
				exportedSessionKey = randomKey;
			}
			else
			{
				exportedSessionKey = kxkey;
			}

			return exportedSessionKey;
		}

		internal unsafe static ulong GenerateChallenge()
		{
			ulong challenge;
			byte* pChallenge = (byte*)&challenge;
			GetRandomData(new Span<byte>(pChallenge, 8));
			return challenge;
		}

		internal static void SealV1(
			in MessageSealParams sealParams,
			ref Rc4Context sealingKey,
			uint seqNbr,
			uint randomPad
			)
		{
			ref readonly var bufferList = ref sealParams.bufferList;
			var checksum = Crc32(in bufferList);

			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				TransformIfPrivacy(ref sealingKey, bufferList.GetBuffer(i));
			}

			ref NtlmMessageSignatureV1 signature = ref MemoryMarshal.AsRef<NtlmMessageSignatureV1>(sealParams.Trailer);
			MacV1(
				checksum,
				seqNbr,
				randomPad,
				ref sealingKey,
				out signature);
		}

		private static void TransformIfPrivacy(ref Rc4Context sealingKey, in SecBuffer buf)
		{
			if (buf.ShouldEncrypt)
				sealingKey.Transform(buf.Span, buf.Span);
		}

		internal static unsafe void UnsealV1(
			in MessageSealParams unsealParams,
			ref Rc4Context sealingKey,
			uint seqNbr
			)
		{
			ref readonly var bufferList = ref unsealParams.bufferList;
			for (int i = 0; i < bufferList.BufferCount; i++)
			{
				var buf = bufferList.GetBuffer(i);
				TransformIfPrivacy(ref sealingKey, ref buf);
			}

			var checksum = Crc32(in bufferList);

			MacV1(
				checksum,
				seqNbr,
				0, // randomPad
				ref sealingKey,
				out NtlmMessageSignatureV1 expectedSignature);

			ref NtlmMessageSignatureV1 signature = ref MemoryMarshal.AsRef<NtlmMessageSignatureV1>(unsealParams.Trailer);
			if (
				(signature.checksum != expectedSignature.checksum)
				|| (signature.seqnbr != expectedSignature.seqnbr)
				|| (signature.version != 1)
				)
				throw new SecurityException(Messages.Ntlm_BadMessageSignature);
		}

		internal static unsafe void SealV2(
			in MessageSealParams sealParams,
			ref Rc4Context sealingKey,
			in Buffer128 signingKey,
			uint seqNbr,
			bool useKxkey
			)
		{
			{
				// TODO: Does the MAC have to be calculated twice?

				ref NtlmMessageSignatureV2 signature = ref MemoryMarshal.AsRef<NtlmMessageSignatureV2>(sealParams.Trailer);
				MacV2(
					in sealParams.bufferList,
					signingKey,
					ref sealingKey,
					// Defer encryption until after sealed message, so keystream is in sync
					false,
					seqNbr,
					out signature);
			}

			for (int i = 0; i < sealParams.bufferList.BufferCount; i++)
			{
				TransformIfPrivacy(ref sealingKey, sealParams.bufferList.GetBuffer(i));
			}

			if (useKxkey)
			{
				ref NtlmMessageSignatureV2 signature = ref MemoryMarshal.AsRef<NtlmMessageSignatureV2>(sealParams.Trailer);
				var checksumSpan = signature.ChecksumAsSpan();
				sealingKey.Transform(checksumSpan, checksumSpan);
			}
		}

		internal static unsafe void UnsealV2(
			in MessageSealParams unsealParams,
			ref Rc4Context sealingKey,
			in Buffer128 signingKey,
			uint seqNbr,
			bool useKxkey
			)
		{
			for (int i = 0; i < unsealParams.bufferList.BufferCount; i++)
			{
				TransformIfPrivacy(ref sealingKey, unsealParams.bufferList.GetBuffer(i));
			}

			MacV2(
				in unsealParams.bufferList,
				signingKey,
				ref sealingKey,
				useKxkey,
				seqNbr,
				out NtlmMessageSignatureV2 expectedSignature);

			{
				ref NtlmMessageSignatureV2 signature = ref MemoryMarshal.AsRef<NtlmMessageSignatureV2>(unsealParams.Trailer);
				if (
					(signature.checksum != expectedSignature.checksum)
					|| (signature.seqnbr != seqNbr)
					|| (signature.version != 1)
					)
					throw new SecurityException(Messages.Ntlm_BadMessageSignature);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		struct NtlmMacRandom
		{
			internal unsafe static int StructSize => sizeof(NtlmMacRandom);

			internal uint n1;
			internal uint n2;
			internal uint n3;

			internal unsafe Span<byte> AsSpan()
			{
				fixed (uint* pStruc = &this.n1)
				{
					return new Span<byte>((byte*)pStruc, StructSize);
				}
			}
		}


		internal static void MacV1(
			in SecBufferList buffers,
			ref Rc4Context sealKey,
			uint randomPad,
			uint seqNbr,
			out NtlmMessageSignatureV1 signature
		)
		{
			/*
	Define MAC(Handle, SigningKey, SeqNum, Message) as
	Set NTLMSSP_MESSAGE_SIGNATURE.Version to 0x00000001
	Set NTLMSSP_MESSAGE_SIGNATURE.Checksum to CRC32(Message)
	Set NTLMSSP_MESSAGE_SIGNATURE.RandomPad RC4(Handle, RandomPad)
	Set NTLMSSP_MESSAGE_SIGNATURE.Checksum to RC4(Handle,
	NTLMSSP_MESSAGE_SIGNATURE.Checksum)
	Set NTLMSSP_MESSAGE_SIGNATURE.SeqNum to RC4(Handle, 0x00000000)
	If (connection oriented)
	Set NTLMSSP_MESSAGE_SIGNATURE.SeqNum to
	NTLMSSP_MESSAGE_SIGNATURE.SeqNum XOR SeqNum
	Set SeqNum to SeqNum + 1
	Else
	Set NTLMSSP_MESSAGE_SIGNATURE.SeqNum to
	NTLMSSP_MESSAGE_SIGNATURE.SeqNum XOR
	(application supplied SeqNum)
	Endif
	Set NTLMSSP_MESSAGE_SIGNATURE.RandomPad to 0
	EndDefine
			*/

			var checksum = Crc32(in buffers);
			MacV1(checksum, seqNbr, randomPad, ref sealKey, out signature);
		}

		private static void MacV1(
			uint checksum,
			uint seqNbr,
			uint randomPad,
			ref Rc4Context sealKey,
			out NtlmMessageSignatureV1 signature)
		{
			NtlmMacRandom randomKey = new NtlmMacRandom();
			sealKey.Transform(randomKey.AsSpan(), randomKey.AsSpan());

			signature = new NtlmMessageSignatureV1
			{
				version = 1,
				randomPad = randomPad ^ randomKey.n1,
				checksum = checksum ^ randomKey.n2,
				seqnbr = seqNbr ^ randomKey.n3
			};
		}

		#region MacV2
		internal static void MacV2(
			in SecBufferList buffers,
			in Buffer128 signingKey,
			ref Rc4Context sealingKey,
			bool useKxkey,
			uint seqNbr,
			out NtlmMessageSignatureV2 signature
			)
		{
			/*
	Set NTLMSSP_MESSAGE_SIGNATURE.Version to 0x00000001
	 Set NTLMSSP_MESSAGE_SIGNATURE.Checksum to 
	 HMAC_MD5(SigningKey,
	 ConcatenationOf(SeqNum, Message))[0..7]
	 Set NTLMSSP_MESSAGE_SIGNATURE.SeqNum to SeqNum
	 Set SeqNum to SeqNum + 1
			 */

			signature = new NtlmMessageSignatureV2
			{
				version = 1,
				checksum = ComputeHmacMd5(signingKey, seqNbr, in buffers).k1,
				seqnbr = seqNbr
			};
			if (useKxkey)
			{
				var checksumSpan = signature.ChecksumAsSpan();
				sealingKey.Transform(checksumSpan, checksumSpan);
			}
		}
		#endregion


		private static readonly byte[] SignKeyC2SMagic = Encoding.UTF8.GetBytes("session key to client-to-server signing key magic constant\0");
		private static readonly byte[] SignKeyS2CMagic = Encoding.UTF8.GetBytes("session key to server-to-client signing key magic constant\0");
		private static readonly byte[] SealKeyC2SMagic = Encoding.UTF8.GetBytes("session key to client-to-server sealing key magic constant\0");
		private static readonly byte[] SealKeyS2CMagic = Encoding.UTF8.GetBytes("session key to server-to-client sealing key magic constant\0");

		internal static void ComputeKey(
			Span<byte> keymat,
			ReadOnlySpan<byte> salt,
			ref Buffer128 keybuf
			)
		{
			ComputeKey(keymat, Ntlm.SessionKeySize, salt, ref keybuf);
		}

		internal static void ComputeKey(
			Span<byte> keymat,
			int keySize,
			ReadOnlySpan<byte> salt,
			ref Buffer128 keybuf
			)
		{
			salt.CopyTo(keymat.Slice(keySize));
			Ntlm.ComputeMd5(keymat.Slice(0, keySize + salt.Length), keybuf.AsSpan());
		}

		internal static void DeriveKeys(ref NtlmAuthResult authResult)
		{
			Span<byte> keymat = stackalloc byte[Ntlm.SessionKeySize + SignKeyC2SMagic.Length];

			if (0 != (authResult.negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity))
			{
				Span<byte> sealkey;

				if (0 != (authResult.negFlags & NegotiateFlags.U_Negotiate128))
					sealkey = authResult.exportedSessionKey.AsSpan();
				else if (0 != (authResult.negFlags & NegotiateFlags.W_Negotiate56))
					sealkey = authResult.exportedSessionKey.AsSpan().Slice(0, 7);
				else
					sealkey = authResult.exportedSessionKey.AsSpan().Slice(0, 5);

				sealkey.CopyTo(keymat);

				ComputeKey(keymat, sealkey.Length, SealKeyC2SMagic, ref authResult.sealKeyC2S);
				ComputeKey(keymat, sealkey.Length, SealKeyS2CMagic, ref authResult.sealKeyS2C);
			}
			else if (0 != (authResult.negFlags & (NegotiateFlags.G_NegotiateLMKey | NegotiateFlags.F_NegotiateDatagram)))
			// TODO: There should also be a Windows version check here
			{
				/*
				If (NTLMSSP_NEGOTIATE_56 flag is set in NegFlg)
				 Set SealKey to ConcatenationOf(ExportedSessionKey[0..6], 0xA0)
				 Else
				 Set SealKey to ConcatenationOf(ExportedSessionKey[0..4], 0xE5,
				 0x38, 0xB0)
				 EndIf
				*/

				authResult.shortSealKey = true;
				Buffer128 sealKey = new Buffer128
				{
					k1 = authResult.exportedSessionKey.k1
				};
				if (0 != (authResult.negFlags & NegotiateFlags.W_Negotiate56))
				{
					sealKey.AsSpan()[7] = 0xA0;
				}
				else
				{
					sealKey.AsSpan()[5] = 0xE5;
					sealKey.AsSpan()[6] = 0x38;
					sealKey.AsSpan()[7] = 0xB0;
				}

				authResult.sealKeyC2S = sealKey;
				authResult.sealKeyS2C = sealKey;
			}
			else
			{
				authResult.sealKeyC2S = authResult.exportedSessionKey;
				authResult.sealKeyS2C = authResult.exportedSessionKey;
			}

			authResult.exportedSessionKey.AsSpan().CopyTo(keymat);
			ComputeKey(keymat, SignKeyC2SMagic, ref authResult.signKeyC2S);
			ComputeKey(keymat, SignKeyS2CMagic, ref authResult.signKeyS2C);
		}

		internal static void SealMessage(
			in MessageSealParams sealParams,
			uint seqNbr,
			NegotiateFlags negFlags,
			ref Rc4Context sealKey,
			in Buffer128 signKey
			)
		{
			if (sealParams.Trailer.Length < NtlmMessageSignatureV1.StructSize)
				throw new ArgumentException(Messages.Ntlm_MacBufferTooSmall, nameof(sealParams.Trailer));

			if (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity))
			{
				Ntlm.SealV2(
					in sealParams,
					ref sealKey,
					signKey,
					seqNbr,
					(0 != (negFlags & NegotiateFlags.V_NegotiateKeyExchange))
					);
			}
			else
			{
				Ntlm.SealV1(
					in sealParams,
					ref sealKey,
					seqNbr,
					0
					);
			}
		}

		internal static void UnsealMessage(
			in MessageSealParams unsealParams,
			uint seqNbr,
			NegotiateFlags negFlags,
			ref Rc4Context sealKey,
			in Buffer128 signKey
			)
		{
			if (unsealParams.Trailer.Length < NtlmMessageSignatureV1.StructSize)
				throw new ArgumentException(Messages.Ntlm_MacBufferTooSmall, nameof(unsealParams.Trailer));

			if (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity))
			{
				Ntlm.UnsealV2(
					in unsealParams,
					ref sealKey,
					signKey,
					seqNbr,
					(0 != (negFlags & NegotiateFlags.V_NegotiateKeyExchange))
					);
			}
			else
			{
				Ntlm.UnsealV1(
					in unsealParams,
					ref sealKey,
					seqNbr
					);
			}
		}

		internal static unsafe void SignMessage(
			in MessageSignParams signParams,
			uint seqNbr,
			NegotiateFlags negFlags,
			in Buffer128 signKey,
			ref Rc4Context sealKey
			)
		{
			if (signParams.MacBuffer.Length < NtlmMessageSignatureV1.StructSize)
				throw new ArgumentException(Messages.Ntlm_MacBufferTooSmall, nameof(signParams.MacBuffer));

			bool fExtended = (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity));

			if (fExtended)
			{
				fixed (byte* pMac = signParams.MacBuffer)
				{
					ref NtlmMessageSignatureV2 sig = ref *(NtlmMessageSignatureV2*)pMac;
					Ntlm.MacV2(
						in signParams.bufferList,
						signKey,
						ref sealKey,
						(0 != (negFlags & NegotiateFlags.V_NegotiateKeyExchange)),
						seqNbr,
						out sig);
				}
			}
			else
			{
				fixed (byte* pMac = signParams.MacBuffer)
				{
					ref NtlmMessageSignatureV1 sig = ref *(NtlmMessageSignatureV1*)pMac;
					Ntlm.MacV1(
						in signParams.bufferList,
						ref sealKey,
						0,
						seqNbr,
						out sig);
				}
			}
		}

		internal static unsafe void VerifyMessage(
			in MessageVerifyParams verifyParams,
			uint seqNbr,
			NegotiateFlags negFlags,
			in Buffer128 signKey,
			ref Rc4Context sealKey
			)
		{
			if (verifyParams.MacBuffer.Length < NtlmMessageSignatureV1.StructSize)
				throw new ArgumentException(Messages.Ntlm_MacBufferTooSmall, nameof(verifyParams.MacBuffer));

			bool fExtended = (0 != (negFlags & NegotiateFlags.P_NegotiateExtendedSessionSecurity));

			if (fExtended)
			{
				Ntlm.MacV2(
					in verifyParams.bufferList,
					signKey,
					ref sealKey,
					(0 != (negFlags & NegotiateFlags.V_NegotiateKeyExchange)),
					seqNbr,
					out NtlmMessageSignatureV2 expectedSignature
					);
				fixed (byte* pMac = verifyParams.MacBuffer)
				{
					ref NtlmMessageSignatureV2 sig = ref *(NtlmMessageSignatureV2*)pMac;
					if (sig != expectedSignature)
						throw new SecurityException(Messages.Ntlm_BadMessageSignature);
				}
			}
			else
			{
				Ntlm.MacV1(
					in verifyParams.bufferList,
					ref sealKey,
					0,
					seqNbr,
					out NtlmMessageSignatureV1 expectedSignature
					);
				fixed (byte* pMac = verifyParams.MacBuffer)
				{
					ref NtlmMessageSignatureV1 sig = ref *(NtlmMessageSignatureV1*)pMac;
					if (sig != expectedSignature)
						throw new SecurityException(Messages.Ntlm_BadMessageSignature);
				}
			}
		}

	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	struct LMChallengeInput
	{
		internal static unsafe int StructSize = sizeof(LMChallengeInput);

		internal ulong serverChallenge;
		internal ulong clientChallenge;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct NtlmMessageSignatureV1 : IEquatable<NtlmMessageSignatureV1>
	{
		internal unsafe static int StructSize => sizeof(NtlmMessageSignatureV1);

		internal uint version;
		internal uint randomPad;
		internal uint checksum;
		internal uint seqnbr;

		internal unsafe Span<byte> AsSpan()
		{
			fixed (uint* pStruc = &this.version)
			{
				return new Span<byte>((byte*)pStruc, StructSize);
			}
		}

		public override bool Equals(object obj)
		{
			return obj is NtlmMessageSignatureV1 v && this.Equals(v);
		}

		public bool Equals(NtlmMessageSignatureV1 other)
		{
			return this.version == other.version &&
				   //this.randomPad == other.randomPad &&
				   this.checksum == other.checksum &&
				   this.seqnbr == other.seqnbr;
		}

		public override int GetHashCode()
		{
			int hashCode = -1736386402;
			hashCode = hashCode * -1521134295 + this.version.GetHashCode();
			//hashCode = hashCode * -1521134295 + this.randomPad.GetHashCode();
			hashCode = hashCode * -1521134295 + this.checksum.GetHashCode();
			hashCode = hashCode * -1521134295 + this.seqnbr.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(NtlmMessageSignatureV1 left, NtlmMessageSignatureV1 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NtlmMessageSignatureV1 left, NtlmMessageSignatureV1 right)
		{
			return !(left == right);
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct NtlmMessageSignatureV2 : IEquatable<NtlmMessageSignatureV2>
	{
		internal unsafe static int StructSize => sizeof(NtlmMessageSignatureV2);

		internal uint version;
		internal ulong checksum;
		internal uint seqnbr;

		internal unsafe Span<byte> ChecksumAsSpan()
		{
			fixed (ulong* pStruc = &this.checksum)
			{
				return new Span<byte>((byte*)pStruc, sizeof(ulong));
			}
		}
		internal unsafe Span<byte> AsSpan()
		{
			fixed (uint* pStruc = &this.version)
			{
				return new Span<byte>((byte*)pStruc, StructSize);
			}
		}

		public override bool Equals(object obj)
		{
			return obj is NtlmMessageSignatureV2 v && this.Equals(v);
		}

		public bool Equals(NtlmMessageSignatureV2 other)
		{
			return this.version == other.version &&
				   this.checksum == other.checksum &&
				   this.seqnbr == other.seqnbr;
		}

		public override int GetHashCode()
		{
			int hashCode = -1113294829;
			hashCode = hashCode * -1521134295 + this.version.GetHashCode();
			hashCode = hashCode * -1521134295 + this.checksum.GetHashCode();
			hashCode = hashCode * -1521134295 + this.seqnbr.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(NtlmMessageSignatureV2 left, NtlmMessageSignatureV2 right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NtlmMessageSignatureV2 left, NtlmMessageSignatureV2 right)
		{
			return !(left == right);
		}
	}
}
