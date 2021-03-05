using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Titanis.Crypto
{
	struct Rc4State
	{
		internal byte i;
		internal byte j;
	}

	public struct Rc4Context
	{
		internal SBox sbox;
		internal Rc4State state;

		public Rc4Context(ReadOnlySpan<byte> key)
			: this()
		{
			this.Initialize(key);
		}

		public void Initialize(ReadOnlySpan<byte> key)
		{
			Rc4.BuildS(key, out this.sbox);
		}

		public void Transform(ReadOnlySpan<byte> input, Span<byte> output)
		{
			this.state = Rc4.Transform(
				ref this.sbox,
				input,
				output,
				this.state);
		}
	}

	public class Rc4 : SymmetricAlgorithm
	{
		public Rc4()
		{
			this.BlockSize = 1;
		}

		private static readonly KeySizes[] _legalBlockSizes = new KeySizes[] { new KeySizes(1, 1, 1) };
		public override KeySizes[] LegalBlockSizes => _legalBlockSizes;

		private void ValidateState()
		{
			var key = this.Key;
			if (!IsKeyValid(key))
				throw new InvalidOperationException(Messages.Crypto_InvalidKey);
		}

		public static bool IsKeyValid(ReadOnlySpan<byte> key)
		{
			return (key.Length >= (40 / 8));
		}

		public static void Transform(ReadOnlySpan<byte> key, Span<byte> data)
		{
			Transform(key, data, data);
		}
		public static void Transform(ReadOnlySpan<byte> key, ReadOnlySpan<byte> input, Span<byte> output)
		{
			if (!IsKeyValid(key))
				throw new ArgumentException(Messages.Crypto_InvalidKey);

			SBox sbox;
			Span<byte> S = stackalloc byte[256];
			BuildS(key, out sbox);
			Transform(ref sbox, input, output, new Rc4State());
		}

		internal static Rc4State Transform(ref SBox sbox, ReadOnlySpan<byte> inputBuffer, Span<byte> outputBuffer, Rc4State state)
		{
			var s = sbox.S;
			for (int index = 0; index < inputBuffer.Length; index++)
			{
				state.i++;
				state.j += s[state.i];

				byte temp = s[state.i];
				s[state.i] = s[state.j];
				s[state.j] = temp;
				byte k = s[(byte)((s[state.i] + s[state.j]))];

				outputBuffer[index] = (byte)(inputBuffer[index] ^ k);
			}

			return state;
		}

		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return CreateTransform(rgbKey ?? this.Key);
		}

		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return CreateTransform(rgbKey ?? this.Key);
		}

		private ICryptoTransform CreateTransform(ReadOnlySpan<byte> rgbKey)
		{
			if (rgbKey.Length == 0)
				throw new ArgumentNullException(nameof(rgbKey));
			else if (!IsKeyValid(rgbKey))
				throw new ArgumentException(Messages.Crypto_InvalidKey, nameof(rgbKey));

			return new Rc4Transform(rgbKey);
		}

		public override void GenerateIV()
		{
		}

		public override void GenerateKey()
		{
			var keySize = this.KeySize;
			if (!IsValidKeySize(keySize))
				throw new InvalidOperationException(Messages.Crypto_InvalidKeySize);

			int cb = this.KeySize / 8;
			byte[] key = new byte[cb];
			RandomNumberGenerator.Create().GetBytes(key);
			this.Key = key;
		}

		private bool IsValidKeySize(int keySize)
		{
			return (keySize >= 40);
		}

		internal static void BuildS(ReadOnlySpan<byte> key, out SBox sbox)
		{
			sbox = new SBox();
			Span<byte> s = sbox.S;

			int keylength = key.Length;
			for (int i = 0; i < 256; i++)
			{
				s[i] = (byte)i;
			}

			byte j = 0;
			for (int i = 0; i < 256; i++)
			{
				j = (byte)(j + s[i] + key[i % keylength]);
				var tmp = s[i];
				s[i] = s[j];
				s[j] = tmp;
			}
		}
	}
}
