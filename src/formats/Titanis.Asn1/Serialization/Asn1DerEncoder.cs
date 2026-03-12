using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
	[Flags]
	public enum Asn1DerEncoderOptions
	{
		None = 0,
		Ber = 1,
	}

	/// <summary>
	/// Encodes a value according to ASN.1 Distinguished Encoding Rules.
	/// </summary>
	public class Asn1DerEncoder : Asn1Encoder
	{
		/// <summary>
		/// Initializes a new <see cref="Asn1DerEncoder"/>.
		/// </summary>
		/// <param name="encoding"></param>
		/// <param name="writer"></param>
		internal Asn1DerEncoder(
			Asn1Encoding encoding,
			ByteWriter writer,
			IAsn1EncoderCallback? callback = null,
			Asn1DerEncoderOptions options = Asn1DerEncoderOptions.None
			)
			: base(encoding)
		{
			Debug.Assert(writer != null);
			Debug.Assert(writer.IsReverse);

			this._writer = writer;
			this._callback = callback;
			this._options = options;
		}

		private ByteWriter _writer;
		private readonly IAsn1EncoderCallback? _callback;
		private readonly Asn1DerEncoderOptions _options;
		private bool IsBer => (this._options & Asn1DerEncoderOptions.Ber) != 0;


		public ByteWriter GetWriter() => this._writer;
		public override Memory<byte> GetBytes() => this._writer.GetData();

		public int Position => this._writer.Position;

		public static Memory<byte> EncodeTlv<T>(T obj, IAsn1EncoderCallback? callback = null, Asn1DerEncoderOptions options = Asn1DerEncoderOptions.None)
			where T : IAsn1DerEncodableTlv
		{
			ByteWriter writer = new ByteWriter(0x10, ByteWriterOptions.Reverse);
			Asn1DerEncoder encoder = new Asn1DerEncoder(Asn1DerEncoding.Instance, writer, callback, options);
			obj.EncodeTlv(encoder);
			return writer.GetData();
		}

		public static Memory<byte> EncodeValue<T>(T obj, IAsn1EncoderCallback? callback = null, Asn1DerEncoderOptions options = Asn1DerEncoderOptions.None)
			where T : IAsn1DerEncodableValue
		{
			ByteWriter writer = new ByteWriter(0x10, ByteWriterOptions.Reverse);
			Asn1DerEncoder encoder = new Asn1DerEncoder(Asn1DerEncoding.Instance, writer, callback, options);
			obj.EncodeValue(encoder);
			return writer.GetData();
		}

		public void EncodeValueTlv<T>(T obj, Asn1Tag tag) where T : IAsn1DerEncodableValue
		{
			int pos = this._writer.Position;
			obj.EncodeValue(this);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public void EncodeValueTlv<T>(T obj) where T : IAsn1DerEncodableValue, IAsn1DerEncodableTlv
			=> this.EncodeValueTlv(obj, obj.Tag);

		public void EncodeExplicitTlv<T>(Asn1Tag explicitTag, T obj, Action<Asn1DerEncoder, T> encodeFunc)
		{
			int pos = this._writer.Position;
			encodeFunc(this, obj);
			this.EncodeCloseTlvHeader(explicitTag, pos);
		}

		public void EncodeListTlv<T>(Asn1Tag listTag, IList<T> list, Action<Asn1DerEncoder, T> itemEncodeFunc)
		{
			var pos = this.Position;
			this.EncodeListValue(list, itemEncodeFunc);
			this.EncodeCloseTlvHeader(listTag, pos);
		}
		public void EncodeListValue<T>(IList<T> list, Action<Asn1DerEncoder, T> itemEncodeFunc)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				var item = list[i];
				itemEncodeFunc(this, item);
			}
		}

		internal void WriteBytes(ReadOnlySpan<byte> bytes)
		{
			this._writer.WriteBytes(bytes);
		}
		public override void EncodeOctetStringTlv(ReadOnlySpan<byte> octets, Asn1Tag tag)
		{
			int pos = this.Position;
			this.EncodeOctetStringValue(octets);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public void EncodeOctetStringValue(ReadOnlySpan<byte> octets)
		{
			this.WriteBytes(octets);
		}

		public override void EncodeBitStringTlv(ulong bitstringValue, int bitCount, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeBitStringValue(bitstringValue, bitCount);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		public override void EncodeBitStringValue(ulong bitstringValue, int bitCount)
		{
			if ((uint)bitCount > 64UL)
				throw new ArgumentOutOfRangeException(nameof(bitCount));

			var byteCount = (bitCount + 7) / 8;
			Span<byte> bytes = stackalloc byte[byteCount];
			while (byteCount > 0)
			{
				byteCount--;
				bytes[byteCount] = (byte)bitstringValue;
				bitstringValue >>= 8;
			}

			var unusedBits = (8 - (bitCount % 8)) & 0x07;
			this.EncodeBitStringValue(bytes, (byte)unusedBits);
		}
		public override void EncodeBitStringTlv(ReadOnlySpan<byte> bitstring, byte unusedBits, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeBitStringValue(bitstring, unusedBits);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		public override void EncodeBitStringValue(ReadOnlySpan<byte> bitstring, byte unusedBits)
		{
			if ((uint)unusedBits > (uint)(bitstring.Length * 8))
				throw new ArgumentOutOfRangeException(nameof(unusedBits));

			if (unusedBits >= 8)
			{
				bitstring = bitstring.Slice(0, bitstring.Length - (unusedBits / 8));
				unusedBits %= 8;
			}

			if (bitstring.Length > 0)
			{
				this.WriteBytes(bitstring);
				this._WriteByte(unusedBits);
			}
		}

		// [X.690] § 8.1.2
		private void EncodeTag(Asn1Tag tag)
		{
			var value = tag.TagNumber;
			var flags = tag.Flags;
			if (value < 31)
			{
				var n = (byte)((flags) | value);
				this._WriteByte(n);
			}
			else
			{
				this._WriteByte((byte)(flags | 0x1F));
				uint rvalue = 0;
				int byteCount = 0;
				for (; value != 0; value >>= 7)
				{
					rvalue <<= 7;
					rvalue |= value & 0x7F;
					byteCount++;
				}

				while (--byteCount > 0)
				{
					this._WriteByte((byte)((rvalue & 0x7F) | 0x80));
					rvalue >>= 7;
				}
				Debug.Assert(rvalue < 0x80);
				this._WriteByte((byte)rvalue);
			}
		}

		public override void EncodeCloseTlvHeader(Asn1Tag tag, int endPos)
		{
			this.EncodeLength((uint)(this._writer.Position - endPos));
			this.EncodeTag(tag);

			this._callback?.OnCloseTlv(this, tag);
		}

		private void _WriteByte(byte b) => this._writer.WriteByte(b);

		private void EncodeLength(uint length)
		{
			if (length < 0x80)
				this._WriteByte((byte)length);
			else if (this.IsBer)
			{
				this._writer.WriteUInt32BE(length);
				this._writer.WriteByte(0x84);
			}
			else
			{
				int endPos = this._writer.Position;
				//
				byte b;
				var m = length;
				do
				{
					b = (byte)m;
					this._WriteByte(b);
					m >>= 8;
				} while (m != 0);
				//
				int cbLength = this._writer.Position - endPos;
				this._WriteByte((byte)(0x80 | cbLength));
			}
		}

		public override void EncodeNullTlv(Asn1Null nul, Asn1Tag tag)
		{
			int pos = this._writer.Position;
			this.EncodeNullValue();
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public void EncodeNullValue()
		{
			// Do nothing
		}

		public override void EncodeBoolTlv(bool v, Asn1Tag tag)
		{
			int pos = this._writer.Position;
			this._WriteByte(v ? (byte)0xFF : (byte)0);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public override void EncodeSByteTlv(sbyte n, Asn1Tag tag) => this.EncodeInt64Tlv(n, tag);
		public override void EncodeByteTlv(byte n, Asn1Tag tag) => this.EncodeUInt64Tlv(n, tag);
		public override void EncodeInt16Tlv(short n, Asn1Tag tag) => this.EncodeInt64Tlv(n, tag);
		public override void EncodeUInt16Tlv(ushort n, Asn1Tag tag) => this.EncodeUInt64Tlv(n, tag);
		public override void EncodeInt32Tlv(int n, Asn1Tag tag) => this.EncodeInt64Tlv(n, tag);

		public override void EncodeUInt32Tlv(uint n, Asn1Tag tag)
		{
			this.EncodeUInt64Tlv(n, tag);
		}

		public override void EncodeInt64Tlv(long n, Asn1Tag tag)
		{
			int pos = this._writer.Position;
			this.EncodeInt64Value(n);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public override void EncodeInt64Value(long n)
		{
			bool neg = (n < 0);
			byte b;
			do
			{
				b = (byte)n;
				this._WriteByte(b);
				n >>= 8;
			} while (((ulong)n + 1) > 1);
			if (!neg && 0 != (b & 0x80))
				this._WriteByte(0);
			if (neg && 0 == (b & 0x80))
				this._WriteByte(0xFF);
		}

		public override void EncodeUInt64Tlv(ulong m, Asn1Tag tag)
		{
			int pos = this._writer.Position;
			this.EncodeUInt64Value(m);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public override void EncodeUInt64Value(ulong n)
		{
			byte b;
			do
			{
				b = (byte)n;
				this._WriteByte(b);
				n >>= 8;
			} while (n != 0);
			if (0 != (b & 0x80))
				this._WriteByte(0);
		}

		public override void EncodeBigIntegerTlv(BigInteger n, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeBigIntegerValue(n);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public override void EncodeBigIntegerValue(BigInteger n)
		{
			byte[] bytes = n.ToByteArray();
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			this._writer.WriteBytes(bytes);
		}

		public override void EncodeEnumeratedTlv(long value, Asn1Tag tag)
		{
			int pos = this.Position;
			this.EncodeEnumeratedValue(value);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		public override void EncodeEnumeratedValue(long value)
		{
			this.EncodeInt64Value(value);
		}

		public override void EncodeSingleTlv(float n, Asn1Tag tag)
		{
			throw new NotImplementedException();
		}
		public override void EncodeDoubleTlv(double n, Asn1Tag tag)
		{
			throw new NotImplementedException();
		}
		public override void EncodeDecimalTlv(decimal n, Asn1Tag tag)
		{
			throw new NotImplementedException();
		}
		public override void EncodeUtcTimeTlv(DateTime dt, Asn1Tag tag)
		{
			var pos = this.Position;
			// TODO: This should support milliseconds
			string str = dt.ToString("yyMMddHHmmss");
			//if (dt.Kind == DateTimeKind.Utc)
			str += "Z";
			this.EncodeStringBytes(str);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		public override void EncodeDateTimeTlv(DateTime dt, Asn1Tag tag)
		{
			var pos = this.Position;
			// TODO: This should support milliseconds
			string str = dt.ToString("yyyyMMddHHmmss");
			//if (dt.Kind == DateTimeKind.Utc)
			str += "Z";
			this.EncodeStringBytes(str);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		protected override void EncodeStringBytes(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int cb = System.Text.Encoding.UTF8.GetByteCount(str);
				var charBytes = this._writer.Consume(cb);
				System.Text.Encoding.UTF8.GetBytes(str, charBytes);
			}
		}
		public override void EncodeStringTlv<T>(T str, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeStringBytes(str.Value);
			this.EncodeCloseTlvHeader(tag, pos);
		}
		public sealed override void EncodeUtf8StringTlv(string str, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeStringBytes(str);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public override void EncodeOidTlv(Asn1Oid oid, Asn1Tag tag)
		{
			var pos = this.Position;
			this.EncodeOidValue(oid);
			this.EncodeCloseTlvHeader(tag, pos);
		}

		public void EncodeOidValue(Asn1Oid oid)
		{
			var etc = oid._arcs;
			if (etc != null)
			{
				for (int i = etc.Length - 1; i >= 2; i--)
				{
					this._EncodeOidArc(etc[i]);
				}
			}

			var initial = oid[0] * 40 + oid[1];
			this._WriteByte((byte)initial);
		}

		public override void EncodeRelativeOidTlv(Asn1Oid oid, Asn1Tag tag)
		{
			var pos = this.Position;
			var etc = oid._arcs;
			if (etc != null)
			{
				for (int i = etc.Length - 1; i >= 0; i--)
				{
					this._EncodeOidArc(etc[i]);
				}
			}

			this.EncodeCloseTlvHeader(tag, pos);
		}

		private void _EncodeOidArc(uint arc)
		{
			this._WriteByte((byte)(arc & 0x7F));
			arc >>= 7;
			while (arc != 0)
			{
				// Since 0x80 is being set anyway, there is no need to mask value
				this._WriteByte((byte)(0x80 | arc));
				arc >>= 7;
			}
		}
	}
}
