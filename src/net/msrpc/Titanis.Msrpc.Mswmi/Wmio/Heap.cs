using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	struct HeapRange
	{
		internal HeapRange(int startOffset, int length, HeapRangeType rangeType, string? tag)
		{
			this.startOffset = startOffset;
			this.length = length;
			this.rangeType = rangeType;
			this.tag = tag;
		}
		internal readonly int startOffset;
		internal readonly int length;
		private readonly HeapRangeType rangeType;
		private readonly string? tag;

		public override string ToString()
			=> $"Start offset: {this.startOffset} (0x{this.startOffset:X}); Length: {this.length} (0x{this.length:X}); Type: {this.rangeType}";
	}

	enum HeapRangeType
	{
		String,
		ObjectBlock,
		Array,
		QualifierSet,
		InputSignature,
		OutputSignature,
		PropertyInfo,
		EncodedValue
	}

	[PduStruct]
	partial struct HeapStruc
	{
		public HeapStruc(byte[] bytes)
		{
			this.bytes = bytes;
		}

		private uint length;
		private int byteCount => (int)(this.length & ~0x8000_0000);
		[PduArraySize(nameof(byteCount))]
		internal byte[] bytes;

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			this.length = (uint)this.bytes.Length | 0x8000_0000U;
		}
		partial void OnAfterReadPdu(IByteSource source)
		{
			Debug.Assert(0 != (this.length & 0x8000_0000));
		}

		internal Heap ToHeap() => new Heap(this.bytes);
	}
	class Heap : IPduStruct
	{
		// TODO: Once PduStructAttribute supports constructor parameters, remove this constructor
		public Heap()
		{
			this.Init(Array.Empty<byte>());

#if DEBUG
			this._enableTracking = true;
#endif
		}

		internal Heap(ReadOnlyMemory<byte> bytes)
		{
			this.Init(bytes);

#if DEBUG
			this._enableTracking = true;
#endif
		}

		private void Init(ReadOnlyMemory<byte> bytes)
		{
			this.Bytes = bytes;
			this.Reader = new ByteMemoryReader(bytes);
		}

		private ReadOnlyMemory<byte> Bytes { get; set; }
		private ByteMemoryReader Reader { get; set; }
		private bool _enableTracking;

		private List<HeapRange> _refRanges = new List<HeapRange>();


		// [MS-WMIO] § 2.2.80 - DictionaryReference
		internal static readonly string[] implicitStrings = new string[]
		{
			"\"",
			"key",
			string.Empty,
			"read",
			"write",
			"volatile",
			"provider",
			"dynamic",
			"cimwin32",
			"DWORD",
			"CIMTYPE"
		};
		internal T Resolve<T>(
			HeapRef heapRef,
			HeapRangeType rangeType,
			Func<ByteMemoryReader, T> readerFunc)
			=> this.Resolve<T>(heapRef, rangeType, null, readerFunc);
		internal T Resolve<T>(
			HeapRef heapRef,
			HeapRangeType rangeType,
			string? tag,
			Func<ByteMemoryReader, T> readerFunc)
		{
			Debug.Assert(!heapRef.IsNull);
			Debug.Assert(heapRef.IsHeapItem);

			Debug.Assert(heapRef.Value < this.Bytes.Length);

			// TODO: Synchronize for multi-thread if desired
			var reader = this.Reader;
			var pos_ = reader.Position;
			try
			{
				reader.Position = heapRef.Value;
				var value = readerFunc(reader);
				if (this._enableTracking)
					this._refRanges.Add(new HeapRange(heapRef.Value, reader.Position - heapRef.Value, rangeType, tag));
				return value;
			}
			finally
			{
				reader.Position = pos_;
			}
		}
		internal string? ResolveString(HeapStringRef nameRef, string? tag = null)
		{
			if (nameRef.IsNull)
				return null;

			if (nameRef.Value < 0)
			{
				// [MS-WMIO] § 2.2.80 - DictionaryReference
				var index = nameRef.Value & 0x7FFFFFFF;
				if (index < implicitStrings.Length)
					return implicitStrings[index];
				else
					throw new InvalidDataException("The HeapStringRef references a dictionary entry that is not present.");
			}

			var str = this.Resolve(nameRef.heapRef, HeapRangeType.String, tag, ByteSource.ReadPduStruct<EncodedString>).value;
			return str;
		}

		public void ReadFrom(IByteSource reader)
		{
			var struc = reader.ReadPduStruct<HeapStruc>();
			this.Init(struc.bytes);
		}

		public void ReadFrom(IByteSource reader, PduByteOrder byteOrder)
		{
			this.ReadFrom(reader);
		}

		public void WriteTo(ByteWriter writer)
		{
			// TODO: Remove ToArray
			writer.WritePduStruct(new HeapStruc(this.Bytes.ToArray()));
		}

		public void WriteTo(ByteWriter writer, PduByteOrder byteOrder)
		{
			this.WriteTo(writer);
		}
	}

	// [MS-WMIO] § 2.2.68 - HeapStringRef
	[PduStruct]
	internal partial struct HeapStringRef
	{
		public HeapStringRef(uint value)
		{
			this.heapRef = new HeapRef(value);
		}

		internal static HeapStringRef Null => new HeapStringRef(HeapRef.NullHeapRefValue);

		internal HeapRef heapRef;
		internal int Value => this.heapRef.Value;
		internal bool IsNull => this.heapRef.IsNull;
	}
	// [MS-WMIO] § 2.2.69 - HeapRef
	internal struct HeapRef : IPduStruct
	{
		internal const uint NullHeapRefValue = 0xFFFFFFFF;
		public static HeapRef Null => new HeapRef(NullHeapRefValue);
		public HeapRef(uint value)
		{
			this._value = NormalizeValue(value);
		}

		private static uint NormalizeValue(uint value) =>
			// Value == 0 refers to a valid item, but an uninitialized structure has a value == 0.
			// So swap these two.
			value is NullHeapRefValue or 0 ? ~value : value;
		private static int UnnormalizeValue(uint value) => value is NullHeapRefValue or 0 ? (int)~value : (int)value;

		public override string ToString()
			=> this.IsNull ? "<null heap ref>"
			: $"Heap offset = {this.Value} (0x{this.Value:X})";

		#region IPduStruct
		void IPduStruct.ReadFrom(IByteSource reader)
		{
			this._value = NormalizeValue(reader.ReadUInt32LE());
		}

		void IPduStruct.ReadFrom(IByteSource reader, PduByteOrder byteOrder)
		{
			this._value = NormalizeValue(reader.ReadUInt32LE());
		}

		void IPduStruct.WriteTo(ByteWriter writer)
		{
			writer.WriteInt32LE(this.Value);
		}

		void IPduStruct.WriteTo(ByteWriter writer, PduByteOrder byteOrder)
		{
			writer.WriteInt32LE(this.Value);
		}
		#endregion

		private uint _value;
		public int Value => UnnormalizeValue(this._value);

		public bool IsNull => this._value == 0; // Because NullValue and 0 are swapped
		public bool IsHeapItem => this.Value >= 0;
		public bool IsDictionaryRef => !this.IsNull && this.Value < 0;
	}
}
