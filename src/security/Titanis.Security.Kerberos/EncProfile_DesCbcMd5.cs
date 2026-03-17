using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Titanis.Crypto;

namespace Titanis.Security.Kerberos
{
	// [RFC 3961] § 6.2
	public abstract class EncProfile_DesBase : EncProfile
	{
		public sealed override int MessageBlockSizeBytes => 8;

		public sealed override int KeyBits => 64;

		public sealed override int KeyGenerationSeedSizeBytes => 8;

		public override int SignTokenSize => throw new NotImplementedException();

		/// <inheritdoc/>
		public override void GetWrapBufferSizes(WrapOptions options, out int requiredHeaderSize, out int requiredTrailerSize)
		{
			throw new NotImplementedException();
		}

		public sealed override int CipherHeaderSizeBytes => 8 + this.ChecksumSizeBytes;

		protected sealed override int SpecificKeySizeBytes => 8;


		public static ulong EncryptCbc(ulong key, ulong initialState, ulong block)
		{
			DesContext des = new DesContext(key);
			initialState = des.Transform(block ^ initialState, false);
			return initialState;
		}

		public static ulong EncryptCbc(ulong key, ulong initialState, Span<ulong> blocks)
		{
			DesContext des = new DesContext(key);
			for (int i = 0; i < blocks.Length; i++)
			{
				blocks[i] = initialState = des.Transform(blocks[i] ^ initialState, false);
			}
			return initialState;
		}

		public static ulong DecryptCbc(ulong key, ulong initialState, Span<ulong> blocks)
		{
			for (int i = 0; i < blocks.Length; i++)
			{
				var newState = blocks[i];
				blocks[i] = DesPrimitives.DecryptBlock(key, newState) ^ initialState;
				initialState = newState;
			}
			return initialState;
		}

		protected abstract void ComputeChecksum(ulong key, ReadOnlySpan<byte> message, Span<byte> checksumBuffer);
		private void VerifyChecksum(ulong key, ReadOnlySpan<byte> message, ReadOnlySpan<byte> checksum)
		{
			Span<byte> computedHash = stackalloc byte[this.ChecksumSizeBytes];
			this.ComputeChecksum(key, message, computedHash);
			BasicEncProfile.CompareChecksums(checksum, computedHash);
		}

		public sealed override void Decrypt(ReadOnlySpan<byte> protocolKey, KeyUsage usage, Span<byte> header, in SecBufferList buffers, Span<byte> trailer)
		{
			var bytes = Consolidate(header, buffers, trailer);
			var key = BitConverter.ToUInt64(protocolKey);
			DecryptCbc(key, 0, MemoryMarshal.Cast<byte, ulong>(bytes));

			Span<byte> checksum = stackalloc byte[this.ChecksumSizeBytes];
			{
				Span<byte> checksumSent = bytes.AsSpan(this.MessageBlockSizeBytes, this.ChecksumSizeBytes);
				checksumSent.CopyTo(checksum);
				checksumSent.Fill(0);
			}

			this.VerifyChecksum(key, bytes, checksum);

			buffers.CopySectionFrom(bytes.Slice(header.Length), MessageSecBufferOptions.Privacy, 0);
		}

		public sealed override void Encrypt(ReadOnlySpan<byte> protocolKey, KeyUsage usage, Span<byte> header, in SecBufferList buffers, Span<byte> trailer)
		{
			Debug.Assert(header.Length == this.CipherHeaderSizeBytes);
			Debug.Assert(trailer.Length == 0);

			byte[] cipherBuf = Consolidate(header, buffers, trailer);

			// Confounder
			GetRandomBytes(cipherBuf.Slice(0, this.MessageBlockSizeBytes));

			var key = BitConverter.ToUInt64(protocolKey);
			this.ComputeChecksum(key, cipherBuf, cipherBuf.AsSpan(this.MessageBlockSizeBytes, this.ChecksumSizeBytes));

			EncryptCbc(key, 0, MemoryMarshal.Cast<byte, ulong>(cipherBuf));

			cipherBuf.AsSpan(0, this.CipherHeaderSizeBytes).CopyTo(header);
			buffers.CopySectionFrom(cipherBuf.AsSpan(this.CipherHeaderSizeBytes), MessageSecBufferOptions.Privacy, 0);
		}

