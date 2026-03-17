using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Titanis.Asn1.Serialization;

namespace Titanis.Asn1
{
	/// <summary>
	/// Represents a <c>BIT STRING</c>.
	/// </summary>
	public struct Asn1BitString : IEquatable<Asn1BitString>, IAsn1DerEncodableValue, IAsn1DerEncodableTlv
	{
		/// <summary>
		/// Initializes a new <see cref="Asn1BitString"/>.
		/// </summary>
		/// <param name="octets">Bytes constituting the bitstring</param>
		/// <param name="unusedBits">Number of unused bits at the end</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="unusedBits"/> exceeds the number of bits in <see cref="Asn1OctetString"/>.</exception>
		/// <remarks>
		/// <paramref name="unusedBits"/> may be any value up to the number of bits in <paramref name="octets"/>.
		/// </remarks>
		public Asn1BitString(byte[] octets, byte unusedBits)
		{
			ArgumentNullException.ThrowIfNull(octets);
			if ((uint)unusedBits > (uint)(octets.Length * 8))
				throw new ArgumentOutOfRangeException(nameof(unusedBits));

			this.UnusedBits = unusedBits;
			this.Octets = octets;
		}
		/// <summary>
		/// Initializes a new <see cref="Asn1BitString"/>.
		/// </summary>
		/// <param name="value32">Value as a <see cref="uint"/></param>
		/// <remarks>
		/// The initialized <see cref="Asn1BitString"/> includes 4 octets with no unused bits.
		/// </remarks>
		public Asn1BitString(uint value32)
			: this(CreateOctetsFromUInt32(value32), 0)
		{
		}
		/// <summary>
		/// Initializes a new <see cref="Asn1BitString"/>.
		/// </summary>
		/// <param name="value64">Value as a <see cref="ulong"/></param>
		/// <remarks>
		/// The initialized <see cref="Asn1BitString"/> includes 8 octets with no unused bits.
		/// </remarks>
		public Asn1BitString(ulong value64)
			: this(CreateOctetsFromUInt64(value64), 0)
		{
		}

		private static byte[] CreateOctetsFromUInt32(uint value32)
		{
			byte[] bytes = new byte[4];
			BinaryPrimitives.WriteUInt32BigEndian(bytes, value32);
			return bytes;
		}

		private static byte[] CreateOctetsFromUInt64(ulong value64)
		{
			byte[] bytes = new byte[8];
			BinaryPrimitives.WriteUInt64BigEndian(bytes, value64);
			return bytes;
		}

		public bool IsEmpty => this.Octets == null || this.Octets.Length == 0 || (this.UnusedBits == (this.Octets.Length * 8));

		/// <summary>
		/// Gets the number of unused bits at the end of <see cref="Octets"/>.
		/// </summary>
		public byte UnusedBits { get; }
		/// <summary>
		/// Gets the octets constituting the bitstring.
		/// </summary>
		public byte[]? Octets { get; }
		/// <summary>
		/// Gets the number of bits in the string, excluding unused bits.
		/// </summary>
		public int BitLength => (this.Octets?.Length ?? 0) * 8 - this.UnusedBits;
		/// <inheritdoc/>
		public readonly Asn1Tag Tag => Asn1PredefTag.BitString;
		/// <summary>
		/// Gets the value of the bitstring as <see langword="ulong"/>.
		/// </summary>
		/// <returns>A <see langword="ulong"/> value</returns>
		/// <exception cref="OverflowException">The value of the bitstring exceeds what can be represented by a <see langword="ulong"/>.</exception>
		/// <remarks>
		/// The bitstring value is interpreted as a big-endian integer.  The size of the bitstring may exceed 64 bits so long as the excess leading bits are zero.  The last octet is included in its entirety.  That is, if <see cref="UnusedBits"/> is not a multiple of 8, the unused bits in the last (partial) octet are included.
		/// </remarks>
		public readonly ulong ToUInt64()
		{
			if (this.Octets is null)
				return 0UL;

			int cb = this.Octets.Length;
			cb -= (this.UnusedBits / 8);

			int i = 0;
			for (i = 0; i < cb && this.Octets[i] == 0; i++)
				;

			if ((cb - i) <= 8)
			{
				ulong n = this.Octets[i++];
				while (i < cb)
				{
					n <<= 8;
					n |= this.Octets[i++];
				}
				return n;
			}
			else
				throw new OverflowException(Messages.Asn1_IntegerOverflow);
		}

