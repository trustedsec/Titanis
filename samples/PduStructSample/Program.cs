using System.Runtime.InteropServices;
using Titanis;
using Titanis.IO;
using Titanis.PduStruct;

// Sets the default byte order for all PDU structs defined in this assembly
[assembly: PduByteOrder(PduByteOrder.LittleEndian)]

namespace PduStructSample
{
	internal class Program
	{
		static void ReadWriteStruct<T>(T struc)
			where T : IPduStruct, new()
		{
			ByteWriter writer = new ByteWriter();
			writer.WritePduStruct(struc);

			byte[] bytes = writer.GetData().ToArray();

			ByteMemoryReader reader = new ByteMemoryReader(bytes);
			var decoded = reader.ReadPduStruct<T>();
		}

		static void Main(string[] args)
		{
			// Note the project reference to Titanis.SourceGen
		}
	}

	enum PduType : byte
	{
		Request,
		Response
	}

	[PduStruct]
	// This struct overrides the default set for the assembly (above)
	[PduByteOrder(PduByteOrder.BigEndian)]
	partial struct Pdu1
	{
		// Not actually read or written, but captures the position within the stream
		[PduPosition]
		internal long position;

		// Read as the enum's underlying type (byte)
		internal PduType pduType;

		// All fields are processed, even private fields
		private byte pad;

		// Ignored
		[PduIgnore]
		public object? tag;

		// Define an optional field
		private bool ShouldIncludeOptional => (this.pduType == PduType.Request);
		[PduConditional(nameof(ShouldIncludeOptional))]
		internal int? optional;

		// This works for reference types as well, using the Nullable annotation
		[PduConditional(nameof(ShouldIncludeOptional))]
		internal DerivedPduClass? derived;

		// Properties are ignored by default
		public object? Tag => this.tag;

		// Properties can be included by applying [PduField]
		[PduField]
		public int Id { get; set; }

		// Composable using an embedded PDU struct
		internal CountedString str1;

		// Overrides the byte order for this field
		[PduByteOrder(PduByteOrder.LittleEndian)]
		internal int int32LE;

		// Include a parameterized struct
		internal int paramForStruct;
		[PduArguments(nameof(paramForStruct))]
		internal ParameterizedStruct paramStruct;

		// Customized read/write
		[PduField(ReadMethod = nameof(ReadCustom), WriteMethod = nameof(WriteCustom))]
		internal NonPduClass customField;
		private NonPduClass ReadCustom(IByteSource source, PduByteOrder byteOrder)
		{
			Console.WriteLine("ReadCustom called");
			return new NonPduClass();
		}
		private void WriteCustom(ByteWriter writer, NonPduClass value, PduByteOrder byteOrder)
		{
			Console.WriteLine("WriteCustom called");
		}

		// The struct may implement callbacks
		partial void OnBeforeReadPdu(IByteSource writer)
		{
			Console.WriteLine("OnBeforeReadPdu called");
		}
		partial void OnAfterReadPdu(IByteSource writer)
		{
			Console.WriteLine("OnAfterReadPdu called");
		}
		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			Console.WriteLine("OnBeforeWritePdu called");
		}
		partial void OnAfterWritePdu(ByteWriter writer)
		{
			Console.WriteLine("OnAfterWritePdu called");
		}
	}

	[PduStruct]
	partial class PduClass
	{
		internal int value;
	}

	[PduStruct]
	partial class DerivedPduClass : PduClass
	{
		internal int value2;
	}

	class NonPduClass
	{

	}

	[PduStruct]
	partial struct CountedString
	{
		internal ushort byteCount;

		[PduString(CharSet.Ansi, nameof(byteCount))]
		internal string str;
	}

	[PduStruct]
	partial struct CountedArray
	{
		internal ushort elementCount;

		[PduArraySize(nameof(elementCount))]
		internal int[] elements;
	}

	[PduStruct]
	partial struct ConditionalField
	{
		internal int includedFields;

		internal bool IncludeField1 => 0 != (this.includedFields & 1);
		[PduConditional(nameof(IncludeField1))]
		internal int? field1;

		internal bool IncludeField2 => 0 != (this.includedFields & 2);
		[PduConditional(nameof(IncludeField2))]
		internal int field2;

		internal bool IncludeField3 => 0 != (this.includedFields & 4);
		internal const int StringLength = 42;
		[PduConditional(nameof(IncludeField3))]
		[PduString(CharSet.Ansi, nameof(StringLength))]
		internal string? field3;
	}

	// This struct requires a parameter
	[PduStruct]
	partial struct ParameterizedStruct
	{
		// Received as a parameter
		[PduParameter]
		internal int countParam;

		[PduArraySize(nameof(countParam))]
		internal int[] elements;
	}

	[PduStruct]
	partial struct PduWithPosition
	{
		[PduPosition]
		internal long positionWithinStream;
	}

	[PduStruct]
	partial struct PduWithCustomField
	{
		[PduField(ReadMethod = nameof(ReadCustom), WriteMethod = nameof(WriteCustom))]
		[PduAlignment(2)]
		internal string customField;
		private string ReadCustom(IByteSource source, PduByteOrder byteOrder)
		{
			Console.WriteLine("ReadCustom called");
			return "";
		}
		private void WriteCustom(ByteWriter writer, string value, PduByteOrder byteOrder)
		{
			Console.WriteLine("WriteCustom called");
		}
	}


}
