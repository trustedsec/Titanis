using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Text;
using Titanis.IO;

namespace Titanis.Asn1.Serialization
{
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
			ByteWriter writer
			)
			: base(encoding)
		{
			Debug.Assert(writer != null);

			this._writer = writer;
		}

		private ByteWriter _writer;
		public ByteWriter GetWriter() => this._writer;
		public override Memory<byte> GetBytes() => this._writer.GetData();

		public int Position => this._writer.Position;

		public static Memory<byte> EncodeTlv<T>(T obj)
			where T : IAsn1DerEncodableTlv
		{
			ByteWriter writer = new ByteWriter(0x10, ByteWriterOptions.Reverse);
			Asn1DerEncoder encoder = new Asn1DerEncoder(Asn1DerEncoding.Instance, writer);
			encoder.EncodeObjTlv(obj);
			return writer.GetData();
		}

		public void EncodeObjTlv<T>(T obj) where T : IAsn1DerEncodableTlv
		{
			int pos = this._writer.Position;
			obj.EncodeValue(this);
			this.EncodeCloseTlvHeader(obj.Tag, pos);
		}

		public override void EncodeOctetString(ReadOnlySpan<byte> octets)
		{
			this._writer.WriteBytes(octets);
		}

		public override void EncodeBitString(Asn1BitString bitstring)
		{
			this._writer.WriteBytes(bitstring.octets);
			this._WriteByte(bitstring.unusedBits);
		}

		// [X.690] § 8.1.2
		private void EncodeTag(Asn1Tag tag)
		{
			var value = tag.TagNumber;
			var flags = tag.Flags;
			if (value < 0x31)
			{
				var n = (byte)((flags) | value);
				this._WriteByte(n);
			}
			else
			{
				this._WriteByte((byte)(flags | 0x1F));
				uint rvalue = 0;
				for (; value != 0; value >>= 7)
				{
					rvalue <<= 7;
					rvalue |= value & 0x7F;
				}

				for (; rvalue != 0; rvalue >>= 7)
					this._WriteByte((byte)((rvalue & 0x7F) | 0x80));
			}
		}

		public override void EncodeCloseTlvHeader(Asn1Tag tag, int endPos)
		{
			this.EncodeLength((uint)(this._writer.Position - endPos));
			this.EncodeTag(tag);
		}

		private void _WriteByte(byte b) => this._writer.WriteByte(b);

		private void EncodeLength(uint length)
		{
			if (length < 0x80)
				this._WriteByte((byte)length);
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

		public override void EncodeNull(DBNull dbnull)
		{
		}
		public override void EncodeBool(bool v)
		{
			this._WriteByte(v ? (byte)0xFF : (byte)0);
		}
		public unsafe override void EncodeUtf8har(char c)
		{
			int cb = System.Text.Encoding.UTF8.GetByteCount(new ReadOnlySpan<char>(&c, 1));
			var charSpan = this._writer.Consume(cb);
			System.Text.Encoding.UTF8.GetBytes(new ReadOnlySpan<char>(&c, 1), charSpan);
		}

		public override void EncodeSByte(sbyte n) => this.EncodeInt64(n);
		public override void EncodeByte(byte n) => this.EncodeUInt64(n);
		public override void EncodeInt16(short n) => this.EncodeInt64(n);
		public override void EncodeUInt16(ushort n) => this.EncodeUInt64(n);
		public override void EncodeInt32(int n) => this.EncodeInt64(n);

		public override void EncodeUInt32(uint n)
		{
			this.EncodeUInt64(n);
		}

		public override void EncodeInt64(long n)
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

		public override void EncodeUInt64(ulong m)
		{
			byte b;
			do
			{
				b = (byte)m;
				this._WriteByte(b);
				m >>= 8;
			} while (m != 0);
			if (0 != (b & 0x80))
				this._WriteByte(0);
		}

		public override void EncodeBigInteger(BigInteger n)
		{
			byte[] bytes = n.ToByteArray();
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			this._writer.WriteBytes(bytes);
		}

		public override void EncodeSingle(float n)
		{
			throw new NotImplementedException();
		}
		public override void EncodeDouble(double n)
		{
			throw new NotImplementedException();
		}
		public override void EncodeDecimal(decimal n)
		{
			throw new NotImplementedException();
		}
		public override void EncodeUtcTime(DateTime dt)
		{
			// TODO: This should support milliseconds
			string str = dt.ToString("yyMMddHHmmss");
			//if (dt.Kind == DateTimeKind.Utc)
			str += "Z";
			this.EncodeString(str);
		}
		public override void EncodeDateTime(DateTime dt)
		{
			// TODO: This should support milliseconds
			string str = dt.ToString("yyyyMMddHHmmss");
			//if (dt.Kind == DateTimeKind.Utc)
			str += "Z";
			this.EncodeString(str);
		}
		public override void EncodeString(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				int cb = System.Text.Encoding.UTF8.GetByteCount(str);
				var charBytes = this._writer.Consume(cb);
				System.Text.Encoding.UTF8.GetBytes(str, charBytes);
			}
		}

		public override void EncodeOid(Asn1Oid oid)
		{
			var etc = oid._subparts;
			if (etc != null)
			{
				for (int i = etc.Length - 1; i >= 0; i--)
				{
					this._EncodeOidPart(etc[i]);
				}
			}

			var initial = oid._part0.Value.Value * 40 + oid._part1.Value.Value;
			this._WriteByte((byte)initial);
		}

		public override void EncodeRelativeOid(Asn1Oid oid)
		{
			var etc = oid._subparts;
			if (etc != null)
			{
				for (int i = etc.Length - 1; i >= 0; i--)
				{
					this._EncodeOidPart(etc[i]);
				}
			}
			if (oid._part1.Value != 0)
				_EncodeOidPart(oid._part1);
			if (oid._part0.Value != 0)
				_EncodeOidPart(oid._part0);

			var initial = oid._part0.Value.Value * 40 + oid._part1.Value.Value;
			this._WriteByte((byte)initial);
		}

		private void _EncodeOidPart(Asn1OidPart part)
		{
			var value = part.Value.Value;
			this._WriteByte((byte)(value & 0x7F));
			value >>= 7;
			while (value != 0)
			{
				// Since 0x80 is being set anyway, there is no need to mask value
				this._WriteByte((byte)(0x80 | value));
				value >>= 7;
			}
		}

		//public void EncodeTlv<T, TNode>(T obj, TNode node)
		//	where TNode : IAsn1Node<T>
		//{
		//	node.EncodeTlv(this, obj);
		//}

		//public void EncodeTlv<T, TNode>(Asn1Tag tag, T obj, TNode node)
		//	where TNode : IAsn1Node<T>
		//{
		//	int startIndex = this._writer.Position;

		//	node.EncodeValue(this, obj);
		//	this.WriteCloseTlvHeader((byte)tag, startIndex);
		//}

		//public void EncodeTlv<T>(Asn1Tag tag, T obj)
		//	where T : IAsn1DerEncodable
		//{
		//	int endIndex = this._writer.Position;
		//	obj.EncodeValue(this);
		//	int length = this._writer.Position - endIndex;
		//	this.WriteLength((uint)length);
		//	this.WriteByte((byte)tag);
		//}
	}
}