		private static byte[] Consolidate(Span<byte> header, SecBufferList buffers, Span<byte> trailer)
		{
			int cbPrivacy = buffers.TotalPrivacyLength;
			byte[] cipherBuf = new byte[header.Length + cbPrivacy + trailer.Length];
			header.CopyTo(cipherBuf);
			buffers.CopySectionTo(MessageSecBufferOptions.Privacy, 0, cipherBuf.Slice(header.Length, cbPrivacy));
			trailer.CopyTo(cipherBuf.Slice(header.Length + cbPrivacy));
			return cipherBuf;
		}

		public sealed override void RandomToKey(ReadOnlySpan<byte> input, Span<byte> keyBuffer)
		{
			if (input.Length < this.KeyGenerationSeedSizeBytes)
				throw new ArgumentException("Not enough random data.", nameof(input));
			if (keyBuffer.Length != this.KeySizeBytes)
				throw new ArgumentException("Key buffer is the wrong size.", nameof(keyBuffer));

			// TODO: So...RFC 3961 mentions the key generation seed length == 8
			// yet the Random-to-Key function accepts a 56-bit string, but 64 < 56,
			// so either way, only 56 bits of the input are used.
			// Am I missing something here?

			var value = BitConverter.ToUInt64(input);
			value = DesPrimitives.AddParityAndReverse(value);
			BinaryPrimitives.WriteUInt64LittleEndian(keyBuffer, value);
		}

		#region Key generation
		internal static ulong Reverse56Bits(ulong bits)
		{
			Debug.Assert(bits < (1UL << 56));

			ulong rev = 0;
			for (int i = 0; i < 56; i++)
			{
				rev <<= 1;
				rev |= (bits & 1);

				bits >>= 1;
			}
			return rev;
		}
		internal static ulong SetParity(ulong bits64)
		{
			ulong withParity = 0;
			for (int i = 0; i < 8; i++)
			{
				bits64 = BitOperations.RotateLeft(bits64, 8);
				var b = (byte)bits64;
				if ((BitOperations.PopCount(b) & 1) == 0)
					b ^= 1;

				withParity <<= 8;
				withParity |= b;
			}

			Debug.Assert((BitOperations.PopCount(withParity) & 1) == 0);
			return withParity;
		}
		internal static ulong Reduce64To56(ulong block)
		{
			ulong block56 = 0;
			for (int i = 0; i < 8; i++)
			{
				byte b = (byte)(block & 0x7F);
				block56 <<= 7;
				block56 |= b;

				block >>= 8;
			}

			return block56;
		}

		// https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-67r2.pdf
		// [SP800-67 Rev2] § 3.3.2 Weak Keys
		// Expressed as big-endian
		// TODO: Change to LE
		private static readonly ulong[] WeakOrSemiWeakKeys = new ulong[]
		{
			// Weak
			0x0101010101010101,
			0xFEFEFEFEFEFEFEFE,
			0xE0E0E0E0F1F1F1F1,
			0x1F1F1F1F0E0E0E0E,
			// Semi-weak
			0x011F011F010E010E, 0x1F011F010E010E01,
			0x01E001E001F101F1, 0xE001E001F101F101,
			0x01FE01FE01FE01FE, 0xFE01FE01FE01FE01,
			0x1FE01FE00EF10EF1, 0xE01FE01FF10EF10E,
			0x1FFE1FFE0EFE0EFE, 0xFE1FFE1FFE0EFE0E,
			0xE0FEE0FEF1FEF1FE, 0xFEE0FEE0FEF1FEF1,
		};
		internal static bool IsWeakOrSemiWeakKey(ulong key64)
		{
			if (BitConverter.IsLittleEndian)
				key64 = BinaryPrimitives.ReverseEndianness(key64);

			return WeakOrSemiWeakKeys.Contains(key64);
		}
		#endregion

#if DEBUG
		public string ToBitstring56(ulong bits56)
		{
			StringBuilder sb = new StringBuilder(56 + 7);
			for (int i = 0; i < 8; i++)
			{
				if (i != 0)
					sb.Append(' ');
				var bitstr = Convert.ToString((byte)(bits56 & 0x7F), 2).PadLeft(7, '0');
				sb.Append(bitstr);

				bits56 >>= 7;
			}
			return sb.ToString();
		}
#endif

