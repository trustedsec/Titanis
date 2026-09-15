using System.Buffers.Binary;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml;
using Titanis.Msrpc.Mseven6.BinXml;
using Titanis.Winterop.Security;

namespace Titanis.Msrpc.Mseven6.BinXml
{
	ref struct BinXmlReadContext
	{
		public BinXmlReadContext(ReadOnlySpan<byte> bytes)
		{
			this.bytes = bytes;
		}

		private readonly ReadOnlySpan<byte> bytes;
		private int readIndex;

		private int PeekByte() => (this.readIndex < this.bytes.Length) ? this.bytes[this.readIndex] : -1;
		private byte ReadByte()
		{
			var n = this.PeekByte();
			if (n < 0)
				throw new EndOfStreamException();

			this.readIndex++;

			return (byte)n;
		}
		private Token PeekToken() => (Token)this.PeekByte();
		private Token ReadToken() => (Token)this.ReadByte();
		private void ReadExpectedToken(Token expected)
		{
			var token = this.ReadToken();
			if (token != expected)
				throw new InvalidDataException($"Expected token {expected}, but encountered {token}.");
		}
		private void ReadExpectedByte(byte expected)
		{
			var b = this.ReadByte();
			if (b != expected)
				throw new InvalidDataException($"Expected token {expected}, but encountered {b}.");
		}
		private ReadOnlySpan<byte> Consume(int count)
		{
			var bytes = this.bytes.Slice(this.readIndex, count);
			this.readIndex += count;
			return bytes;
		}
		private ushort ReadUInt16() => BinaryPrimitives.ReadUInt16LittleEndian(this.Consume(2));
		private uint ReadUInt32() => BinaryPrimitives.ReadUInt32LittleEndian(this.Consume(4));
		private Guid ReadGuid() => new Guid(this.Consume(16));



		public BinXmlDocument ReadDocument()
		{
			var doc = new BinXmlDocument(
				this.TryReadProlog(),
				this.ReadFragment(),
				this.TryReadMisc()
				);
			this.ReadEofToken();
			return doc;
		}

		private ProcessingInstruction? TryReadProlog() => this.TryReadPI();

		private Fragment ReadFragment()
		{
			return new Fragment(
				this.TryReadFragHeaders(),
				this.ReadElementOrTemplateInstance()
				);
		}

		private FrugalList<FragmentHeader> TryReadFragHeaders()
		{
			FrugalList<FragmentHeader> hdrs = new FrugalList<FragmentHeader>();
			FragmentHeader? hdr;
			while ((hdr = this.TryReadFragHeader()) != null)
			{
				hdrs.Add(hdr);
			}

			return hdrs;
		}

		private FragmentHeader TryReadFragHeader()
		{
			if (this.PeekToken() == Token.FragHeader)
			{
				this.ReadToken();
				return new FragmentHeader(
					this.ReadByte(),
					this.ReadByte(),
					(FragmentHeaderFlags)this.ReadByte()
					);
			}
			else
				return null;
		}

		private Node ReadElementOrTemplateInstance()
		{
			var token = this.PeekToken();
			if (token is Token.OpenStartElement_NoElements or Token.OpenStartElement_HasAttributes)
				return this.ReadElement();
			else if (token is Token.TemplateInstance)
				return this.ReadTemplateInstance();
			else
				throw new InvalidDataException($"Expected element or template, but encountered {token}.");
		}
		private FrugalList<Node> ReadContent()
		{
			return this.ReadAttrCharData(true);
		}
		private Element ReadElement()
		{
			// StartElement
			var token = this.ReadToken();
			// TODO: How many?
			var depId = this.ReadUInt16();
			var elemByteLength = this.ReadUInt32();
			var name = this.ReadName();

			FrugalList<Attr> attrList;
			if (token is Token.OpenStartElement_HasAttributes)
			{
				attrList = this.ReadAttrList();
			}
			else if (token is Token.OpenStartElement_NoElements)
			{
				attrList = [];
			}
			else
				throw new InvalidDataException($"Expected OpenStartElementToken, but encountered {token}");

			token = this.ReadToken();
			FrugalList<Node> content;
			if (token is Token.CloseEmptyElement)
			{
				// No children
				content = new FrugalList<Node>();
			}
			else if (token is Token.CloseStartElement)
			{
				content = this.ReadContent();
				this.ReadExpectedToken(Token.EndElement);
			}
			else
				throw new InvalidDataException($"Expected CloseEmptyElement or CloseStartElement, but encountered {token}");

			return new Element(name, attrList, content);
		}

		private FrugalList<Attr> ReadAttrList()
		{
			var byteLength = (int)this.ReadUInt32();
			int endIndex = this.readIndex + byteLength;
			FrugalList<Attr> attrs = new FrugalList<Attr>();

			// Attribute
			Token attrToken;
			while ((attrToken = this.PeekToken()) is Token.Attribute_Last or Token.Attribute_More)
			{
				this.ReadToken();
				attrs.Add(new Attr(this.ReadName(), this.ReadAttrCharData(true)));
			} while (attrToken == Token.Attribute_More) ;

			// TODO: Check length
			return attrs;
		}

		private FrugalList<Node> ReadAttrCharData(bool forElement)
		{
			FrugalList<Node> charData = new FrugalList<Node>();
			while (true)
			{
				var token = this.PeekToken();
				bool more = 0 != (token & Token.More);
				token &= ~Token.More;

				if (token is Token.ValueText_5)
				{
					this.ReadToken();
					this.ReadExpectedByte((byte)BinXmlValueType.String);
					charData.Add(new TextValueNode(this.ReadLengthPrefixedUnicodeString()));
				}
				else if (token is Token.NormalSubstitution or Token.OptionalSubstitution)
				{
					this.ReadToken();
					charData.Add(new Substitution(token, this.ReadUInt16(), this.ReadValueType()));
				}
				else if (token is Token.CharRef_8)
				{
					this.ReadToken();
					charData.Add(new CharRefNode(this.ReadUInt16()));
				}
				else if (token is Token.EntityRef_9)
				{
					this.ReadToken();
					charData.Add(new EntityRefNode(this.ReadName()));
				}
				else if (forElement && token is Token.CdataSection_7)
				{
					throw new NotImplementedException();
				}
				else if (forElement && token is Token.PiTarget)
				{
					charData.Add(this.TryReadPI());
				}
				else if (forElement && token is Token.OpenStartElement_HasAttributes or Token.OpenStartElement_NoElements)
				{
					charData.Add(this.ReadElement());
					continue;
				}
				else if (forElement && token is Token.EndElement)
				{
					break;
				}
				else
				{
					throw new NotImplementedException();
				}

				if (!more)
				{
					break;
				}
			}

			return charData;
		}

		// TODO: CDATA section


		private ProcessingInstruction? TryReadPI()
		{
			if (this.PeekToken() is Token.PiTarget)
			{
				this.ReadToken();
				string name = this.ReadName();
				this.ReadExpectedToken(Token.PiData);
				string data = this.ReadLengthPrefixedUnicodeString();
				return new ProcessingInstruction(name, data);
			}
			else
				return null;
		}

		private string ReadName()
		{
			var hash = this.ReadUInt16();
			var cch = this.ReadUInt16();
			var str = this.ReadNullTerminatedUnicodeString(cch);
			return str;
		}

		private void ReadEofToken()
		{
			this.ReadExpectedToken(Token.Eof);
		}





		private TemplateInstance ReadTemplateInstance()
		{
			this.ReadExpectedToken(Token.TemplateInstance);
			// TemplateDef
			this.ReadExpectedByte(0);
			var id = this.ReadGuid();
			var byteLength = this.ReadUInt32();
			var headers = this.TryReadFragHeaders();
			var elem = this.ReadElement();
			this.ReadEofToken();

			var data = this.ReadTemplateInstanceData();

			return new TemplateInstance(id, headers, elem, data);
		}

		private ValueSpecEntry[] ReadTemplateInstanceData()
		{
			// ValueSpec
			var numValues = this.ReadUInt32();
			ValueSpecEntry[] entries = new ValueSpecEntry[numValues];
			for (int i = 0; i < entries.Length; i++)
			{
				entries[i] = new ValueSpecEntry(
					this.ReadUInt16(),
					this.ReadValueType()
					);
				this.ReadExpectedByte(0);
			}
			for (int i = 0; i < entries.Length; i++)
			{
				ref var spec = ref entries[i];
				var value = this.ReadValue(spec.valueByteLength, spec.valueType);
				spec.Value = value;
			}

			return entries;
		}

		private BinXmlValueType ReadValueType()
		{
			var code = (BinXmlValueType)this.ReadByte();
			if (code == 0)
			{
				// TODO: How is NullType differentiated from the hex array types?
				if (this.PeekByte() is 0x94 or 0x95)
					code = (BinXmlValueType)(this.ReadByte() << 8);
			}
			return code;
		}

		private Node ReadValue(int size, BinXmlValueType valueType)
		{
			var value = ReadValue(this.bytes.Slice(this.readIndex, size), valueType);
			this.readIndex += size;
			return value;
		}
		private static Node ReadValue(ReadOnlySpan<byte> bytes, BinXmlValueType valueType)
		{
			var ctx = new BinXmlReadContext(bytes);
			int size = bytes.Length;

			return valueType switch
			{
				BinXmlValueType.String => new TextValueNode(Encoding.Unicode.GetString(ctx.Consume(size))),
				// TODO: ANSI, not UTF8
				BinXmlValueType.AnsiString => new TextValueNode(Encoding.UTF8.GetString(ctx.Consume(size))),
				BinXmlValueType.Int8 => new IntegerValue((sbyte)ctx.ReadByte(), valueType),
				BinXmlValueType.UInt8 => new UnsignedIntegerValue(ctx.ReadByte(), valueType),
				BinXmlValueType.Int16 => new IntegerValue((short)ctx.ReadUInt16(), valueType),
				BinXmlValueType.UInt16 => new UnsignedIntegerValue(ctx.ReadUInt16(), valueType),
				BinXmlValueType.Int32 => new IntegerValue((int)ctx.ReadUInt32(), valueType),
				BinXmlValueType.HexInt32 => new HexIntegerValue(ctx.ReadUInt32(), valueType),
				BinXmlValueType.UInt32 => new UnsignedIntegerValue(ctx.ReadUInt32(), valueType),
				BinXmlValueType.Int64 => new IntegerValue((long)ctx.ReadUInt64(), valueType),
				BinXmlValueType.HexInt64 => new HexIntegerValue(ctx.ReadUInt64(), valueType),
				BinXmlValueType.UInt64 => new UnsignedIntegerValue(ctx.ReadUInt64(), valueType),
				BinXmlValueType.Real32 => new FloatValue(ctx.ReadReal32(), valueType),
				BinXmlValueType.Real64 => new FloatValue(ctx.ReadReal64(), valueType),
				BinXmlValueType.Bool => new BoolValue(0 != ctx.ReadByte()),
				BinXmlValueType.Binary => new BinaryValue(ctx.Consume(size).ToArray()),
				BinXmlValueType.Guid => new GuidValue(ctx.ReadGuid()),
				BinXmlValueType.Size => ctx.ReadSizeValue(size),
				BinXmlValueType.FileTime => ctx.ReadFileTime(),
				BinXmlValueType.SysTime => ctx.ReadSysTime(),
				BinXmlValueType.Sid => ctx.ReadSid(size),
				BinXmlValueType.Null => ctx.ReadNullValue(size),
				BinXmlValueType.BinXml => ctx.ReadBinXmlValue(),
				BinXmlValueType.StringArray => ctx.ReadVariableValueArray(size, valueType, (ref BinXmlReadContext ctx) => new TextValueNode(ctx.ReadNullTerminatedUnicodeString())),
				BinXmlValueType.AnsiStringArray => ctx.ReadVariableValueArray(size, valueType, (ref BinXmlReadContext ctx) => new TextValueNode(ctx.ReadNullTerminatedAnsiString())),
				BinXmlValueType.Int8Array => ctx.ReadFixedValueArray(size, 1, valueType, (ref BinXmlReadContext ctx) => new IntegerValue((sbyte)ctx.ReadByte(), valueType)),
				BinXmlValueType.UInt8Array => ctx.ReadFixedValueArray(size, 1, valueType, (ref BinXmlReadContext ctx) => new UnsignedIntegerValue(ctx.ReadByte(), valueType)),
				BinXmlValueType.Int16Array => ctx.ReadFixedValueArray(size, 2, valueType, (ref BinXmlReadContext ctx) => new IntegerValue((short)ctx.ReadUInt16(), valueType)),
				BinXmlValueType.UInt16Array => ctx.ReadFixedValueArray(size, 2, valueType, (ref BinXmlReadContext ctx) => new UnsignedIntegerValue(ctx.ReadUInt16(), valueType)),
				BinXmlValueType.Int32Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new IntegerValue((int)ctx.ReadUInt32(), valueType)),
				BinXmlValueType.UInt32Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new UnsignedIntegerValue(ctx.ReadUInt32(), valueType)),
				BinXmlValueType.Int64Array => ctx.ReadFixedValueArray(size, 8, valueType, (ref BinXmlReadContext ctx) => new IntegerValue((long)ctx.ReadUInt64(), valueType)),
				BinXmlValueType.UInt64Array => ctx.ReadFixedValueArray(size, 8, valueType, (ref BinXmlReadContext ctx) => new UnsignedIntegerValue(ctx.ReadUInt64(), valueType)),
				BinXmlValueType.Real32Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new FloatValue(ctx.ReadReal32(), valueType)),
				BinXmlValueType.Real64Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new FloatValue(ctx.ReadReal64(), valueType)),
				BinXmlValueType.BoolArray => ctx.ReadFixedValueArray(size, 1, valueType, (ref BinXmlReadContext ctx) => new BoolValue(0 != ctx.ReadByte())),
				BinXmlValueType.GuidArray => ctx.ReadFixedValueArray(size, 16, valueType, (ref BinXmlReadContext ctx) => new GuidValue(ctx.ReadGuid())),
				BinXmlValueType.HexInt32Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new HexIntegerValue(ctx.ReadUInt32(), valueType)),
				BinXmlValueType.HexInt64Array => ctx.ReadFixedValueArray(size, 4, valueType, (ref BinXmlReadContext ctx) => new HexIntegerValue(ctx.ReadUInt64(), valueType)),
				BinXmlValueType.FileTimeArray => ctx.ReadFixedValueArray(size, 8, valueType, (ref BinXmlReadContext ctx) => ctx.ReadFileTime()),
				BinXmlValueType.SysTimeArray => ctx.ReadFixedValueArray(size, 16, valueType, (ref BinXmlReadContext ctx) => ctx.ReadSysTime()),
				// TODO: How to tell if sizes are 32-bit or 64-bit?
				BinXmlValueType.SidArray => ctx.ReadVariableValueArray(size, valueType, (ref BinXmlReadContext ctx) => ctx.ReadSid(size)),
				//case BinXmlValueType.HexInt64Array:
				// TODO: How to tell if sizes are 32-bit or 64-bit?
				BinXmlValueType.SizeTArray => throw new NotImplementedException(),
				_ => throw new NotImplementedException()
			};
		}

		private TimeValue ReadFileTime()
		{
			return new TimeValue(DateTime.FromFileTimeUtc((long)this.ReadUInt64()), BinXmlValueType.FileTime);
		}

		private SizeValue ReadSizeValue(int size)
		{
			return new SizeValue((size == 8) ? this.ReadUInt64() : (size == 4) ? this.ReadUInt32() : throw new InvalidDataException($"Expected a size of 4 or 8, but encountered {size}."), size);
		}

		private NullValueNode ReadNullValue(int size)
		{
			this.Consume(size);
			return new NullValueNode();
		}

		private Node ReadBinXmlValue()
		{
			var frag = this.ReadFragment();
			this.ReadExpectedToken(Token.Eof);
			return frag;
		}

		private Node ReadSysTime()
		{
			// [MS-DTYP] § 2.3.13 SYSTEMTIME
			var year = this.ReadUInt16();
			var month = this.ReadUInt16();
			var dayOfWeek = this.ReadUInt16();
			var day = this.ReadUInt16();
			var hour = this.ReadUInt16();
			var minute = this.ReadUInt16();
			var second = this.ReadUInt16();
			var ms = this.ReadUInt16();

			DateTime dt = new DateTime(
				year,
				month,
				day,
				hour,
				minute,
				second,
				ms,
				DateTimeKind.Utc
				);
			return new TimeValue(dt, BinXmlValueType.SysTime);
		}

		private SidValue ReadSid(int size)
		{
			return new SidValue(new SecurityIdentifier(this.Consume(size)));
		}

		private double ReadReal64()
		{
			return BinaryPrimitives.ReadDoubleLittleEndian(this.Consume(8));
		}

		private float ReadReal32()
		{
			return BinaryPrimitives.ReadSingleLittleEndian(this.Consume(4));
		}

		private ulong ReadUInt64()
		{
			return BinaryPrimitives.ReadUInt64LittleEndian(this.Consume(8));
		}

		private delegate T ReaderFunc<T>(ref BinXmlReadContext ctx);
		private ArrayValue ReadFixedValueArray<T>(int size, int elemSize, BinXmlValueType valueType, ReaderFunc<T> func)
			where T : Node
		{
			int count = size / elemSize;
			FrugalList<Node> elements = new FrugalList<Node>(count);
			for (int i = 0; i < count; i++)
			{
				T elem = func(ref this);
				elements.Add(elem);
			}

			return new ArrayValue(elements);
		}
		private ArrayValue ReadVariableValueArray<T>(int size, BinXmlValueType valueType, ReaderFunc<T> func)
			where T : Node
		{
			FrugalList<Node> elements = new FrugalList<Node>();
			var subctx = new BinXmlReadContext(this.bytes.Slice(this.readIndex, size));
			while (subctx.readIndex < size)
			{
				T elem = func(ref subctx);
				elements.Add(elem);
			}

			this.readIndex += size;
			return new ArrayValue(elements);
		}

		private ProcessingInstruction TryReadMisc() => this.TryReadProlog();

		private string ReadNullTerminatedUnicodeString()
		{
			int cch;
			for (cch = 0; BinaryPrimitives.ReadUInt16LittleEndian(this.bytes.Slice(this.readIndex + cch * 2, 2)) != 0; cch++)
				;

			string str = ReadStringValue(cch);
			this.ReadExpectedByte(0);
			this.ReadExpectedByte(0);
			return str;
		}

		private string ReadNullTerminatedUnicodeString(int charCount)
		{
			string str = ReadStringValue(charCount);
			this.ReadExpectedByte(0);
			this.ReadExpectedByte(0);
			return str;
		}

		private string ReadNullTerminatedAnsiString()
		{
			int cch;
			for (cch = 0; this.bytes[this.readIndex + cch] != 0; cch++)
				;

			string str = ReadAnsiStringValue(cch);
			this.ReadExpectedByte(0);
			return str;
		}

		private string ReadStringValue(int charCount)
		{
			return Encoding.Unicode.GetString(this.Consume(charCount * 2));
		}

		private string ReadAnsiStringValue(int charCount)
		{
			// TODO: ANSI
			return Encoding.UTF8.GetString(this.Consume(charCount));
		}

		private string ReadLengthPrefixedUnicodeString()
		{
			var cch = this.ReadUInt16();
			return this.ReadStringValue(cch);
		}
	}




	// [MS-EVEN6] § 2.2.12 BinXml
	enum Token
	{
		Eof = 0,
		OpenStartElement_NoElements = 1,
		OpenStartElement_HasAttributes = 0x41,
		CloseStartElement = 2,
		CloseEmptyElement = 3,
		EndElement = 4,
		ValueText_5 = 5,
		ValueText_45 = 0x45,
		Attribute_Last = 6,
		Attribute_More = 0x46,
		CdataSection_7 = 7,
		CdataSection_47 = 0x47,
		CharRef_8 = 8,
		CharRef_48 = 0x48,
		EntityRef_9 = 9,
		EntityRef_49 = 0x49,
		PiTarget = 0xA,
		PiData = 0xB,
		TemplateInstance = 0x0C,
		NormalSubstitution = 0x0D,
		OptionalSubstitution = 0x0E,
		FragHeader = 0x0F,

		More = 0x40,
	}

	record struct ValueSpecEntry(int valueByteLength, BinXmlValueType valueType)
	{
		public Node? Value { get; set; }
	}



	enum BinXmlValueType
	{
		Null = 0,
		String = 1,
		AnsiString = 2,
		Int8 = 3,
		UInt8 = 4,
		Int16 = 5,
		UInt16 = 6,
		Int32 = 7,
		UInt32 = 8,
		Int64 = 9,
		UInt64 = 0x0A,
		Real32 = 0x0B,
		Real64 = 0x0C,
		Bool = 0x0D,
		Binary = 0x0E,
		Guid = 0x0F,
		Size = 0x10,
		FileTime = 0x11,
		SysTime = 0x12,
		Sid = 0x13,
		HexInt32 = 0x14,
		HexInt64 = 0x15,
		BinXml = 0x21,
		StringArray = 0x81,
		AnsiStringArray = 0x82,
		Int8Array = 0x83,
		UInt8Array = 0x84,
		Int16Array = 0x85,
		UInt16Array = 0x86,
		Int32Array = 0x87,
		UInt32Array = 0x88,
		Int64Array = 0x89,
		UInt64Array = 0x8A,
		Real32Array = 0x8B,
		Real64Array = 0x8C,
		BoolArray = 0x8D,
		GuidArray = 0x8F,
		SizeTArray = 0x90,
		FileTimeArray = 0x91,
		SysTimeArray = 0x92,
		SidArray = 0x93,
		HexInt32Array = 0x9400,
		HexInt64Array = 0x9500,
	}
}
