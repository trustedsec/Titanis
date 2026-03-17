using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Titanis.Asn1;
using Titanis.Asn1.Serialization;

namespace Titanis.Crypto.DiffieHellman
{
	public class ModpKeyPair
	{
		public ModpKeyPair(ModpGroup group, BigInteger x)
		{
			ArgumentNullException.ThrowIfNull(group);
			this.Group = group;
			this.PrivateExponent = x;
#if DEBUG
			Debug.Print("x_a = " + BinaryHelper.ToHexString(x.ToByteArray(true, true)));
#endif

			Debug.Assert(x <= group.Q);
			Debug.Assert(x > 2);
			Debug.Assert(x.Sign > 0);

			// [SP 800-56] § 5.6.1.1 FFC Key Pair Generation
			var y = BigInteger.ModPow(group.Generator, x, group.P);
			this.PublicExponent = y;
		}

		public ModpGroup Group { get; }
		public BigInteger PrivateExponent { get; }
		public BigInteger PublicExponent { get; }

		public static ModpKeyPair Generate(ModpGroup group, int length)
		{
			ArgumentNullException.ThrowIfNull(group);
			if (length < 160)
				throw new ArgumentOutOfRangeException(nameof(length), "The key length cannot be below 160 bits.");

			if (length >= group.BitLength)
				throw new ArgumentOutOfRangeException("The length of the key cannot exceed the length of the modulus.", nameof(length));


			var xBytes = RandomNumberGenerator.GetBytes((length + 7) / 8);
			int m = (length % 8);
			if (m == 1)
				xBytes[0] = 1;
			else if (m > 1)
			{
				xBytes[0] &= (byte)((1 << m) - 1);
				xBytes[0] |= (byte)(1 << (m - 1));
			}
			var x = new BigInteger(xBytes, true, true);

			return new ModpKeyPair(group, x);
		}

		public byte[] EncodePublicExponent()
		{
			return Asn1DerEncoder.EncodeTlv(new Asn1Integer(this.PublicExponent)).ToArray();
		}

		public byte[] GenerateSessionKey(Asn1BitString subjectPublicKey)
		{
			// TODO: Check for unused bytes
			Debug.Assert(subjectPublicKey.UnusedBits == 0);

			var yb = Asn1DerDecoder.DecodeTlv<Asn1Integer>(subjectPublicKey.Octets).Value;
			BigInteger zz = CalculateAgreement(yb);

			byte[] zzBytes = new byte[this.Group.P.GetByteCount(true)];
			var cbZZ = zz.GetByteCount(true);
			zz.TryWriteBytes(zzBytes.AsSpan(zzBytes.Length - cbZZ), out int cbWritten, true, true);
			return zzBytes;
		}

		public BigInteger CalculateAgreement(BigInteger yb)
		{
			var r = BigInteger.ModPow(yb, this.Group.Q, this.Group.P);
			if (!r.IsOne)
				throw new SecurityException($"The public key sent by the server is invalid.");


			// [RFC 2631] § 2.1.1
			var zz = BigInteger.ModPow(yb, this.PrivateExponent, this.Group.P);
			if (zz.IsOne)
				throw new SecurityException("Server public key failed validation.");

#if DEBUG
			Debug.Print("zz = " + BinaryHelper.ToHexString(zz.ToByteArray(true, true)));
#endif

			return zz;
		}
	}
}