		/// <summary>
		/// Gets the value of the bitstring as <see langword="ulong"/>.
		/// </summary>
		/// <returns>A <see langword="ulong"/> value</returns>
		/// <exception cref="OverflowException">The value of the bitstring exceeds what can be represented by a <see langword="ulong"/>.</exception>
		/// <remarks>
		/// The bitstring value is interpreted as a big-endian integer.  The size of the bitstring may exceed 64 bits so long as the excess leading bits are zero.  The last octet is included in its entirety.  That is, if <see cref="UnusedBits"/> is not a multiple of 8, the unused bits in the last (partial) octet are included.
		/// </remarks>
		public readonly uint ToUInt32()
		{
			if (this.Octets is null)
				return 0U;

			int cb = this.Octets.Length;
			cb -= (this.UnusedBits / 8);

			int i = 0;
			for (i = 0; i < cb && this.Octets[i] == 0; i++)
				;

			if ((cb - i) <= 4)
			{
				uint n = 0;
				while (i < cb)
				{
					n <<= 8;
					n |= this.Octets[i++];
				}
				return n;
			}
			else
				throw new OverflowException(Messages.Asn1_IntegerOverflow);
		}

		/// <inheritdoc/>
		public readonly void EncodeValue(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringValue(this);
		}

		/// <inheritdoc/>
		public readonly void EncodeTlv(Asn1DerEncoder encoder)
		{
			encoder.EncodeBitStringTlv(this.Octets, this.UnusedBits, this.Tag);
		}

		/// <inheritdoc/>
		public override readonly bool Equals(object? obj)
		{
			return obj is Asn1BitString bitstring && this.Equals(bitstring);
		}

		/// <inheritdoc/>
		public readonly bool Equals(Asn1BitString other)
		{
			return this.UnusedBits == other.UnusedBits &&
				   ArrayExtensions.ElementsEqual(this.Octets, other.Octets);
		}

		/// <inheritdoc/>
		public override readonly int GetHashCode()
		{
			return System.HashCode.Combine(this.UnusedBits, ArrayExtensions.GetElementsHashCode(this.Octets));
		}

		public static bool operator ==(Asn1BitString left, Asn1BitString right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1BitString left, Asn1BitString right)
		{
			return !(left == right);
		}
	}

	/// <summary>
	/// Represents an enumeration as a <c>BIT STRING</c>.
	/// </summary>
	/// <typeparam name="TEnum">Underlying enumeration</typeparam>
	public readonly struct Asn1BitString<TEnum> : IAsn1DerEncodableValue, IAsn1DerEncodableTlv, IEquatable<Asn1BitString<TEnum>> where TEnum : struct, Enum, IConvertible
	{
		static Asn1BitString()
		{
			var bitCountAttr = typeof(TEnum).GetCustomAttribute<BitCountAttribute>();
			if (bitCountAttr == null)
				throw new InvalidProgramException($"Type {typeof(TEnum).FullName} is missing {nameof(bitCountAttr)} and cannot be used as a generic argument for {nameof(Asn1BitString)}.");

			BitCount = bitCountAttr.BitCount;
		}

		public static int BitCount { get; }

		/// <summary>
		/// Initializes a new <see cref="Asn1BitString{TEnum}"/>.
		/// </summary>
		/// <param name="value">Value</param>
		public Asn1BitString(TEnum value)
		{
			this.Value = value;
		}

		public TEnum Value { get; }
		/// <inheritdoc/>
		public readonly Asn1Tag Tag => Asn1PredefTag.BitString;

		/// <inheritdoc/>
		public readonly void EncodeValue(Asn1DerEncoder encoder) => encoder.EncodeBitStringValue(this.Value.ToUInt64(null), BitCount);

		/// <inheritdoc/>
		public readonly void EncodeTlv(Asn1DerEncoder encoder) => encoder.EncodeEnumeratedTlv(this.Value.ToInt64(null), Asn1PredefTag.BitString);

		public override bool Equals(object? obj)
		{
			return obj is Asn1BitString<TEnum> bitstring && Equals(bitstring);
		}

		public bool Equals(Asn1BitString<TEnum> other)
		{
			return EqualityComparer<TEnum>.Default.Equals(Value, other.Value) &&
				   BitCount == BitCount
				   ;
		}

		public override int GetHashCode()
		{
			return System.HashCode.Combine(Value, BitCount);
		}

		public static bool operator ==(Asn1BitString<TEnum> left, Asn1BitString<TEnum> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Asn1BitString<TEnum> left, Asn1BitString<TEnum> right)
		{
			return !(left == right);
		}
	}
}