		public sealed override byte[] StringToKey(ReadOnlySpan<byte> str, ReadOnlySpan<byte> salt)
		{
			var blockCount = (str.Length + salt.Length + 7) / 8;
			byte[] buf = new byte[blockCount * 8];
			Span<ulong> blocks = MemoryMarshal.Cast<byte, ulong>(buf);
			str.CopyTo(buf);
			salt.CopyTo(buf.Slice(str.Length));

			ulong temp = 0;
			for (int i = 0; i < blocks.Length; i++)
			{
				var block = blocks[i];
				block = Reduce64To56(block);
				if ((i & 1) == 1)
					block = Reverse56Bits(block);

				temp ^= block;
			}

			Debug.Assert(temp < (1UL << 56));

			// temp56 is reversed from what it should be, but AddParity reverses it back
			temp = DesPrimitives.AddParityAndReverse(temp);
			temp = CorrectKeyIfWeak(temp);
			temp = CorrectKeyIfWeak(SetParity(EncryptCbc(temp, temp, blocks)));

			var bytes = BitConverter.GetBytes(temp);
			return bytes;
		}

		private static ulong CorrectKeyIfWeak(ulong temp)
		{
			if (IsWeakOrSemiWeakKey(temp))
				temp ^= BitConverter.IsLittleEndian ? 0xF0000000_00000000 : 0xF0;
			return temp;
		}

		protected override void ComputeChecksum(ReadOnlySpan<byte> specificKey, ReadOnlySpan<byte> confounder, in SecBufferList bufferList, ReadOnlySpan<byte> micTokenHeader, Span<byte> checksum)
		{
			throw new NotImplementedException();
		}

		protected sealed override void DeriveSpecificKey(ReadOnlySpan<byte> protocolKey, KeyUsage usage, KeyIntent intent, Span<byte> specificKeyBuffer)
		{
			if (protocolKey.Length != this.KeySizeBytes)
				throw new ArgumentException("Invalid key size", nameof(protocolKey));
			if (specificKeyBuffer.Length != this.KeySizeBytes)
				throw new ArgumentException("Invalid key size", nameof(specificKeyBuffer));

			// DesCbcMd5 doesn't specify key derivation

			protocolKey.CopyTo(specificKeyBuffer);
		}

		internal override void SealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}

		internal override void UnsealMessage(ReadOnlySpan<byte> sessionKey, KeyUsage usage, uint seqNbr, WrapFlags flags, in MessageSealParams sealParams)
		{
			throw new NotImplementedException();
		}
	}
	public class EncProfile_DesCbcMd5 : EncProfile_DesBase
	{
		public override int ChecksumSizeBytes => 128 / 8;

		public override EType EType => EType.DesCbcMd5;

		internal override EncChecksumType ChecksumType => EncChecksumType.RsaMd5Des;

		protected sealed override void ComputeChecksum(ulong key, ReadOnlySpan<byte> message, Span<byte> checksumBuffer)
		{
			Debug.Assert(checksumBuffer.Length == this.ChecksumSizeBytes);

			Md5Context md5 = new Md5Context();
			md5.Initialize();
			md5.HashData(message);
			md5.HashFinal(checksumBuffer);
		}
	}
}
